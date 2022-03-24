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
using Municipalities = TaxCalculator.Entities.Municipalities;

namespace TaxCalculator.Dal.Repositories
{
    public class DalProcessor
    {
        private readonly TaxCalculatorDBContext _context;
        public DalProcessor(TaxCalculatorDBContext context)
        {
            _context = context;
        }

        public async Task<List<Municipalities>> GetMunicipalityTaxDetails(int municipalityId, DateTime date)
        {
            List<Municipalities> lstMunicipalities = null;

            // Create a SQL command to execute the sproc
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "[dbo].[GetMunicipalityTaxDetails]";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@MunicipalityId";
            paramId.Value = municipalityId;
            cmd.Parameters.Add(paramId);

            SqlParameter paramDate = new SqlParameter();
            paramDate.ParameterName = "@Date";
            paramDate.Value = date;
            cmd.Parameters.Add(paramDate);

            try
            {
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                // Run the sproc
                var result = cmd.ExecuteReader();
                lstMunicipalities = new List<Municipalities>();
                while (await result.ReadAsync())
                {
                    lstMunicipalities.Add(new Municipalities
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
            bool result;
            try
            {
                var output = await _context.Municipalities.Where(x => x.MunicipalityName.Equals(municipalityName)).FirstOrDefaultAsync();
                result = (output == null) ? false : true;
            }
            catch { throw; }
            return result;
        }
    }
}
