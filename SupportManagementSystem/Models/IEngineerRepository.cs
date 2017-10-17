using System.Collections.Generic;

namespace SupportManagementSystem.Models
{
    public interface IEngineerRepository
    {
        IEnumerable<SupportEngineer> GetEngeneers();
    }
}
