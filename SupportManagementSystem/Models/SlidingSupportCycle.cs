using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportManagementSystem.Models
{
    public class SlidingSupportCycle : ISupportCycle
    {
        private int _cycle;
        private int _slotsInADay;
        private ISupportSlotRepository _supportSlotRepository;
        private Lazy<List<SupportEngeneer>> _supportEngeneers;
        private DateTime _curentDate;

        public SlidingSupportCycle(int cycle, int slotsInADay, ISupportSlotRepository supportSlotRepository, IEngeneerRepository engeneerRepository)
        {
            _cycle = cycle;
            _supportSlotRepository = supportSlotRepository;
            _supportEngeneers = new Lazy<List<SupportEngeneer>>(() =>
            {
                return engeneerRepository.GetEngeneers().ToList();
            });

            _slotsInADay = slotsInADay;

            var lastScheduledSupportDay = _supportSlotRepository.GetLastSupportDay();
            if (lastScheduledSupportDay == null)
            {
                _curentDate = DateTime.UtcNow.Date;
            }
            else
            {
                _curentDate = lastScheduledSupportDay.Date.Date;
            }
        }

        public SupportDay GenerateNewDay()
        {
            var day = _supportSlotRepository.GetSupportDay(_curentDate);
            if (day != null)
            {
                _curentDate = _curentDate.AddDays(1);
                return day;
            }

            var supportDay = new SupportDay(_slotsInADay, _curentDate);
            Random rand = new Random();

            var engeneersToSelectFrom = GetEngeneersToSelectFrom();

            while (!supportDay.IsFilled)
            {
                if (engeneersToSelectFrom.Count == 0)
                {
                    engeneersToSelectFrom = GetEngeneersToSelectFrom();
                }

                var nextEngeneerIndex = rand.Next(0, engeneersToSelectFrom.Count);

                supportDay.AddSlot(engeneersToSelectFrom[nextEngeneerIndex]);
                engeneersToSelectFrom.Remove(engeneersToSelectFrom[nextEngeneerIndex]);
            }

            // persist support day
            _supportSlotRepository.AddSupportDay(supportDay);

            _curentDate = _curentDate.AddDays(1);

            return supportDay;
        }

        private List<SupportEngeneer> GetEngeneersToSelectFrom()
        {
            // Key: Support Engeneer, Value: occurance count
            var engeneers = _supportEngeneers.Value.ToDictionary(x => x, x => 0);
            var scanStartDate = _curentDate.AddDays(-1 * _cycle);
            var currentScanDate = scanStartDate;

            while (!currentScanDate.Date.Equals(_curentDate.Date))
            {
                // place for performance improvement on real system: get all days (slots) at one go
                var supportDay = _supportSlotRepository.GetSupportDay(currentScanDate.Date);
                if (supportDay != null)
                {
                    foreach (var slot in supportDay.Slots)
                    {
                        engeneers[slot.Engeneer]++;
                    }
                }

                currentScanDate = currentScanDate.AddDays(1);
            }

            var minTimesWorked = engeneers.Values.Min();

            var restrictedEngeneers = GetEngeneersThatCanNotBeAssignedOnCurrentDay();
            var engeneersWhoWorkedLessThanOthers = engeneers.Where(x => x.Value == minTimesWorked).Select(x => x.Key).ToList();

            return engeneersWhoWorkedLessThanOthers.Except(restrictedEngeneers).ToList();
        }

        private List<SupportEngeneer> GetEngeneersThatCanNotBeAssignedOnCurrentDay()
        {
            var prevSupportDay = _supportSlotRepository.GetSupportDay(_curentDate.AddDays(-1));
            if (prevSupportDay == null)
            {
                return new List<SupportEngeneer>();
            }

            return prevSupportDay.Slots.Select(x => x.Engeneer).ToList();
        }
    }
}
