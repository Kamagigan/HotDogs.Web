using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using HotDogs.Web.Context;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotDogs.Web.Identity
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext oauthContext)
        {
            oauthContext.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            // Get the user
            var user = oauthContext.OwinContext.Get<HotDogContext>().Users.FirstOrDefault(u => u.UserName == oauthContext.UserName);

            // Check Password
            if (!oauthContext.OwinContext.Get<HotDogUserManager>().CheckPassword(user, oauthContext.Password))
            {
                oauthContext.SetError("invalid_grant", "The username or password is incorrect");
                oauthContext.Rejected();
                return Task.FromResult<object>(null);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentity(oauthContext, user), new AuthenticationProperties());
            oauthContext.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, IdentityUser user)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            var userRoles = context.OwinContext.Get<HotDogUserManager>().GetRoles(user.Id);
            foreach(var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}