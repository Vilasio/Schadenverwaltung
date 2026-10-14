using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schadenverwaltung.Api.Dtos;
using Schadenverwaltung.Api.Mapping;
using Schadenverwaltung.Infrastructure.Persistence;
using Schadenverwaltung.Infrastructure.Numbering;
using Schadenverwaltung.Domain.Entities;

namespace Schadenverwaltung.Api.Controllers;

[ApiController]
[Route("api/claims")]
public class ClaimsController : ControllerBase
{
    private readonly SchadenverwaltungDbContext _dbContext;
    private readonly INumberGenerator _numberGenerator;

    public ClaimsController(SchadenverwaltungDbContext dbContext, INumberGenerator numberGenerator)
    {
        _dbContext = dbContext;
        _numberGenerator = numberGenerator;
    }

    [HttpGet]
    public async Task<IEnumerable<ClaimResponse>> GetAll()
    {
        var claim = await _dbContext.Claims
            .Select(c => c.ToResponse())
            .ToListAsync();

        return claim;
    }

    [HttpGet("{Id:int}")]
    public async Task<ActionResult<ClaimResponse>> GetById(int Id)
    {
        var claim = await _dbContext.Claims
            .Where(c => c.Id == Id)
            .Select(c => c.ToResponse())
            .FirstOrDefaultAsync();

        if (claim is null)
        {
            return NotFound();
        }

        return claim;
    }

    [HttpPost]
    public async Task<ActionResult<ClaimResponse>> Create(CreateClaimRequest request)
    {
        if (!await _dbContext.Contracts.AnyAsync(c => c.Id == request.ContractId))
        {
            ModelState.AddModelError(nameof(request.ContractId), "Vertrag nicht gefunden.");
            return ValidationProblem(ModelState);
        }

        var claimNumber = await _numberGenerator.NextClaimNumberAsync();

        var claim = new Claim(request.ContractId, claimNumber, request.DateOfLoss!.Value, request.Description,
            request.Reserve);
        _dbContext.Claims.Add(claim);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = claim.Id }, claim.ToResponse());
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<ClaimResponse>> ChangeStatus(int id, ChangeClaimStatusRequest request)
    {
        var claim = await _dbContext.Claims
            .FirstOrDefaultAsync(c => c.Id == id);

        if (claim is null)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Schaden konnte mit ID={id} nicht gefunden werden.");
        }

        try
        {
            claim.ChangeStatus(request.Status!.Value);
        }
        catch (InvalidOperationException ex)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Statuswechsel nicht erlaubt.",
                detail: ex.Message);
        }

        await _dbContext.SaveChangesAsync();

        return claim.ToResponse();
    }
}