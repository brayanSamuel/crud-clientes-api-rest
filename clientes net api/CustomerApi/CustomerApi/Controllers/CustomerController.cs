using Microsoft.AspNetCore.Mvc;
using CustomerApi.Dtos;
using CustomerApi.Repositories;
using CustomerApi.CasosDeUso;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : Controller
    {

        private readonly dbsystemCContext _dbsystemccontext;
        private readonly IUpdateCustomerUseCase _updatecustomerusecase;

        public CustomerController(dbsystemCContext dbsystemccontext, IUpdateCustomerUseCase updatecustomerusecase)
        {
            _dbsystemccontext = dbsystemccontext;
            _updatecustomerusecase = updatecustomerusecase;
        }

            //api/customer
            [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomer()
        {
            var result = _dbsystemccontext.customers.Select(c=>c.ToDto()).ToList();

            return new OkObjectResult(result);
        }

        //api/customer/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(long id)
        {
            CustomerEntity result = await _dbsystemccontext.Get(id);
            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
             var result = await _dbsystemccontext.Delete(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await _dbsystemccontext.Add(customer);

            return new CreatedResult($"https://localhost:7208/api/customer/{result.Id}",null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await _updatecustomerusecase.Execute(customer);

            if (result == null) return new NotFoundResult();
                
            return new OkObjectResult(result);
        }

    }
}
