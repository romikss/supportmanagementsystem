using System.Collections.Generic;

namespace SupportManagementSystem.Web.Models
{
    public class SupportScheduleModel
    {
        public SupportScheduleModel()
        {
            SupportDays = new List<SupportDayModel>();
        }

        public List<SupportDayModel> SupportDays { get; set; }
    }
}
