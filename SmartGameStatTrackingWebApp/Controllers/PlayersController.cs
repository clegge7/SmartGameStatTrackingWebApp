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
    public class PlayersController : Controller
    {
        private SportsTrackDBContext db = new SportsTrackDBContext();

        // GET: Players
        public ActionResult Index()
        {
            return View(db.Players.OrderBy(players => players.name).ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,number,name,team,gamesPlayed,points,rebounds,assists,blocks,steals,turnovers,personalFouls,technicalFouls,season")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                var teamID = (from teams in db.Teams
                             where teams.Name == player.team
                             select teams).FirstOrDefault().ID;
                return RedirectToAction("Details", "Teams", new { id = teamID });
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,number,name,team,gamesPlayed,points,rebounds,assists,blocks,steals,turnovers,personalFouls,technicalFouls,season")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetTeamPlayers(int teamID, int gameID)
        {
            var playersOnTeam = from players in db.BoxScores.OrderBy(x => x.number)
                                where ((players.teamID == teamID) && (players.gameid==gameID))
                                select players;

            return Json(playersOnTeam);
        }

        [HttpPost]
        public ActionResult GetTeamPlayers2(string teamName)
        {
            var playersOnTeam = from players in db.Players.OrderBy(x => x.number)
                                where (players.team == teamName)
                                select players;

            return Json(playersOnTeam);
        }

        [HttpPost]
        public ActionResult GetPlayers(IEnumerable<SmartGameStatTrackingWebApp.Models.Player> playerModel)
        {
            var playerList = from players in playerModel
                             select players;
            
            return Json(playerList);
        }

        //Need to get working
        [HttpPost]
        public ActionResult PlayerSearch(string query, string category)
        {
            return Json("0");
            if(category == "Players")
            {
                var playerList = (from players in db.Players
                                 where players.name.ToLower() == query.ToLower()
                                 select players).AsEnumerable();

                return RedirectToAction("Index", "Players", playerList);
            }
            else if(category == "Teams")
            {
                var playerList = (from players in db.Players
                                 where players.team == query
                                 select players).AsEnumerable();

                return RedirectToAction("Index", "Players", playerList);
            }
            else
            {
                var playerList = (from players in db.Players
                                 where ((players.name == query) || (players.team==query))
                                 select players).AsEnumerable();

                return RedirectToAction("Index", "Players", playerList);
            }
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
