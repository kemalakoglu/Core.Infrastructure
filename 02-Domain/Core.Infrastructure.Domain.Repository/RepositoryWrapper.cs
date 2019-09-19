

using Core.Infrastructure.Domain.Repository;

namespace Core.Infrastructure.Domain.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly Context.Context.CoreContext context;
        //private RefTypeRepository RefTypeRepository;

        public RepositoryWrapper(Context.Context.CoreContext Context)
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