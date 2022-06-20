using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using PhuQuocVoucher.Api.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
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


    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _customerService.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomer request)
    {
        return Ok(await _customerService.CreateAsync(request));
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