using System;
using System.Threading.Tasks;

namespace SupportManagementSystem.Client
{
    public interface ISupportManagementSystemClient
    {
        Task<DayResult> GetDayAsync(DateTime date);
    }
}
