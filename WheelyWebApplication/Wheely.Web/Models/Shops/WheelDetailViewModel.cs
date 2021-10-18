using System.Collections.Generic;

namespace Wheely.Web.Models.Shops
{
    public class WheelDetailViewModel
    {
        public int StarCount { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal CampaignPrice { get; set; }
        public string ProducerName { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }

        public List<WheelColorViewModel> WheelColorViewModels { get; set; }
        public List<WheelDimensionViewModel> WheelDimensionViewModels { get; set; }
    }

    public class WheelColorViewModel
    {
        public int Id { get; set; }
        public string HexCode { get; set; }
    }

    public class WheelDimensionViewModel
    {
        public int Id { get; set; }
        public int Size { get; set; }
    }
}
