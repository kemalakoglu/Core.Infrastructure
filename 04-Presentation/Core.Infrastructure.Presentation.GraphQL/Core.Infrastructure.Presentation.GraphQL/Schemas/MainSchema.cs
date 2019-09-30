using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Application.Service;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Core.Infrastructure.Domain.Contract.Service;
using Core.Infrastructure.Presentation.GraphQL.Graphs;
using GraphQL;
using GraphQL.Types;

namespace Core.Infrastructure.Presentation.GraphQL.Schemas
{
    public class MainSchema : Schema
    {
        private ICoreApplicationService applicationService;
        private IUnitOfWork uow;
        public MainSchema(
            IDependencyResolver resolver, RefTypeSchema refTypeSchema, ICoreApplicationService applicationService, IUnitOfWork uow)
            : base(resolver)
        {
            this.applicationService = applicationService;
            this.uow = uow;
            resolver.Resolve<CoreApplicationService>();
            this.Query = resolver.Resolve<RefTypeSchema>();

        }
    }
}
