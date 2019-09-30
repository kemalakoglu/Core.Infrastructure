using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using GraphQL.Types;

namespace Core.Infrastructure.Presentation.GraphQL.Graphs
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

            //this.FieldAsync<RefTypeGraph, RefTypeDTO>(
            //    nameof(RefTypeDTO.Parent),
            //    "The friends of the character, or an empty list if they have none.",
            //    resolve: context => this.appService.GetRefTypeById(context.Source));
        }
    }
}
