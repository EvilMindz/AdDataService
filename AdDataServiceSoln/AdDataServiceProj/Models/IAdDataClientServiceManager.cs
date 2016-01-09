using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace AdDataServiceProj.Models
{
    public interface IAdDataClientServiceManager
    {
        Task<List<AdDataClientServiceModel>> GetAdDataByDateRange(DateTime startDate, DateTime endDate);
        List<AdDataClientServiceModel> GetAdDataSync(DateTime startTime, DateTime endTime);
    }
}
