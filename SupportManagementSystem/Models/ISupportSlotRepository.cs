using System.Collections.Generic;

namespace SupportManagementSystem.Models
{
    public interface ISupportSlotRepository
    {
        IEnumerable<SupportSlot> GetSupportSlots();
        void AddSupportSlot(SupportSlot slot);
    }
}
