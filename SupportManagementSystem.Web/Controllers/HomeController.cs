using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SupportManagementSystem.Client;
using SupportManagementSystem.Web.Models;
using System;
using System.Threading.Tasks;

namespace SupportManagementSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public static DateTime Today = DateTime.UtcNow.AddDays(-1); //need this field for emulation

        private readonly ISupportManagementSystemClient _apiClient;
        private readonly IConfiguration _configuration;

        public HomeController(ISupportManagementSystemClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            // just emulation (increase "today" every time we update the page)
            Today = Today.AddDays(1);

            var supportScheduleModel = new SupportScheduleModel();
            var itemsToDisplay = int.Parse(_configuration.GetSection("pageSize:main").Value);

            var startDate = Today.AddDays(-1 * (itemsToDisplay * 0.25));
            for (var i = 0; i < itemsToDisplay; ++i)
            {
                var supportDay = await _apiClient.GetDayAsync(startDate);
                supportScheduleModel.SupportDays.Add(new SupportDayModel
                {
                    FirstEmployee = supportDay.Engeneers?[0],
                    SecondEmployee = supportDay.Engeneers?[1],
                    Date = startDate
                });

                startDate = startDate.AddDays(1);
            }

            return View(supportScheduleModel);
        }

        public IActionResult Reset()
        {
            Today = DateTime.UtcNow.AddDays(-1);

            return RedirectToAction("Index");
        }
    }
}
