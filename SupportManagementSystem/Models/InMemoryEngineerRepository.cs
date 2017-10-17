using System;
using System.Collections.Generic;

namespace SupportManagementSystem.Models
{
    public class InMemoryEngineerRepository : IEngineerRepository
    {
        private static Lazy<List<SupportEngineer>> _engeneers = new Lazy<List<SupportEngineer>>(() =>
        {
            return GetEngeneers(10);
        });

        public IEnumerable<SupportEngineer> GetEngeneers()
        {
            return _engeneers.Value;
        }

        private static List<SupportEngineer> GetEngeneers(int count)
        {
            var res = new List<SupportEngineer>();
            for (var i = 1; i <= count; ++i)
            {
                res.Add(new SupportEngineer
                {
                    Id = Guid.NewGuid(),
                    Name = string.Format("Engineer_{0}", i)
                });
            }

            return res;
        }
    }
}
