using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Core.Resources;
using Dapper;

namespace Core.Infrastructure.Core.Dapper
{
    public static class Execute
    {
        public static ResponseDTO ExecuteCommand(string query, IDictionary<string, string> parameters, string cnString, string trackId)
        {
            object response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameterList = new DynamicParameters();
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    parameterList.Add(entry.Key, entry.Value);
                }

                response = con.Query<object>(query, parameterList).ToList();
            }

            return new ResponseDTO
            {
                Data = response,
                Information = new Information
                {
                    TrackId = trackId
                },
                Message = ResponseMessage.GetDescription(ResponseMessage.Success, "Sql Command is succeeded."),
                RC = ResponseMessage.Success
            };
        }

        public static ResponseDTO ExecuteCommand(string query, string cnString, string trackId)
        {
            object response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                response = con.Query<object>(query).ToList();
            }

            return new ResponseDTO
            {
                Data = response,
                Information = new Information
                {
                    TrackId = trackId
                },
                Message = ResponseMessage.GetDescription(ResponseMessage.Success, "Sql Command is succeeded."),
                RC = ResponseMessage.Success
            };
        }

        public static ResponseDTO ExecuteStoredProcedure(string storedProcedure, string cnString, string trackId)
        {
            object response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                response = con.Query<object>(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return new ResponseDTO
            {
                Data = response,
                Information = new Information
                {
                    TrackId = trackId
                },
                Message = ResponseMessage.GetDescription(ResponseMessage.Success, "Sql Command is succeeded."),
                RC = ResponseMessage.Success
            };
        }

        public static ResponseDTO ExecuteStoredProcedure(string storedProcedure, IDictionary<string, string> parameters, string cnString, string trackId)
        {
            object response;
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameterList = new DynamicParameters();
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    parameterList.Add(entry.Key, entry.Value);
                }

                response = con.Query<object>(storedProcedure, parameterList, commandType: CommandType.StoredProcedure).ToList();
            }

            return new ResponseDTO
            {
                Data = response,
                Information = new Information
                {
                    TrackId = trackId
                },
                Message = ResponseMessage.GetDescription(ResponseMessage.Success, "Sql Command is succeeded."),
                RC = ResponseMessage.Success
            };
        }
    }
}
