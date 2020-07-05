using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Core.Infrastructure.Core.Dapper
{
    public static class Execute<T> where T : class
    {
        public static IEnumerable<T> ExecuteCommand(string query, IDictionary<string, string> parameters, string cnString, string trackId)
        {
            List<T> response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameterList = new DynamicParameters();
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    parameterList.Add(entry.Key, entry.Value);
                }

                response = con.Query<T>(query, parameterList).ToList();
            }

            return response;
        }

        public static IEnumerable<T> ExecuteCommand(string query, string cnString, string trackId)
        {
            List<T> response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                response = con.Query<T>(query).ToList();
            }

            return response;
        }

        public static IEnumerable<T> ExecuteStoredProcedure(string storedProcedure, string cnString, string trackId)
        {
            List<T> response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                response = con.Query<T>(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return response;
        }

        public static IEnumerable<T> ExecuteStoredProcedure(string storedProcedure, IDictionary<string, string> parameters, string cnString, string trackId)
        {
            List<T> response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameterList = new DynamicParameters();
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    parameterList.Add(entry.Key, entry.Value);
                }

                response = con.Query<T>(storedProcedure, parameterList, commandType: CommandType.StoredProcedure).ToList();
            }

            return response;
        }
    }
}
