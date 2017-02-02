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

                    if (stores != null && stores.Count() > 0)
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

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetMyStores()
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    var stores = repo.GetStoresByUsername(User.Identity.Name);

                    if (stores != null && stores.Count() > 0)
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
        [Authorize]
        public async Task<IHttpActionResult> AddStore([FromBody]HotDogStoreViewModel storeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HotDogRepository repo = new HotDogRepository())
                    {
                        var newStore = Mapper.Map<HotDogStore>(storeViewModel);

                        newStore.ManagerName = User.Identity.Name;

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
                    Request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError, ex));
            }
        }
    }
}