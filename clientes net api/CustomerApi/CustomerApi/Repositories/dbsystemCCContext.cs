using CustomerApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerApi.Repositories
{
    public class dbsystemCContext :DbContext
    {
        

        public dbsystemCContext(DbContextOptions<dbsystemCContext> options) : base(options)
        {
            
        }
        public DbSet<CustomerEntity> customers { get; set; }

        public async Task<CustomerEntity?> Get(long id)
        {
            return await customers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            customers.Remove(entity);
            SaveChanges();
            return true;
        }



        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                Address = customerDto.Address,
                Email = customerDto.Email,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Phone = customerDto.Phone,
            };

            EntityEntry<CustomerEntity> response = await customers.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("no se ha podido guardar"));

        
        }

        public async Task<bool> Actualizar(CustomerEntity customerEntity)
        {
            customers.Update(customerEntity);
            await SaveChangesAsync();

            return true;
        }


    }

    public class CustomerEntity
    {

        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }


        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address,
                Id = Id ?? throw new Exception("no puede ser null")
            };
        
        }

    }

}
