using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AdDataServiceProj.VM
{
    public class AdDataClientServiceViewModel
    {
        public int AdId { get; set; }
        public AdBrandVM Brand { get; set; }        
        public decimal NumPages { get; set; }
        public string Position { get; set; }
        public ExtensionDataObject ExtensionData { get; set; }

    }

    public class AdBrandVM
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public ExtensionDataObject ExtensionData { get; set; }
    }

    public class PagePlusSorter
    {
        public string SortField { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
    }       
}