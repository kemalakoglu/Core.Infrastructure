using System;
using Core.Infrastructure.Core.Resources;

namespace Core.Infrastructure.Presentation.API.Extensions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string message)
        {
            RC = message;
        }

        public BusinessException(string message, string param1)
        {
            RC = message;
            this.param1 = param1;
        }

        public BusinessException(string message, string param1, string param2)
        {
            RC = message;
            this.param1 = param1;
            this.param2 = param2;
        }

        public string param1 { get; set; }

        public string param2 { get; set; }

        public string RC { get; set; }

        public static string GetDescription(string RC)
        {
            return ResponseMessage.GetDescription(RC);
        }

        public static string GetDescription(string RC, string param1)
        {
            return ResponseMessage.GetDescription(RC, param1);
        }

        public static string GetDescription(string RC, string param1, string param2)
        {
            return ResponseMessage.GetDescription(RC, param1, param2);
        }
    }
}