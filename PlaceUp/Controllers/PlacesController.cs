using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlaceUp.Context;
using PlaceUp.Models;
using PlaceUp.Context.PlaceRepository;

namespace PlaceUp.Controllers
{
    [RoutePrefix("Places")]
    public class PlacesController : Controller
    {
        IPlaceRepository placeRep;

        public PlacesController(IPlaceRepository placeRepository)
        {
            placeRep = placeRepository;
        }

        [Route("GetAll")]
        public async Task<ActionResult> Index()
        {
            return View(await placeRep.GetAll());
        }

        [Route("GetAllByTag/{name}")]
        public async Task<ActionResult> GetAllByTag(string name)
        {
            return View(await placeRep.GetAllByTag(name));
        }

        [Route("GetAllByTagId/{id}")]
        public async Task<ActionResult> GetAllByTagId(Guid? id)
        {
            return View("Index", await placeRep.GetAllByTagId(id));
        }

        [Route("GetAllByCategoryId/{id}")]
        public async Task<ActionResult> GetAllByCategoryId(Guid? id)
        {
            return View("Index", await placeRep.GetAllByCategoryId(id));
        }

        [Authorize(Roles ="user")]
        [Route("Details/{id}")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!await placeRep.IsExist(id))
            {
                return HttpNotFound();
            }
            return View(await placeRep.GetByGuid(id));
        }

        [Authorize(Roles = "user")]
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoriesList = new SelectList(await placeRep.GetAllCategories(), "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PlaceId, Name, Adress, Phone, WebSite,Email, OpeningDate, Description, ImageData, ImageMimeType, CategoryId")] Place place, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    place.ImageMimeType = image.ContentType;
                    place.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(place.ImageData, 0, image.ContentLength);
                }
                await placeRep.Add(place);
                return RedirectToAction("Index");
            }

            return View(place);
        }

        [Route("Details/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!await placeRep.IsExist(id))
            {
                return HttpNotFound();
            }

            ViewBag.CategoriesList = new SelectList(await placeRep.GetAllCategories(), "CategoryId", "Name");
            return View(await placeRep.GetByGuid(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PlaceId, Name, Adress, Phone, WebSite, Email, OpeningDate, Description, ImageData, ImageMimeType, CategoryId")] Place place, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    place.ImageMimeType = image.ContentType;
                    place.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(place.ImageData, 0, image.ContentLength);
                }
                await placeRep.Edit(place);
                return RedirectToAction("Index");
            }
            return View(place);
        }

        public async Task<FileContentResult> GetImage(Guid? id)
        {
            if (await placeRep.IsExist(id))
            {
                var place = await placeRep.GetByGuid(id);
                if(place.ImageData != null)
                {
                    return File(place.ImageData, place.ImageMimeType);
                }

                return null;
            }

            return null;
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!await placeRep.IsExist(id))
            {
                return HttpNotFound();
            }
            return View(await placeRep.GetByGuid(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
        {
            await placeRep.DeleteByGuid(id);
            return RedirectToAction("Index");
        }
    }
}