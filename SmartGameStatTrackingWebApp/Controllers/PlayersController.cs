using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmartGameStatTrackingWebApp.Models;

namespace SmartGameStatTrackingWebApp.Controllers
{
    public class PlayersController : Controller
    {
        private SportsTrackDBContext db = new SportsTrackDBContext();

        // GET: Players
        public async Task<ActionResult> Index(string search)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }

            var playersQuery = from player in db.Players select player;

            if (!String.IsNullOrEmpty(search))
            {
                playersQuery = playersQuery.Where(s => s.name.Contains(search));
            }

            return View(await playersQuery.OrderBy(players => players.name).ToListAsync());
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

            List<int> teamsFollowingIDs = new List<int>();
            List<Player> followingPlayers = new List<Player>();

            for (int i = 0; i < teamsFollowing.Count; i++)
            {
                var tempTeamID = teamsFollowing[i];
                var playersOnTeam = (from players in db.Players
                                    where (players.Team_ID == tempTeamID)
                                    select players).ToList();

                for (int j = 0; j < playersOnTeam.Count; j++)
                {
                    followingPlayers.Add(playersOnTeam[j]);
                }
            }

            //return PartialView("_PartialIndex", db.Games.OrderByDescending(game => game.gameDate).ToList());
            return PartialView("_FollowingPlayers", followingPlayers);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin" && User.Identity.Name != "stephan" && User.Identity.Name != "Marius")
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,number,name,team,gamesPlayed,points,rebounds,assists,blocks,steals,turnovers,personalFouls,technicalFouls,season,Team_ID")] Player player)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin" && User.Identity.Name != "stephan" && User.Identity.Name != "Marius")
            {
                return Redirect("/Home/Index");
            }
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                //Team ID's now being used by Player Model Need to pass correct team ID here for use
                return RedirectToAction("Details", "Teams", new { id = player.Team_ID });
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.Name == "")
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
        public ActionResult Edit([Bind(Include = "id,number,name,team,gamesPlayed,points,rebounds,assists,blocks,steals,turnovers,personalFouls,technicalFouls,season,Team_ID")] Player player)
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin" && User.Identity.Name != "stephan" && User.Identity.Name != "Marius")
            {
                return Redirect("/Home/Index");
            }
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
            if (User.Identity.Name == "")
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
            if (User.Identity.Name == "")
            {
                return Redirect("/Login.aspx");
            }
            else if (User.Identity.Name != "colin" && User.Identity.Name != "stephan" && User.Identity.Name != "Marius")
            {
                return Redirect("/Home/Index");
            }
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
        public ActionResult GetTeamPlayers2(int teamName)
        {
            var playersOnTeam = from players in db.Players.OrderBy(x => x.number)
                                where (players.Team_ID == teamName)
                                select players;

            return Json(playersOnTeam);
        }

        [HttpPost]
        public ActionResult GetPlayers(IEnumerable<SmartGameStatTrackingWebApp.Models.Player> playerModel)
        {
            try
            {
                var playerList = from players in playerModel
                                 select players;

                return Json(playerList);
            }
            catch
            {
                return Json((from player in db.Players select player).OrderBy(player => player.name).ToList());
            }
            
        }

        [HttpPost]
        public ActionResult Search(string SearchString)
        {
            return View("~/Views/Players/Index.cshtml", db.Players.Where(players => players.name.Contains(SearchString)).ToList());
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
