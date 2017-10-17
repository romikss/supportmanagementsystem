using System;
using System.Linq;

namespace SupportManagementSystem.Models
{
    public static class SupportRepositoryExtensions
    {
        public static SupportDay GetLastSupportDay(this ISupportSlotRepository supportSlotRepository)
        {
            if (!supportSlotRepository.GetSupportSlots().Any())
            {
                return null;
            }

            var lastSlotDateTime = supportSlotRepository.GetSupportSlots().Max(x => x.Date);
            var lastDaySlots = supportSlotRepository.GetSupportSlots().Where(x => x.Date.Date.Equals(lastSlotDateTime.Date));
            if (!lastDaySlots.Any())
            {
                return null;
            }

            var lastSupportDay = new SupportDay(lastDaySlots);
            return lastSupportDay;
        }

        public static SupportDay GetSupportDay(this ISupportSlotRepository supportSlotRepository, DateTime date)
        {
            var slots = supportSlotRepository.GetSupportSlots().Where(x => x.Date.Date.Equals(date.Date));
            if (!slots.Any())
            {
                return null;
            }

            return new SupportDay(slots);
        }
        public static void AddSupportDay(this ISupportSlotRepository supportSlotRepository, SupportDay supportDay)
        {
            foreach (var slot in supportDay.Slots)
            {
                supportSlotRepository.AddSupportSlot(slot);
            }
        }
    }
}
