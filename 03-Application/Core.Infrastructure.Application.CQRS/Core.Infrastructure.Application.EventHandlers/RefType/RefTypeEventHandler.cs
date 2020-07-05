using System.Threading.Tasks;
using Core.Infrastructure.Domain.Contract.DTO.RefType;
using MediatR;

namespace Core.Infrastructure.Application.EventHandlers.RefType
{
    public class RefTypeEventHandler: IRefTypeEventHandler
    {
        private readonly IMediator mediator;

        public RefTypeEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// GetRefTypes
        /// </summary>
        /// <returns></returns>
        public Task<GetRefTypesResponseDTO> GetRefTypes()
        {
            return Task.FromResult(this.mediator.Send(new GetRefTypesResponseDTO()).Result);
        }

        /// <summary>
        /// GetRefTypeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RefTypeDTO> GetRefTypeById(long id)
        {
            return Task.FromResult(this.mediator.Send(new RefTypeDTO{Id = id}).Result);
        }
    }

    public interface IRefTypeEventHandler
    {
        Task<GetRefTypesResponseDTO> GetRefTypes();
        Task<RefTypeDTO> GetRefTypeById(long id);
    }
}
