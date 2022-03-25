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
        public static async Task<Municipalities> GetTaxForMunicipality(string url, Municipalities model)
        {
            Municipalities result = null;
            try
            {                
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

        public static async Task<Municipalities> GetDetails(string url, string name, string taxRuleId)
        {
            try
            {
                using (var client = new WebApiClient())
                {
                    return await client.GetJsonWithModelAsync<Municipalities>(String.Join("/", new string[] { url, "getdetails", name, taxRuleId }));
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<Lookup> GetLookup(string url)
        {
            try
            {
                using (var client = new WebApiClient())
                {
                    return await client.GetJsonWithModelAsync<Lookup>(String.Join("/", new string[] { url, "getlookup" }));
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> AddDetails(string url, Municipalities model)
        {
            try
            {
                using (var client = new WebApiClient())
                {
                    var serializeModel = JsonConvert.SerializeObject(model);
                    return await client.PostJsonWithModelAsync<bool>(String.Join("/", new string[] { url, "adddetails" }), serializeModel);
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> UpdateDetails(string url, Municipalities model)
        {
            try
            {
                using (var client = new WebApiClient())
                {
                    var serializeModel = JsonConvert.SerializeObject(model);
                    return await client.PostJsonWithModelAsync<bool>(String.Join("/", new string[] { url, "updatedetails" }), serializeModel);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
