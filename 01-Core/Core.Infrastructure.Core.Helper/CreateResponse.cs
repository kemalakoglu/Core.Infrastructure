using System;
using System.Collections.Generic;
using System.Linq;
using Core.Infrastructure.Core.Resources;
using Serilog;
using Serilog.Events;

namespace Core.Infrastructure.Core.Helper
{
    public static class CreateResponse<T> where T : class
    {
        public static ResponseDTO<T> Return(T entity, string methodName)
        {

            string message = string.Empty;
            if (entity != null)
                message = ResponseMessage.GetDescription(ResponseMessage.Success, methodName);
            else
                message = ResponseMessage.GetDescription(ResponseMessage.NotFound, methodName);
            ResponseDTO<T> response = new ResponseDTO<T>
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

        public static ResponseListDTO<T> Return(IEnumerable<T> entity, string methodName)
        {
            string message = string.Empty;
            if (entity.Count() > 0)
                message = ResponseMessage.GetDescription(ResponseMessage.Success, methodName);
            else
                message = ResponseMessage.GetDescription(ResponseMessage.NotFound, methodName);

            ResponseListDTO<T> response = new ResponseListDTO<T>
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
