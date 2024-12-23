using Microsoft.AspNetCore.Mvc;
using OpenShelter.Dtos.Mapping;
using OpenShelter.Dtos.Models;
using OpenShelter.Models;
using OpenShelter.Repositories;

namespace OpenShelter.Configuration;

[ApiController]
[Route("api/[controller]")]
public sealed class SheltersController(IShelterRepository shelterRepository) : ControllerBase
{
    private readonly IShelterRepository _shelterRepository = shelterRepository;

    [HttpPost]
    public async Task<IActionResult> CreateShelter(ShelterRequest shelterRequest, CancellationToken cancellationToken)
    {
        Shelter shelter = shelterRequest.ToModel();
        await _shelterRepository.AddShelterAsync(shelter, cancellationToken);

        return CreatedAtAction(
            actionName: nameof(GetShelter),
            routeValues: new { shelterId = shelter.Id},
            value: shelter.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<Shelter>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShelters(CancellationToken cancellationToken)
    {
        List<Shelter> shelters = await _shelterRepository.GetSheltersAsync(cancellationToken);

        return Ok(shelters.ToResponse());
    }

    [HttpGet("{shelterId:guid}")]
    [ProducesResponseType<Shelter>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetShelter(Guid shelterId, CancellationToken cancellationToken)
    {
        Shelter shelter = await _shelterRepository.GetShelterAsync(shelterId, cancellationToken);

        return Ok(shelter.ToResponse());
    }

    [HttpDelete("{shelterId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteShelter(Guid shelterId, CancellationToken cancellationToken)
    {
        await _shelterRepository.DeleteShelterAsync(shelterId, cancellationToken);

        return NoContent();
    }
}
