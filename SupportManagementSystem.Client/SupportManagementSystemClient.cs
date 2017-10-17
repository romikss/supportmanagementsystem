using System;
using System.Threading.Tasks;

namespace SupportManagementSystem.Client
{
    public class SupportManagementSystemClient : ClientBase, ISupportManagementSystemClient
    {
        private const string GetDayUrlTemplate = "api/supportday?date={0}";

        public SupportManagementSystemClient(Uri uri) : base(uri)
        {
        }

        public async Task<DayResult> GetDayAsync(DateTime date)
        {
            return await GetEntityAsync<DayResult>(string.Format(GetDayUrlTemplate, date));
        }
    }
}
