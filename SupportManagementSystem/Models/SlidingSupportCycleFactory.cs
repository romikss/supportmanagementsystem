﻿namespace SupportManagementSystem.Models
{
    public class SlidingSupportCycleFactory : ISupportCycleFactory
    {
        private int _cycle;
        private int _slotsInADay;
        private ISupportSlotRepository _supportSlotRepository;
        private IEngineerRepository _engeneerRepository;

        public SlidingSupportCycleFactory(int cycle, int slotsInADay, ISupportSlotRepository supportSlotRepository, IEngineerRepository engeneerRepository)
        {
            _cycle = cycle;
            _slotsInADay = slotsInADay;
            _supportSlotRepository = supportSlotRepository;
            _engeneerRepository = engeneerRepository;
        }

        public ISupportCycle GetSupportCycle()
        {
            return new SlidingSupportCycle(_cycle, _slotsInADay, _supportSlotRepository, _engeneerRepository);
        }
    }
}
