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
    public class GamesController : Controller
    {
        private SportsTrackDBContext db = new SportsTrackDBContext();

        // GET: Games
        public ActionResult Index()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            return View(db.Games.OrderByDescending(game => game.gameDate).ToList());
        }

        // GET: Games/Details/5
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
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,gameDate,homeTeam,awayTeam")] Game game)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (ModelState.IsValid)
            {
                game.homeTeamID = (from teams in db.Teams
                                   where (teams.Name == game.homeTeam)
                                   select teams.ID).FirstOrDefault();
                game.awayTeamID = (from teams in db.Teams
                                   where (teams.Name == game.awayTeam)
                                   select teams.ID).FirstOrDefault();
                game.homePoints = 0;
                game.awayPoints = 0;
                db.Games.Add(game);
                db.SaveChanges();
                int gameID = (from games in db.Games
                              select games).OrderByDescending(games => games.id)
                              .FirstOrDefault().id;

                var playersHome = (from players in db.Players
                                  where (players.team == game.homeTeam)
                                  select players).ToList();
                var playersAway = (from players in db.Players
                                   where (players.team == game.awayTeam)
                                   select players).ToList();

                for(int i = 0; i < playersHome.Count; i++)
                {
                    BoxScore boxscorehome = new BoxScore();
                    boxscorehome.gameid = gameID;
                    boxscorehome.number = playersHome[i].number;
                    boxscorehome.player = playersHome[i].name;
                    boxscorehome.playerid = playersHome[i].id;
                    boxscorehome.teamID = game.homeTeamID;
                    boxscorehome.points = 0;
                    boxscorehome.rebounds = 0;
                    boxscorehome.assists = 0;
                    boxscorehome.blocks = 0;
                    boxscorehome.steals = 0;
                    boxscorehome.turnovers = 0;
                    boxscorehome.personalFouls = 0;
                    boxscorehome.technicalFouls = 0;
                    db.BoxScores.Add(boxscorehome);
                }

                for (int i = 0; i < playersAway.Count; i++)
                {
                    BoxScore boxscoreaway = new BoxScore();
                    boxscoreaway.gameid = gameID;
                    boxscoreaway.number = playersHome[i].number;
                    boxscoreaway.player = playersAway[i].name;
                    boxscoreaway.playerid = playersAway[i].id;
                    boxscoreaway.teamID = game.awayTeamID;
                    boxscoreaway.points = 0;
                    boxscoreaway.rebounds = 0;
                    boxscoreaway.assists = 0;
                    boxscoreaway.blocks = 0;
                    boxscoreaway.steals = 0;
                    boxscoreaway.turnovers = 0;
                    boxscoreaway.personalFouls = 0;
                    boxscoreaway.technicalFouls = 0;
                    db.BoxScores.Add(boxscoreaway);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
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
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,homeTeamID,awayTeamID,gameDate,homeTeam,awayTeam,homePoints,awayPoints")] Game game)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
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
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            var BoxScores = (from boxscores in db.BoxScores
                             where (boxscores.gameid == game.id)
                             select boxscores).ToList();
            for(int i = 0; i < BoxScores.Count; i++)
            {
                db.BoxScores.Remove(BoxScores[i]);
            }
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
