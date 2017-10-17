using System.Collections.Generic;

namespace SupportManagementSystem.Models
{
    public class InMemorySupportSlotRepository : ISupportSlotRepository
    {
        private List<SupportSlot> _slots = new List<SupportSlot>();

        public void AddSupportSlot(SupportSlot slot)
        {
            _slots.Add(slot);
        }

        public IEnumerable<SupportSlot> GetSupportSlots()
        {
            return _slots;
        }
    }
}
