using Core.Infrastructure.Domain.Contract.DTO.RefType;
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
