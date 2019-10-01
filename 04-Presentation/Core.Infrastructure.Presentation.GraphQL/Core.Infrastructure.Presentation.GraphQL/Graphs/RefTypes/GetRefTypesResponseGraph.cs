using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Presentation.GraphQL.Graphs.RefType;
using GraphQL.Types;

namespace Core.Infrastructure.Presentation.GraphQL.Graphs.RefType
{
    public class GetRefTypesResponseGraph : ObjectGraphType<GetRefTypesResponseDTO>
    {
        public GetRefTypesResponseGraph()
        {
            this.Name = "GetRefTypesResponse";
            this.Description = "RefType List";

            this.Field(x => x.RefTypes, type: typeof(ListGraphType<RefTypeGraph>)).Description("The unique identifier of the RefType."); ;
        }
    }
}
