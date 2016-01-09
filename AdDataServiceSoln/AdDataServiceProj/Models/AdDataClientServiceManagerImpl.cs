using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Compilation;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using wcf = AdDataServiceProj.AdDataClientProxyService;

namespace AdDataServiceProj.Models
{
    public class AdDataClientServiceManagerImpl : IAdDataClientServiceManager
    {
        //TODO: Best practice is to use Channel Factory and not Proxy class
        private readonly wcf.AdDataServiceClient _svc;

        public AdDataClientServiceManagerImpl(wcf.AdDataServiceClient newSvc)
        {
            _svc = newSvc;
        }

        //TODO: Fix Async
        public async Task<List<AdDataClientServiceModel>> GetAdDataByDateRange(DateTime startDate, DateTime endDate)
        {            
                       
                //Try get cached object AdData from cache
                List<AdDataClientServiceModel> adModelList =
                    MemCacheUtil.GetCachedObject("AdData") as List<AdDataClientServiceModel>;

                //if available in cache then return object
                if (adModelList != null) return adModelList;

                //else prepare request to fetch data from service
                var request = new wcf.GetAdDataByDateRangeRequest(startDate, endDate);

                //Get data asynchronously - gO0d!
                var response = await _svc.GetAdDataByDateRangeAsync(request);

                //extract result from response
                var adList = response.GetAdDataByDateRangeResult;

                //no data available return null; dont store in cache either
                if (adList == null || adList.Count <= 0) return null;

                //automapper
                Mapper.CreateMap<wcf.Ad, AdDataClientServiceModel>();
                Mapper.CreateMap<wcf.Brand, AdBrand>();
                adModelList = AutoMapper.Mapper.Map<List<wcf.Ad>, List<AdDataClientServiceModel>>(adList);

                //Cache AdData object
                MemCacheUtil.Add("AdData", adModelList, DateTimeOffset.UtcNow.AddHours(8));

                return adModelList;
        }

        public List<AdDataClientServiceModel> GetAdDataSync(DateTime startDate, DateTime endDate)
        {
            //Try get cached object AdData from cache
            List<AdDataClientServiceModel> adModelList =
                MemCacheUtil.GetCachedObject("AdData") as List<AdDataClientServiceModel>;

            //if available in cache then return object
            if (adModelList != null) return adModelList;

            //else prepare request to fetch data from service
            var request = new wcf.GetAdDataByDateRangeRequest(startDate, endDate);

            //Get data synchronously - nOgO0d!
            var response = _svc.GetAdDataByDateRange(request);

            //extract result from response
            var adList = response.GetAdDataByDateRangeResult;

            //no data available return null; dont store in cache either
            if (adList == null || adList.Count <= 0) return null;

            //automapper
            Mapper.CreateMap<wcf.Ad, AdDataClientServiceModel>();
            Mapper.CreateMap<wcf.Brand, AdBrand>();
            adModelList = AutoMapper.Mapper.Map<List<wcf.Ad>, List<AdDataClientServiceModel>>(adList);

            //Cache AdData object
            MemCacheUtil.Add("AdData", adModelList, DateTimeOffset.UtcNow.AddHours(8));

            return adModelList;
        }
    }
}    