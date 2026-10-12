using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schadenverwaltung.Api.Dtos;
using Schadenverwaltung.Infrastructure.Persistence;

namespace Schadenverwaltung.Api.Controllers;

[ApiController]
[Route("api/contracts")]
public class ContractController : ControllerBase
{
    private readonly SchadenverwaltungDbContext _dbContext;
    
    public ContractController(SchadenverwaltungDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<ContractResponse>> GetAll()
    {
        var contracts = await _dbContext.Contracts
            .Select(c => new ContractResponse(
                c.Id,
                c.ContractNumber,
                c.PolicyHolder,
                c.LineOfBusiness.ToString(),
                c.StartDate,
                c.StartDate.ToString())).ToListAsync();
        
        return contracts;
    }
    
    [HttpGet("{id}")]
    public string GetById(int id)
    {
        var contracts = new Dictionary<int, string>()
        {
            [1] = "V-000001",
            [2] = "V-000002"
        };

        return contracts[id];
    }
}