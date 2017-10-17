using System.Collections.Generic;

namespace SupportManagementSystem.Models
{
    public interface IEngeneerRepository
    {
        IEnumerable<SupportEngeneer> GetEngeneers();
    }
}
