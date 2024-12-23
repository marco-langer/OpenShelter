using OpenShelter.Models;

namespace OpenShelter.Repositories;

public interface IShelterRepository
{
    public Task AddShelterAsync(Shelter shelter, CancellationToken cancellationToken);
    
    public Task<List<Shelter>> GetSheltersAsync(CancellationToken cancellationToken);

    public Task<Shelter> GetShelterAsync(Guid shelterId, CancellationToken cancellationToken);

    public Task DeleteShelterAsync(Guid shelterId, CancellationToken cancellationToken);

    public Task AddAnimalAsync(Animal animal, CancellationToken cancellationToken);

    public Task<List<Animal>> GetAnimalsAsync(CancellationToken cancellationToken);

    public Task<Animal> GetAnimalAsync(Guid animalId, CancellationToken cancellationToken);

    public Task DeleteAnimalAsync(Guid animalId, CancellationToken cancellationToken);
}
