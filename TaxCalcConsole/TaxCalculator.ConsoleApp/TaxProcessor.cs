using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.ConsoleApp.Utilities;
using TaxCalculator.Entities;

namespace TaxCalculator.ConsoleApp
{
    public static class TaxProcessor
    {
        public static async Task<Municipalities> GetTaxForMunicipality(string url, string name, DateTime date)
        {
            Municipalities result = null;
            try
            {
                Municipalities model = new Municipalities
                {
                    MunicipalityId = 1,
                    Date = date
                };
                using (var client = new WebApiClient())
                {
                    var serializeModel = JsonConvert.SerializeObject(model);
                    result = await client.PostJsonWithModelAsync<Municipalities>(String.Join("/", new string[] { url, "gettax" }), serializeModel);
                }
            }
            catch 
            {
                throw;
            }
            return result;
        }

        public static async Task<bool> CheckIfExists(string url, string name)
        {
            try
            {
                using (var client = new WebApiClient())
                {
                    return await client.GetJsonWithModelAsync<bool>(String.Join("/", new string[] { url, "validate", name }));
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
