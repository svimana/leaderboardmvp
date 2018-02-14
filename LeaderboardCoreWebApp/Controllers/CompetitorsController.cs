using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaderboardCoreWebApp.Data;
using LeaderboardCoreWebApp.Models;
using LeaderboardCoreWebApp.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace LeaderboardCoreWebApp.Controllers
{
    public class CompetitorsController : Controller
    {
        private readonly ILogger<CompetitorsController> logger;
        private readonly ILeaderboardService leaderboardService;

        public CompetitorsController(ILeaderboardService leaderboardService, ILogger<CompetitorsController> logger)
        {
            this.leaderboardService = leaderboardService;
            this.logger = logger;
        }

        // GET: Competitors
        public async Task<IActionResult> Index()
        {
            return View(await this.leaderboardService.GetAllCompetitors());
        }

        // GET: Competitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitor = await this.leaderboardService.FindCompetitor((int)id);
            if (competitor == null)
            {
                return NotFound();
            }

            return View(competitor);
        }

        // GET: Competitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Competitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Score,LeaderBoardId")] Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                await this.leaderboardService.AddCompetitor(competitor);
                return RedirectToAction(nameof(Index));
            }
            return View(competitor);
        }

        // GET: Competitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitor = await this.leaderboardService.FindCompetitor((int)id);
            if (competitor == null)
            {
                return NotFound();
            }
            return View(competitor);
        }

        // POST: Competitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Score,LeaderBoardId")] Competitor competitor)
        {
            if (id != competitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.leaderboardService.UpdateCompetitor(competitor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitorExists(competitor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(competitor);
        }

        // GET: Competitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitor = await this.leaderboardService.FindCompetitor((int)id);
            if (competitor == null)
            {
                return NotFound();
            }

            return View(competitor);
        }

        // POST: Competitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.leaderboardService.DeleteCompetitor(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitorExists(int id)
        {
            return this.leaderboardService.HasCompetitor(id).Result;
        }
    }
}
