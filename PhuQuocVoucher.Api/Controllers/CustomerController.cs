using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.Dtos.CustomerDto;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/customer")]
[CrudExceptionFilter]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    private IUnitOfWork _work;

    private readonly ILogger<CustomerController> _logger;

    private readonly IRepository<Customer> _repo;

    public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger, IUnitOfWork work)
    {
        _customerService = customerService;
        _logger = logger;
        _work = work;
        _repo = work.Get<Customer>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindCustomer request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _customerService.GetAsync<CustomerSView>(new GetRequest<Customer>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Customer>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateCustomer request)
    {
        return Ok(await _customerService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<CustomerSView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Customer)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomer request, int id)
    {
        return Ok(await _customerService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _customerService.DeleteAsync(id));
    }


}