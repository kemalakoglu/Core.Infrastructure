using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Application.EventHandlers.RefType;
using Core.Infrastructure.Domain.Contract.DTO.RefType;
using Core.Infrastructure.Presentation.GraphQL.Graphs.RefType;
using GraphQL.Types;

namespace Core.Infrastructure.Presentation.GraphQL.Schemas
{
    public class RefTypeSchema : ObjectGraphType<object>
    {
        private readonly ICoreApplicationService applicationService;
        private readonly IRefTypeEventHandler handler;
        public RefTypeSchema(ICoreApplicationService applicationService, IRefTypeEventHandler handler)
        {
            this.applicationService = applicationService;
            this.handler = handler;
            this.Name = "RefType";
            this.Description = "The query type, represents all of the entry points into our object graph.";

            this.FieldAsync<GetRefTypesResponseGraph, GetRefTypesResponseDTO>(
                "GetRefTypes",
                "Get all refTypes",
                arguments: new QueryArguments(),
                resolve: async context => this.handler.GetRefTypes().Result);

            this.FieldAsync<RefTypeGraph, RefTypeDTO>(
                "GetRefTypeById",
                "Get a refType by id",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id",
                        Description = "The unique identifier of the RefType.",
                    }),
                resolve: async context => this.handler.GetRefTypeById(context.GetArgument<long>("id")).Result);
        }
    }
}
