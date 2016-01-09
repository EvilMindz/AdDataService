using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AdDataServiceProj.Models
{
    public class AdDataClientServiceModel
    {
        public int AdId { get; set; }
        public AdBrand Brand { get; set; }
        public decimal NumPages { get; set; }
        public string Position { get; set; }
        public ExtensionDataObject ExtensionData { get; set; }
    }


    public class AdBrand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public ExtensionDataObject ExtensionData { get; set; }
    }
}