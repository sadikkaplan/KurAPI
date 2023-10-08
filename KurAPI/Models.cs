using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurAPI
{
    public class RequestData
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool isToday { get; set; }
    }
    public class ResponseDataCurrency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public decimal BuyingRate { get; set; }
        public decimal SellingRate { get; set; }
        public decimal EffectiveBuyingRate { get; set; }
        public decimal EffectiveSellingRate { get; set; }
    }

    public class ResponseData
    {
        public List<ResponseDataCurrency> Data { get; set; }
        public string Error { get; set; }
    }
}
