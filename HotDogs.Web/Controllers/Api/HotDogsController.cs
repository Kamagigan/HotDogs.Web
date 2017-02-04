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
using System.Web;
using System.Web.Http;

namespace HotDogs.Web.Controllers.Api
{
    public class HotDogsController : ApiController
    {
        public IHttpActionResult GetByStoreId(int storeId)
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    if (storeId > 0)
                    {
                        var hotdogs = repo.GetHotDogsByStoreId(storeId);

                        if (hotdogs != null && hotdogs.Count() > 0)
                        {
                            return Ok(Mapper.Map<IEnumerable<HotDogViewModel>>(hotdogs));
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return BadRequest("StoreId doit être superieur à 0");
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
        public async Task<IHttpActionResult> Add(int storeId, [FromBody] HotDogViewModel hotDogViewModel)
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    if (ModelState.IsValid)
                    {
                        var newHotDog = Mapper.Map<HotDog>(hotDogViewModel);

                        repo.AddHotDog(storeId, newHotDog);

                        if (await repo.SaveChangesAsync())
                        {
                            return Created($"api/stores/{newHotDog.Store.Id}/hotdogs/{newHotDog.Id}",
                                Mapper.Map<HotDogViewModel>(newHotDog));
                        }
                        else
                        {
                            return ResponseMessage(
                                Request.CreateErrorResponse(
                                    HttpStatusCode.InternalServerError, "euh.... Savegarde en BDD échoué"));
                        }
                    }

                    return BadRequest(ModelState); 
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete (int hotdogId)
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    repo.DeleteHotDogById(hotdogId);

                    if (await repo.SaveChangesAsync())
                    {
                        return Ok();
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
    }
}