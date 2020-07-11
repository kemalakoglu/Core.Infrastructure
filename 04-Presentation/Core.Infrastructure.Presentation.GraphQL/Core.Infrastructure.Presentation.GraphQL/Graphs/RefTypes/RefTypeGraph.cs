using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Domain.Contract.DTO.RefType;
using GraphQL.Types;

namespace Core.Infrastructure.Presentation.GraphQL.Graphs.RefType
{
    public class RefTypeGraph : ObjectGraphType<RefTypeDTO>
    {
        private ICoreApplicationService appService;
        public RefTypeGraph(ICoreApplicationService appService)
        {
            this.appService = appService;
            this.Name = "RefType";
            this.Description = "RefType List";

            this.Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of the RefType."); ;
            this.Field(x => x.Name, true);
            this.Field(name: "Parent", type: typeof(RefTypeGraph), resolve: context => this.appService.GetRefTypeById(context.Source));
            this.Field(x => x.InsertDate, true);
            this.Field(x => x.UpdateDate, true);
            this.Field(x => x.IsActive, true);
            this.Field(x => x.Status, true);
        }
    }
}
