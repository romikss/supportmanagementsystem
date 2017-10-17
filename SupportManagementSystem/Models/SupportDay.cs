using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportManagementSystem.Models
{
    public class SupportDay
    {
        private int _slotsCount;
        private List<SupportSlot> _slots;
        private DateTime _date;

        public SupportDay(int slotsCount, DateTime date)
        {
            _slotsCount = slotsCount;
            _slots = new List<SupportSlot>();
            _date = date;
        }

        public SupportDay(IEnumerable<SupportSlot> slots)
        {
            _slots = slots.ToList();
            _slotsCount = _slots.Count;
        }

        public Guid Id { get; set; }

        public List<SupportSlot> Slots
        {
            get
            {
                return _slots;
            }
        }

        public DateTime Date
        {
            get
            {
                if (_slots.Any())
                {
                    return _slots[0].Date;
                }

                return DateTime.MinValue;
            }
        }

        public SupportSlot AddSlot(SupportEngineer engeneer)
        {
            if (IsFilled)
            {
                throw new InvalidOperationException();
            }

            var supportSlot = new SupportSlot
            {
                Engeneer = engeneer,
                Date = _date
            };

            _slots.Add(supportSlot);
            return supportSlot;
        }

        public bool IsFilled
        {
            get
            {
                return _slots.Count == _slotsCount;
            }
        }
    }
}
