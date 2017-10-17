using System;

namespace SupportManagementSystem.Models
{
    public class SupportSlot
    {
        public Guid Id { get; set; }

        public SupportEngineer Engeneer { get; set; }

        public DateTime Date { get; set; }
    }
}
