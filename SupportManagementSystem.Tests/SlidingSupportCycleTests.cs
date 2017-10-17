using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportManagementSystem.Models;
using System;
using System.Linq;

namespace SupportManagementSystem.Tests
{
    [TestClass]
    public class SlidingSupportCycleTests
    {
        [TestMethod]
        public void AddOneDayTest()
        {
            var supportRepository = new InMemorySupportSlotRepository();
            var engeneerRepository = new InMemoryEngeneerRepository();
            var slidingSupportCycle = new SlidingSupportCycle(14, 2, supportRepository, engeneerRepository);

            var day = slidingSupportCycle.GenerateNewDay();
            Console.Write(string.Join(" | ", day.Slots.Select(x => x.Engeneer.Name)));
            Console.WriteLine();

            Assert.IsTrue(day.Slots.Any());
        }

        [TestMethod]
        public void AddManyDaysTest()
        {
            const int DaysNumber = 20;
            var supportRepository = new InMemorySupportSlotRepository();
            var engeneerRepository = new InMemoryEngeneerRepository();
            var slidingSupportCycle = new SlidingSupportCycle(14, 2, supportRepository, engeneerRepository);

            for (var i = 0; i < DaysNumber; ++i)
            {
                var day = slidingSupportCycle.GenerateNewDay();
                Console.Write(string.Join(" | ", day.Slots.Select(x => x.Engeneer.Name)));
                Console.WriteLine();

                Assert.IsTrue(day.Slots.Any());
            }
        }
    }
}
