using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Presentation.GraphQL.Graphs;
using GraphQL;
using GraphQL.Types;

namespace Core.Infrastructure.Presentation.GraphQL.Schemas
{
    public class MainSchema: Schema
    {
    public MainSchema(
        IDependencyResolver resolver)
        : base(resolver)
    {
        this.Query = resolver.Resolve<RefTypeSchema>();
    }
    }
}
