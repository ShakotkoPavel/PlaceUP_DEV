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
using PlaceUp.Context.CategoryRepository;

namespace PlaceUp.Controllers
{
    [RoutePrefix("Categories")]
    public class CategoriesController : Controller
    {
        ICategoryRepository catRep;

        public CategoriesController(ICategoryRepository catRepository)
        {
            catRep = catRepository;
        }

        //[Route("GetAll")]
        //public async Task<ActionResult> Index()
        //{
        //    return View(await catRep.GetAll());
        //}


        //[Authorize(Roles = "admin")]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            return Json(new { data = await catRep.GetAll() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(Guid? id = null)
        {
            if (id == null)
            {
                return View(new Category());
            }
            else
            {
                return View(await catRep.GetByGuid(id));
            }
        }

        [HttpPost]
        [Route("AddOrEdit")]
        public async Task<ActionResult> AddOrEdit(Category cat)
        {
            if (cat.CategoryId == new Guid())
            {
                await catRep.Add(cat);
                return Json(new { success = true, message = "Saved successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                await catRep.Edit(cat);
                return Json(new { success = true, message = "Update successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "CategoryId,Name")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            await catRep.Add(category);
        //            return RedirectToAction("Index");
        //        }

        //        return View(category);
        //    }

        //    return View(category);
        //}

        //[Authorize(Roles = "admin")]
        //public async Task<ActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    if (!await catRep.IsExist(id))
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(await catRep.GetByGuid(id));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "CategoryId,Name")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await catRep.Edit(category);
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}
    }
}