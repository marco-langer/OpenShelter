using Microsoft.AspNetCore.Mvc;
using OpenShelter.Dtos.Mapping;
using OpenShelter.Dtos.Models;
using OpenShelter.Models;
using OpenShelter.Repositories;

namespace OpenShelter.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AnimalsController(IShelterRepository shelterRepository) : ControllerBase
{
    private readonly IShelterRepository _shelterRepository = shelterRepository;

    [HttpPost]
    [ProducesResponseType<Animal>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAnimal(AnimalRequest animalRequest, CancellationToken cancellationToken)
    {
        Animal animal = animalRequest.ToModel();
        await _shelterRepository.AddAnimalAsync(animal, cancellationToken);

        return CreatedAtAction(
            actionName: nameof(GetAnimal),
            routeValues: new { AnimalId = animal.Id },
            value: animal.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<Animal>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnimals(CancellationToken cancellationToken)
    {
        List<Animal> animals = await _shelterRepository.GetAnimalsAsync(cancellationToken);

        return Ok(animals.ToResponse());
    }

    [HttpGet("{animalId:guid}")]
    [ProducesResponseType<IEnumerable<Animal>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAnimal(Guid animalId, CancellationToken cancellationToken)
    {
        Animal animal = await _shelterRepository.GetAnimalAsync(animalId, cancellationToken);

        return Ok(animal.ToResponse());
    }

    [HttpDelete("{animalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAnimal(Guid animalId, CancellationToken cancellationToken)
    {
        await _shelterRepository.DeleteAnimalAsync(animalId, cancellationToken);

        return NoContent();
    }
}
