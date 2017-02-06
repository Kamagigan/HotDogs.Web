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
    [RoutePrefix("api/hotdogs")]
    public class HotDogsController : ApiController
    {
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetHotDogById(int id)
        {
            try
            {
                if (id > 0)
                {
                    using (var repo = new HotDogRepository())
                    {
                        var hotdog = repo.GetHotDogById(id);

                        if (hotdog != null)
                        {
                            //return Ok(Mapper.Map<IEnumerable<HotDogViewModel>>(hotdog));
                            return Ok(hotdog);
                        }
                        else
                        {
                            //ce hotdog specifique n'existe pas alors on renvoie NotFound
                            return NotFound();
                        }
                    }
                }
                else
                {
                    return BadRequest("HotDogId doit être superieur à 0");
                }
            }
            catch(Exception ex)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpGet]
        [Route("store/{id:int}")]
        public IHttpActionResult GetByStoreId(int id)
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    if (id > 0)
                    {
                        var hotdogs = repo.GetHotDogsByStoreId(id);

                        if (hotdogs != null && hotdogs.Count() > 0)
                        {
                            //return Ok(Mapper.Map<IEnumerable<HotDogViewModel>>(hotdogs));
                            return Ok(hotdogs);
                        }
                        else
                        {
                            // la liste de Hotdogs du magasin est vide mais ce n'est pas erreur alors on renvoie rien 
                            return Ok();
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
        [Authorize(Roles = "manager,admin")]
        public async Task<IHttpActionResult> AddHotDog(int storeId, [FromBody] HotDogViewModel hotDogViewModel)
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    if ((User.IsInRole("admin") || repo.isValidManagerForStore(storeId, User.Identity.Name))
                        && ModelState.IsValid)
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
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpDelete]
        [Authorize(Roles = "manager,admin")]
        public async Task<IHttpActionResult> DeleteHotDog (int hotdogId)
        {
            try
            {
                using (HotDogRepository repo = new HotDogRepository())
                {
                    var hotdog = repo.GetHotDogById(hotdogId);

                    if (hotdog == null)
                        return NotFound();

                    if (User.IsInRole("admin") || repo.isValidManagerForStore(hotdog.Store.Id, User.Identity.Name))
                    {             
                        repo.DeleteHotDog(hotdog);

                        if (await repo.SaveChangesAsync())
                        {
                            return Ok();
                        }
                        else
                        {
                            throw new Exception("Sauvegarde en base échouée");
                        }
                    }
                    else
                    {
                        return ResponseMessage(
                            Request.CreateResponse(HttpStatusCode.Forbidden));
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