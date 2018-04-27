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
using PlaceUp.Context.FeedbackRepository;

namespace PlaceUp.Controllers
{
    [RoutePrefix("Feedbacks")]
    public class FeedbacksController : Controller
    {
        IFeedbackRepository feedbackRep;

        public FeedbacksController(IFeedbackRepository feedbackRepository)
        {
            feedbackRep = feedbackRepository;
        }

        [Route("GetAll")]
        public async Task<ActionResult> Index()
        {
            return View(await feedbackRep.GetAll());
        }

        [Authorize(Roles = "user, admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FeedbackId,LikeDislike,Review,PlaceId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                await feedbackRep.Add(feedback);
                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            Guid? guid = null;
            if (id != null)
            {
                var feedback = await feedbackRep.GetByGuid(id);
                guid = feedback.PlaceId;
                await feedbackRep.DeleteByGuid(id);
            }
            return RedirectToAction("Edit", "Places", new { id = guid });
        }
    }
}