using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartGameStatTrackingWebApp.Models;

namespace SmartGameStatTrackingWebApp.Controllers
{
    public class ProfilesController : Controller
    {
        private SportsTrackDBContext db = new SportsTrackDBContext();

        // GET: Profiles/Details/5
        public ActionResult Details(string id)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = db.Profiles.Find(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        [HttpPost]
        public ActionResult GetFlaggedAudio(int test)
        {
            var flaggedAudio = from audio in db.audio_File_Contents
                               where (audio.manual_control == true)
                               select audio;

            return Json(flaggedAudio);
        }

        [HttpPost]
        public ActionResult ClearFlag(int audio_id)
        {
            var flaggedAudio = db.audio_File_Contents.Find(audio_id);
            flaggedAudio.manual_control = false;
            db.Entry(flaggedAudio).State = EntityState.Modified;
            db.SaveChanges();

            return Json(flaggedAudio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
