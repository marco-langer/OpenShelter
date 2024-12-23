using System.Data;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using OpenShelter.Exceptions;
using OpenShelter.Models;

namespace OpenShelter.Repositories;

public sealed class ShelterRepository(ILogger<ShelterRepository> logger, ShelterDbContext shelterDbContext) : IShelterRepository
{
    private readonly ILogger<ShelterRepository> _logger = logger;
    private readonly ShelterDbContext _shelterContext = shelterDbContext;

    public async Task AddShelterAsync(Shelter shelter, CancellationToken cancellationToken)
    {
        _shelterContext.Shelters.Add(shelter);

        try
        {
            await _shelterContext.SaveChangesAsync();
        }
        catch (UniqueConstraintException ex)
        {
            _logger.LogError("Unique constraint {ConstraintName} violated. Duplicate value for {Property}", ex.ConstraintName, ex.ConstraintProperties[0]);
            throw new ConflictException($"Unique constraint {ex.ConstraintName} violated. Duplicate value for {ex.ConstraintProperties[0]}");
        }
    }

    public async Task<List<Shelter>> GetSheltersAsync(CancellationToken cancellationToken)
    {
        return await _shelterContext.Shelters.ToListAsync(cancellationToken);
    }

    public async Task<Shelter> GetShelterAsync(Guid shelterId, CancellationToken cancellationToken)
    {
        Shelter? shelter = await _shelterContext.Shelters.FindAsync([shelterId], cancellationToken);
        if (shelter is null)
        {
            _logger.LogError("shelter with id '{ShelterId}' not found.", shelterId);
            throw new NotFoundException($"shelter with id '{shelterId}' not found.");
        }

        return shelter;
    }

    public async Task DeleteShelterAsync(Guid shelterId, CancellationToken cancellationToken)
    {
        Int32 affectedRows = await _shelterContext.Shelters.Where(shelter => shelter.Id == shelterId).ExecuteDeleteAsync(cancellationToken);
        if (affectedRows == 0)
        {
            _logger.LogError("shelter with id {ShelterId} not found.", shelterId);
            throw new NotFoundException($"shelter with id {shelterId} not found.");       
        }
    }

    public async Task AddAnimalAsync(Animal animal, CancellationToken cancellationToken)
    {
        _shelterContext.Animals.Add(animal);

        try
        {
            await _shelterContext.SaveChangesAsync();
        }
        catch (UniqueConstraintException ex)
        {
            _logger.LogError("Unique constraint {ConstraintName} violated. Duplicate value for {Property}", ex.ConstraintName, ex.ConstraintProperties[0]);
            throw new ConflictException($"Unique constraint {ex.ConstraintName} violated. Duplicate value for {ex.ConstraintProperties[0]}");
        }
    }

    public async Task<List<Animal>> GetAnimalsAsync(CancellationToken cancellationToken)
    {
        return await _shelterContext.Animals.ToListAsync(cancellationToken);
    }

    public async Task<Animal> GetAnimalAsync(Guid animalId, CancellationToken cancellationToken)
    {
        Animal? animal = await _shelterContext.Animals.FindAsync([animalId], cancellationToken);
        if (animal is null)
        {
            _logger.LogError("animal with id '{AnimalId}' not found.", animalId);
            throw new NotFoundException($"animal with id '{animalId}' not found.");
        }

        return animal;
    }

    public async Task DeleteAnimalAsync(Guid animalId, CancellationToken cancellationToken)
    {
        Int32 affectedRows = await _shelterContext.Animals.Where(animal => animal.Id == animalId).ExecuteDeleteAsync(cancellationToken);
        if (affectedRows == 0)
        {
            _logger.LogError("animal with id {AnimalId} not found.", animalId);
            throw new NotFoundException($"animal with id {animalId} not found.");       
        }
    }
}
