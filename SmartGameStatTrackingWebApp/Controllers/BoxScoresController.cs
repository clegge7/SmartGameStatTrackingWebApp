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

        // GET: BoxScores
        public ActionResult Index()
        {
            return View(db.BoxScores.ToList());
        }

        // GET: BoxScores/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: BoxScores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoxScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,gameid,number,player,teamID,playerid,points,rebounds,assists,blocks,steals,turnovers,personalFouls,technicalFouls")] BoxScore boxScore)
        {
            if (ModelState.IsValid)
            {
                db.BoxScores.Add(boxScore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boxScore);
        }

        // GET: BoxScores/Edit/5
        public ActionResult Edit(int? id)
        {
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
            if (ModelState.IsValid)
            {
                db.Entry(boxScore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boxScore);
        }

        // GET: BoxScores/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: BoxScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoxScore boxScore = db.BoxScores.Find(id);
            db.BoxScores.Remove(boxScore);
            db.SaveChanges();
            return RedirectToAction("Index");
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
