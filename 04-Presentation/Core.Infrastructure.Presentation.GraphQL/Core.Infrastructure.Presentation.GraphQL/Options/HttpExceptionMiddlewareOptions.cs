using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infrastructure.Presentation.GraphQL.Options
{
    /// <summary>
    /// Options controlling <see cref="HttpExceptionMiddleware"/>.
    /// </summary>
    public class HttpExceptionMiddlewareOptions
    {
        /// <summary>
        /// Property controlling if ReasonPhrase should be included in HttpResponseMessage.
        /// </summary>
        public bool IncludeReasonPhraseInResponse { get; set; } = false;
    }
}
