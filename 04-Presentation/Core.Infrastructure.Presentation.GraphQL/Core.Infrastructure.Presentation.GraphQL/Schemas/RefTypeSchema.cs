using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Application.Service;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Core.Infrastructure.Presentation.GraphQL.Graphs;
using GraphQL;
using GraphQL.Types;

namespace Core.Infrastructure.Presentation.GraphQL.Schemas
{
    public class RefTypeSchema : ObjectGraphType<object>
    {
        public RefTypeSchema()
        {
            this.Name = "RefType";
            this.Description = "The query type, represents all of the entry points into our object graph.";

            this.FieldAsync<ListGraphType<RefTypeGraph>, IEnumerable<RefTypeDTO>>(
                "reftype",
                "Get a refTypes by all",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id",
                        Description = "The unique identifier of the RefType.",
                    }),
                resolve: context => null);
        }
    }
}
