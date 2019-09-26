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
        public RefTypeGraph()
        {
            //this.appService = appService;
            this.Name = "RefType";
            this.Description = "RefType List";

            this.Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of the RefType."); ;
            this.Field(x => x.Name);
            this.Field(x => x.Parent, type: typeof(ObjectGraphType<RefTypeGraph>));
            this.Field(x => x.InsertDate);
            this.Field(x => x.UpdateDate);
            this.Field(x => x.IsActive);
            this.Field(x => x.Status);

            //this.FieldAsync<RefTypeGraph, RefTypeDTO>(
            //    nameof(RefTypeDTO.Parent),
            //    "The friends of the character, or an empty list if they have none.",
            //    resolve: context => this.appService.GetRefTypeById(context.Source));
        }
    }
}
