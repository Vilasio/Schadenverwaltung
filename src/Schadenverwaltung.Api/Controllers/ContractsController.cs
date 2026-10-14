using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schadenverwaltung.Api.Dtos;
using Schadenverwaltung.Api.Mapping;
using Schadenverwaltung.Infrastructure.Persistence;
using Schadenverwaltung.Infrastructure.Numbering;
using Schadenverwaltung.Domain.Entities;

namespace Schadenverwaltung.Api.Controllers;

[ApiController]
[Route("api/contracts")]
public class ContractsController : ControllerBase
{
    private readonly SchadenverwaltungDbContext _dbContext;
    private readonly INumberGenerator _numberGenerator;

    public ContractsController(SchadenverwaltungDbContext dbContext, INumberGenerator numberGenerator)
    {
        _dbContext = dbContext;
        _numberGenerator = numberGenerator;
    }

    [HttpGet]
    public async Task<IEnumerable<ContractResponse>> GetAll()
    {
        var contracts = await _dbContext.Contracts
            .Select(c => c.ToResponse())
            .ToListAsync();

        return contracts;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContractResponse>> GetById(int id)
    {
        var contract = await _dbContext.Contracts
            .Where(c => c.Id == id)
            .Select(c => c.ToResponse())
            .FirstOrDefaultAsync();

        if (contract is null)
        {
            return NotFound();
        }

        return contract;
    }

    [HttpPost]
    public async Task<ActionResult<ContractResponse>> Create(CreateContractRequest request)
    {
        string contractNumber = await _numberGenerator.NextContractNumberAsync();
        Contract contract = new Contract(contractNumber, request.PolicyHolder, request.LineOfBusiness, request.StartDate);
        
        _dbContext.Contracts.Add(contract);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = contract.Id }, contract.ToResponse());
    }
}