using Microsoft.AspNetCore.Mvc;
using SupportManagementSystem.Models;
using System;
using System.Linq;

namespace SupportManagementSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/supportday")]
    public class SupportDayController : Controller
    {
        private ISupportSlotRepository _supportSlotRepository;
        private IEngineerRepository _engeneerRepository;
        private ISupportCycleFactory _supportCycleFactory;

        public SupportDayController(ISupportSlotRepository supportSlotRepository, IEngineerRepository engeneerRepository, ISupportCycleFactory supportCycleFactory)
        {
            _supportSlotRepository = supportSlotRepository;
            _engeneerRepository = engeneerRepository;
            _supportCycleFactory = supportCycleFactory;
        }
        
        // GET api/supportday
        [HttpGet]
        public DayResult Get(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                date = DateTime.UtcNow.Date;
            }

            var day = _supportSlotRepository.GetSupportDay(date.Date);
            if (day == null)
            {
                if (date.Date.Date < DateTime.UtcNow.Date)
                {
                    return new DayResult
                    {
                        Date = date.Date
                    };
                }

                var currentDate = date.Date;
                var lastScheduledSupportDay = _supportSlotRepository.GetLastSupportDay();
                var slidingSupportCycle = _supportCycleFactory.GetSupportCycle();
                while (day == null || day.Date.Date != date.Date)
                {
                    day = slidingSupportCycle.GenerateNewDay();
                }
            }

            var res = new DayResult
            {
                Date = date.Date,
                Engeneers = day.Slots.Select(x => x.Engeneer.Name).ToList()
            };

            return res;
        }
    }
}
