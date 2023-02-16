using CustomerApi.Dtos;
using CustomerApi.Repositories;

namespace CustomerApi.CasosDeUso
{
    public interface IUpdateCustomerUseCase
    {
        Task<CustomerDto?> Execute(CustomerDto customer);
    }
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly dbsystemCContext _dbsystemccontext;

        public UpdateCustomerUseCase(dbsystemCContext dbsystemccontext)
        {
            _dbsystemccontext = dbsystemccontext;
        }

        public async Task<CustomerDto?> Execute(CustomerDto customer)
        {
            var entity = await _dbsystemccontext.Get(customer.Id);
            if (entity == null) return null;

            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Address = customer.Address;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;

            await _dbsystemccontext.Actualizar(entity);
            return entity.ToDto();

        }

    }
}
