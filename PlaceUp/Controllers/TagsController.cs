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

namespace PlaceUp.Controllers
{
    [RoutePrefix("Tags")]
    public class TagsController : Controller
    {
        ITagRepository tagRep;

        public TagsController(ITagRepository tagRepository)
        {
            tagRep = tagRepository;
        }

        [Route("GetAll")]
        public async Task<ActionResult> Index()
        {
            return View(await tagRep.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TagId, Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                if (!await tagRep.IsExist(tag.Name))
                {
                    tag.TagId = Guid.NewGuid();
                    await tagRep.Add(tag);
                }
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                await tagRep.DeleteByGuid(id);
            }
            return RedirectToAction("Index"); ;
        }
    }
}
