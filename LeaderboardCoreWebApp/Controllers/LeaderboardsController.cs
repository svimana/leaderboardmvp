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
    public class LeaderboardsController : Controller
    {
        private readonly ILogger<LeaderboardsController> logger;
        private readonly ILeaderboardService leaderboardService;

        public LeaderboardsController(ILeaderboardService leaderboardService, ILogger<LeaderboardsController> logger)
        {
            this.leaderboardService = leaderboardService;
            this.logger = logger;
        }

        // GET: Leaderboards
        public async Task<IActionResult> Index()
        {
            var lb = await this.leaderboardService.FindLeaderboardByName("Default");
            await this.leaderboardService.StartRoundRobin();
            return View(lb);
        }
    }
}
