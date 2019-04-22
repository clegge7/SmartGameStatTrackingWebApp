using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        [ChildActionOnly]
        public ActionResult _PartialIndex()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }

            var userName = User.Identity.Name;

            var teamsFollowing = (from teams in db.Following
                                  where (teams.userName == userName)
                                  select teams.teamID).ToList();

            List<Game> GamesFollowing = new List<Game>();

            for(int i = 0; i < teamsFollowing.Count; i++)
            {
                var tempTeamID = teamsFollowing[i];
                var tempGame = (from games in db.Games
                               where ((games.awayTeamID == tempTeamID) || (games.homeTeamID == tempTeamID))
                               select games).ToList();

                for(int j = 0; j < tempGame.Count; j++)
                {
                    GamesFollowing.Add(tempGame[j]);
                }
            }

            //return PartialView("_PartialIndex", db.Games.OrderByDescending(game => game.gameDate).ToList());
            return PartialView("_PartialIndex", GamesFollowing);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin")
            {
                return Redirect("/Home/Index");
            }
            ViewBag.Teams = new SelectList(db.Teams.OrderBy(team => team.Name), "ID", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,homeTeamID,awayTeamID,gameDate,homeTeam,awayTeam,homePoints,awayPoints,StartGame,StartQ2,StartQ3,StartQ4,GameEnd,DeviceID")] Game game)
        {
            ViewBag.Teams = new SelectList(db.Teams.OrderBy(team => team.Name), "ID", "Name");
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin")
            {
                return Redirect("/Home/Index");
            }
            if (ModelState.IsValid)
            {
                game.homeTeam = (from teams in db.Teams
                                   where (teams.ID == game.homeTeamID)
                                   select teams.Name).FirstOrDefault();
                game.awayTeam = (from teams in db.Teams
                                   where (teams.ID == game.awayTeamID)
                                   select teams.Name).FirstOrDefault();
                game.homePoints = 0;
                game.awayPoints = 0;
                DateTime dateOnly = game.gameDate;
                DateTime timeonly = game.StartGame;
                game.StartGame = game.gameDate.Date.Add(game.StartGame.TimeOfDay);
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

                    playersHome[i].gamesPlayed += 1;
                    db.Entry(playersHome[i]).State = EntityState.Modified;
                }

                for (int i = 0; i < playersAway.Count; i++)
                {
                    BoxScore boxscoreaway = new BoxScore();
                    boxscoreaway.gameid = gameID;
                    boxscoreaway.number = playersAway[i].number;
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

                    playersAway[i].gamesPlayed += 1;
                    db.Entry(playersAway[i]).State = EntityState.Modified;
                }

                db.SaveChanges();

                device_used_in_game device_game_pair = new device_used_in_game();
                device_game_pair.d_id = game.DeviceID;
                device_game_pair.g_id = game.id;
                db.device_used_in_game.Add(device_game_pair);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if(User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin")
            {
                return Redirect("/Home/Index");
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
        public ActionResult Edit([Bind(Include = "id,homeTeamID,awayTeamID,gameDate,homeTeam,awayTeam,homePoints,awayPoints,StartGame,StartQ2,StartQ3,StartQ4,GameEnd,DeviceID")] Game game)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin")
            {
                return Redirect("/Home/Index");
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
            else if (User.Identity.Name != "colin")
            {
                return Redirect("/Home/Index");
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
            else if (User.Identity.Name != "colin")
            {
                return Redirect("/Home/Index");
            }
            Game game = db.Games.Find(id);

            var audio_file = (from audio in db.audio_File_Contents
                              where audio.g_id == game.id
                              select audio).ToList();
            for(int i =0; i<audio_file.Count; i++)
            {
                db.audio_File_Contents.Remove(audio_file[i]);
            }
            db.SaveChanges();

            var device_game_pair = (from devices in db.device_used_in_game
                                    where (devices.g_id == game.id)
                                    select devices).FirstOrDefault();
            db.device_used_in_game.Remove(device_game_pair);
            db.SaveChanges();
            db.Games.Remove(game);
            db.SaveChanges();
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
