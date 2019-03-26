﻿using System;
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
    public class TeamsController : Controller
    {
        private SportsTrackDBContext db = new SportsTrackDBContext();

        // GET: Teams
        public ActionResult Index()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            return View(db.Teams.OrderBy(teams => teams.Name).ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,wins,losses,season")] Team team)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,wins,losses,season")] Team team)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Follow(int teamName)
        {
            var TeamsFollowing = (from teams in db.Following
                                  where ((teams.teamID == teamName) && (teams.userName == User.Identity.Name))
                                  select teams).ToList();

            if(TeamsFollowing.Count == 0)
            {
                Following newFollow = new Following();
                newFollow.teamID = teamName;
                newFollow.userName = User.Identity.Name;
                db.Following.Add(newFollow);
                db.SaveChanges();
            }
            

            return Json(teamName);
        }

        [HttpPost]
        public ActionResult Unfollow(int teamName)
        {
            var TeamsFollowing = (from teams in db.Following
                                  where ((teams.teamID == teamName) && (teams.userName == User.Identity.Name))
                                  select teams).ToList();

            if (TeamsFollowing.Count != 0)
            {
                Following newUnfollow = db.Following.Find(TeamsFollowing[0].id);
                db.Following.Remove(newUnfollow);
                db.SaveChanges();
            }

            return Json(teamName);
        }

        [HttpPost]
        public ActionResult IsFollowing(int teamName)
        {
            var TeamsFollowing = (from teams in db.Following
                                  where ((teams.teamID == teamName) && (teams.userName == User.Identity.Name))
                                  select teams).ToList();

            if (TeamsFollowing.Count != 0)
            {
                return Json(1);
            }

            return Json(teamName);
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
