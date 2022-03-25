using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Dal.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Entity = TaxCalculator.Entities;

namespace TaxCalculator.Dal.Repositories
{
    public class DalProcessor
    {
        private readonly TaxCalculatorDBContext _context;
        public DalProcessor(TaxCalculatorDBContext context)
        {
            _context = context;
        }

        public async Task<List<Entity.Municipalities>> GetMunicipalityTaxDetails(string municipalityName, DateTime date)
        {
            List<Entity.Municipalities> lstMunicipalities = null;
            // Create a SQL command to execute the sproc
            var cmd = _context.Database.GetDbConnection().CreateCommand();

            try
            {                
                cmd.CommandText = "[dbo].[GetMunicipalityTaxDetails]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@MunicipalityName";
                paramId.Value = municipalityName;
                cmd.Parameters.Add(paramId);

                SqlParameter paramDate = new SqlParameter();
                paramDate.ParameterName = "@Date";
                paramDate.Value = date;
                cmd.Parameters.Add(paramDate);
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                // Run the sproc
                var result = cmd.ExecuteReader();
                lstMunicipalities = new List<Entity.Municipalities>();
                while (await result.ReadAsync())
                {
                    lstMunicipalities.Add(new Entity.Municipalities
                    {
                        MunicipalityId = Convert.ToInt16(result["MunicipalityId"]),
                        MunicipalityName = Convert.ToString(result["MunicipalityName"]),
                        TaxRule = Convert.ToInt16(result["TaxRule"]),
                        Order = Convert.ToInt16(result["Order"]),
                        Date = Convert.ToDateTime(result["Date"]),
                        Tax = Convert.ToDouble(result["Tax"])
                    });
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return lstMunicipalities;
        }

        public async Task<bool> CheckIfExists(string municipalityName)
        {
            try
            {
                var output = await _context.Municipalities.Where(x => x.MunicipalityName.Equals(municipalityName)).FirstOrDefaultAsync();
                return (output == null) ? false : true;
            }
            catch { throw; }
        }

        public async Task<Entity.Municipalities> GetDetails(string municipalityName, string taxRuleId)
        {
            Entity.Municipalities municipalities = null;
            // Create a SQL command to execute the sproc
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            try
            {
                cmd.CommandText = "[dbo].[GetMunicipalityDetails]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@MunicipalityName";
                paramId.Value = municipalityName;
                cmd.Parameters.Add(paramId);
                SqlParameter paramTaxTypeId = new SqlParameter();
                paramTaxTypeId.ParameterName = "@TaxRuleId";
                paramTaxTypeId.Value = taxRuleId;
                cmd.Parameters.Add(paramTaxTypeId);
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                // Run the sproc
                var result = cmd.ExecuteReader();
                while (await result.ReadAsync())
                {
                    municipalities = new Entity.Municipalities
                    {
                        MunicipalityId = Convert.ToInt16(result["MunicipalityId"]),
                        MunicipalityName = Convert.ToString(result["MunicipalityName"]),
                        TaxRuleId = Convert.ToInt16(result["TaxRuleId"]),
                        TaxRule = Convert.ToInt16(result["TaxRule"])
                    };
                }
            }
            catch { throw; }
            finally
            {
                cmd.Connection.Close();
            }
            return municipalities;
        }

        public async Task<Entity.Lookup> GetLookup()
        {
            try
            {
                Entity.Lookup lookup = new Entity.Lookup();
                lookup.TaxRules = await _context.TaxRules.Select(x => new Entity.Lookup.TaxRule { TaxRuleId = x.TaxRuleId, TaxRuleName = x.TaxRule }).ToListAsync();
                lookup.TaxTypes = await _context.TaxTypes.Select(x => new Entity.Lookup.TaxType { TaxTypeId = x.TaxTypeId, TaxTypeName = x.TaxType }).ToListAsync();
                return lookup;
            }
            catch { throw; }
        }

        public async Task<bool> UpdateDetails(Entity.Municipalities data)
        {
            try
            {
                var record = _context.Municipalities.Find(data.MunicipalityId);
                if (record != null)
                {
                    record.TaxRuleId = data.TaxRuleId;
                    record.UpdatedOn = DateTime.Now;

                    _context.Municipalities.Update(record);
                    await _context.SaveChangesAsync();
                }
            }
            catch { throw; }
            return false;
        }

        public async Task<bool> AddDetails(Entity.Municipalities data)
        {
            try
            {
                Municipalities municipalities = new Municipalities
                {
                    MunicipalityName = data.MunicipalityName,
                    TaxRuleId = data.TaxRuleId
                };
                _context.Municipalities.Add(municipalities);
                await _context.SaveChangesAsync();

                TaxPeriod taxPeriod = new TaxPeriod { 
                    MunicipalityId = municipalities.MunicipalityId,
                    From = data.From,
                    To = data.To,
                    Tax = data.Tax,
                    TaxTypeId = data.TaxTypeId
                };
                _context.TaxPeriod.Add(taxPeriod);
                await _context.SaveChangesAsync();

                return true;
            }
            catch { throw; }
        }
    }
}
