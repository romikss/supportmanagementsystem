using System;
using System.Collections.Generic;

namespace SupportManagementSystem.Models
{
    public class InMemoryEngeneerRepository : IEngeneerRepository
    {
        private static Lazy<List<SupportEngeneer>> _engeneers = new Lazy<List<SupportEngeneer>>(() =>
        {
            return GetEngeneers(10);
        });

        public IEnumerable<SupportEngeneer> GetEngeneers()
        {
            return _engeneers.Value;
        }

        private static List<SupportEngeneer> GetEngeneers(int count)
        {
            var res = new List<SupportEngeneer>();
            for (var i = 1; i <= count; ++i)
            {
                res.Add(new SupportEngeneer
                {
                    Id = Guid.NewGuid(),
                    Name = string.Format("Engeneer_{0}", i)
                });
            }

            return res;
        }
    }
}
