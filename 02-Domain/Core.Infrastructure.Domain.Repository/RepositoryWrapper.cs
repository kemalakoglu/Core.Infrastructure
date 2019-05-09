

using Core.Infrastructure.Domain.Repository;

namespace Core.Infrastructure.Domain.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly Context.Context.Context context;
        //private RefTypeRepository RefTypeRepository;

        public RepositoryWrapper(Context.Context.Context Context)
        {
            context = Context;
        }

        //public IRefTypeRepository RefTypeRepository
        //{
        //    get
        //    {
        //        if (RefTypeRepository == null) RefTypeRepository = new RefTypeRepository(context);

        //        return RefTypeRepository;
        //    }
        //}
    }
}