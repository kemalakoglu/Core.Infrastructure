using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Core.Resources;
using Serilog;
using Serilog.Events;

namespace Core.Infrastructure.Core.Helper
{
    public static class CreateResponse<T> where T : class
    {
        public static ResponseDTO Return(T entity, string methodName)
        {

            string message = string.Empty;
            if (entity != null)
                message = ResponseMessage.GetDescription(ResponseMessage.Success, methodName);
            else
                message = ResponseMessage.GetDescription(ResponseMessage.NotFound, methodName);
            ResponseDTO response = new ResponseDTO
            {
                Data = entity,
                Message = message,
                Information = new Information
                {
                    TrackId = Guid.NewGuid().ToString()
                },
                RC = entity==null?ResponseMessage.NotFound:ResponseMessage.Success
            };
            Log.Write(LogEventLevel.Information, message, response);
            return response;
        }

        public static ResponseDTO Return(IEnumerable<T> entity, string methodName)
        {
            string message = string.Empty;
            if (entity.Count() > 0)
                message = ResponseMessage.GetDescription(ResponseMessage.Success, methodName);
            else
                message = ResponseMessage.GetDescription(ResponseMessage.NotFound, methodName);

            ResponseDTO response = new ResponseDTO
            {
                Data = entity,
                Message = message,
                Information = new Information
                {
                    TrackId = Guid.NewGuid().ToString()
                },
                RC = entity == null ? ResponseMessage.NotFound : ResponseMessage.Success
            };
            Log.Write(LogEventLevel.Information, message, response);
            return response;
        }
    }
}
