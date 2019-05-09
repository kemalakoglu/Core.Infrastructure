using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Core.Infrastructure.Core.Resources
{
    public static class ResponseMessage
    {
        public const string Success = "RC0000";
        public const string NotNullable = "RC0001";
        public const string Failed = "RC0002";
        public const string NotFound = "RC0003";
        public const string Unauthorized = "RC0004";
        public const string BadRequest = "RC0005";

        public static string GetDescription(string RC)
        {
            var prepareDescription = new PrepareDescription();
            return prepareDescription.GetValue(RC);
        }

        public static string GetDescription(string RC, string param1)
        {
            var prepareDescription = new PrepareDescription();
            return string.Format(prepareDescription.GetValue(RC), param1);
        }

        public static string GetDescription(string RC, string param1, string param2)
        {
            var prepareDescription = new PrepareDescription();
            return string.Format(prepareDescription.GetValue(RC), param1, param2);
        }
    }

    public class PrepareDescription
    {
        public string GetValue(string RC)
        {
            var rm = new ResourceManager("Core.Infrastructure.Core.Resources.RC",
                Assembly.GetExecutingAssembly());
            return rm.GetString(RC, CultureInfo.CurrentCulture);
        }
    }
}