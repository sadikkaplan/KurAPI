using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace KurAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyCurrencyController : ControllerBase
    {
        [HttpPost]
        public ResponseData Run(RequestData data)
        {
            ResponseData result = new ResponseData();

            try
            {
                string apiAdress = string.Format("http://www.tcmb.gov.tr/kurlar/{0}.xml", (data.isToday) ? "today" :
                    string.Format("{2}{1}/{0}{1}{2}", data.Day.ToString().PadLeft(2, '0'), data.Month.ToString().PadLeft(2, '0'), data.Year));

                result.Data = new List<ResponseDataCurrency>();

                XmlDocument doc = new XmlDocument();
                doc.Load(apiAdress);

                if (doc.SelectNodes("Tarih_Date").Count < 1)
                {
                    result.Error = "Kur Bilgisi Okunamadi";
                    return result;
                }

                foreach (XmlNode node in doc.SelectNodes("Tarih_Date")[0].ChildNodes)
                {
                    ResponseDataCurrency currecy = new ResponseDataCurrency();
                    
                    currecy.Code = node.Attributes["Kod"].Value;
                    currecy.Name = node["Isim"].InnerText;
                    currecy.Unit = Int32.Parse(node["Unit"].InnerText);
                    currecy.BuyingRate = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".", ","));
                    currecy.SellingRate = Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".", ","));
                    currecy.EffectiveBuyingRate = Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".", ","));
                    currecy.EffectiveSellingRate = Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));

                    result.Data.Add(currecy);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }




            return result;
        }
    }
}
