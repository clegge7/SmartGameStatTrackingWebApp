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
    public class BoxScoresController : Controller
    {
        private SportsTrackDBContext db = new SportsTrackDBContext();

        // GET: BoxScores/Edit/5
        public ActionResult Edit(int? id)
        {
            if(User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin" && User.Identity.Name != "stephan" && User.Identity.Name != "Marius")
            {
                return Redirect("/Home/Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoxScore boxScore = db.BoxScores.Find(id);
            if (boxScore == null)
            {
                return HttpNotFound();
            }
            return View(boxScore);
        }

        // POST: BoxScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,gameid,number,player,teamID,playerid,points,rebounds,assists,blocks,steals,turnovers,personalFouls,technicalFouls")] BoxScore boxScore)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin" && User.Identity.Name != "stephan" && User.Identity.Name != "Marius")
            {
                return Redirect("/Home/Index");
            }
            int gameid = boxScore.gameid;
            if (ModelState.IsValid)
            {
                db.Entry(boxScore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Games", new { id = gameid });
            }
            return RedirectToAction("Details", "Games", new { id = gameid });
        }

        public int GetStat(int ID, string stat)
        {
            if (stat == "player")
            {
                return db.BoxScores.FirstOrDefault(x => x.id == ID).playerid;
            }
            if (stat == "points")
            {
                return db.BoxScores.FirstOrDefault(x => x.id == ID).points;
            }
            return -1;
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
