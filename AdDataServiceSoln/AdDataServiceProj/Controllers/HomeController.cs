using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AdDataServiceProj.AdDataClientProxyService;
using AutoMapper;
using AdDataServiceProj.Models;
using AdDataServiceProj.VM;
using Ninject.Infrastructure.Language;
using PagedList;

namespace AdDataServiceProj.Controllers
{
    public class HomeController : Controller
    {
        private IAdDataClientServiceManager _adManager;

        public HomeController(IAdDataClientServiceManager adMgr)
        {
            _adManager = adMgr;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }        

        public async Task<ActionResult> View(string sortOrder, int page = 1, int pageSize = 15, int id = 1)
        {

            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 15;

            if (id == 1)
            {
                if (sortOrder == null || sortOrder == "nosort")
                {
                    sortOrder = "BrandName";
                }

                ViewBag.AdIdSortParam = sortOrder == "AdId" ? "AdId_desc" : "AdId";
                ViewBag.BrandIdSortParam = sortOrder == "BrandId" ? "BrandId_desc" : "BrandId";
                ViewBag.BrandNameSortParam = sortOrder == "BrandName" ? "BrandName_desc" : "BrandName";
                ViewBag.NumPagesSortParam = sortOrder == "NumPages" ? "NumPages_desc" : "NumPages";
                ViewBag.PositionSortParam = sortOrder == "Position" ? "Position_desc" : "Position";
            }
            else
            {
                sortOrder = "nosort";
            }

            ViewBag.Title = "View " + Convert.ToString(id);            
            ViewBag.CurrentSort = sortOrder;

            List<AdDataClientServiceModel> val = await _adManager.GetAdDataByDateRange(Convert.ToDateTime("2011-01-01"), Convert.ToDateTime("2011-04-01"));
            Mapper.CreateMap<AdDataClientServiceModel, AdDataClientServiceViewModel>();
            Mapper.CreateMap<AdBrand, AdBrandVM>();
            var temp = AutoMapper.Mapper.Map<List<AdDataClientServiceModel>, List<AdDataClientServiceViewModel>>(val);
            
                        
            List<AdDataClientServiceViewModel> data = new List<AdDataClientServiceViewModel>();

            switch (id)
            {
                case 1:
                    data = temp;               
                    break;
                case 2:
                    //List of ads which appeared in the Cover position and also had at least 50% page coverage
                    data = temp.Where(a => a.Position.ToUpper() == "COVER" && a.NumPages >= (decimal)0.5).OrderBy(a => a.Brand.BrandName).ToList<AdDataClientServiceViewModel>();
                    break;
                case 3:
                    //Top five ads having max page coverage amount for every distinct brand
                    var group = temp.GroupBy(x => x.Brand.BrandId);

                    foreach (var g in group)
                    {
                        data.AddRange(g.AsEnumerable().OrderByDescending(a => a.NumPages).Take(5));
                    }

                    data = data.OrderBy(b => b.Brand.BrandName).ToList<AdDataClientServiceViewModel>();

                    break;
                case 4:
                    //Top five ads by max page coverage amount. Same brand could appear more than once for different ads. Sort by num page desc and then brand name asc
                    data = temp.OrderByDescending(a => a.NumPages).ThenBy(b =>b.Brand.BrandName).Take(5).ToList<AdDataClientServiceViewModel>();
                    break;
            }



            switch (sortOrder)
            {
                case "AdId":
                    data = data.OrderBy(a => a.AdId).ToList();
                    break;
                case "AdId_desc":
                    data = data.OrderByDescending(a => a.AdId).ToList();
                    break;
                case "BrandId":
                    data = data.OrderBy(a => a.Brand.BrandId).ToList();
                    break;
                case "BrandId_desc":
                    data = data.OrderByDescending(a => a.Brand.BrandId).ToList();
                    break;
                case "BrandName":
                    data = data.OrderBy(a => a.Brand.BrandName).ToList();
                    break;
                case "BrandName_desc":
                    data = data.OrderByDescending(a => a.Brand.BrandName).ToList();
                    break;
                case "NumPages":
                    data = data.OrderBy(a => a.NumPages).ToList();
                    break;
                case "NumPages_desc":
                    data = data.OrderByDescending(a => a.NumPages).ToList();
                    break;
                case "Position":
                    data = data.OrderBy(a => a.Position).ToList();
                    break;
                case "Position_desc":
                    data = data.OrderByDescending(a => a.Position).ToList();
                    break;
                case "nosort":
                    break;
            }

            var model = data.ToPagedList(page, pageSize);

            return View("View", model);
        }
    }
}