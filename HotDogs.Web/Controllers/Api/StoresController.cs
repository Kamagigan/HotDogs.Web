using AutoMapper;
using HotDogs.Web.Context;
using HotDogs.Web.Models;
using HotDogs.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HotDogs.Web.Controllers.Api
{
    public class StoresController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    var stores = repo.GetAllStores();

                    if (stores.Any())
                    {
                        return Ok(Mapper.Map<IEnumerable<HotDogStoreViewModel>>(stores));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    using (HotDogRepository repo = new HotDogRepository())
                    {
                        var store = repo.GetStoreById(id);

                        if (store != null)
                        {
                            return Ok(Mapper.Map<HotDogViewModel>(store));
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
                else
                {
                    return BadRequest("Id doit être superieur à 0");
                }
            }
            catch(Exception ex)
            {
                return ResponseMessage(
                   Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpGet]
        [Authorize(Roles = "customer")]
        [Route("favorites")]
        public IHttpActionResult GetFavoriteStores()
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    var stores = repo.GetUserFavoriteStores(User.Identity.Name);

                    if (stores.Any())
                    {
                        return Ok(Mapper.Map<IEnumerable<HotDogStoreViewModel>>(stores));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> AddStore([FromBody]HotDogStoreViewModel storeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HotDogRepository repo = new HotDogRepository())
                    {
                        var newStore = Mapper.Map<HotDogStore>(storeViewModel);

                        newStore.Owner = repo.GetManagerByName(storeViewModel.OwnerName);

                        repo.AddStore(newStore);

                        if (await repo.SaveChangesAsync())
                        {
                            return Created($"api/stores/{newStore.Id}", Mapper.Map<HotDogStoreViewModel>(newStore));
                        }
                        else
                        {
                            return BadRequest("euh.... Savegarde en BDD échoué");
                        } 
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        // Not Working for now

        //[HttpDelete]
        //[Authorize(Roles = "manager, admin")]
        //public async Task<IHttpActionResult> DeleteStore(int id)
        //{
        //    try
        //    {
        //        using (var repo = new HotDogRepository())
        //        {
        //            HotDogStore store = store = repo.GetStoreById(id);

        //            if (store == null)
        //                return NotFound();

        //            if (User.IsInRole("admin") || repo.isValidManagerForStore(store.Id, User.Identity.Name))
        //            {
        //                // l'utilisateur a le droit de supprimer le magasin, donc on peut lancer la suppression
        //                repo.DeleteStore(store);

        //                await repo.SaveChangesAsync();

        //                return Ok();
        //            }
        //            else
        //            {
        //                return ResponseMessage(
        //                    Request.CreateResponse(HttpStatusCode.Forbidden));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseMessage(
        //            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
        //    }
        //}
    }
}