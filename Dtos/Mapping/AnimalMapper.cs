using OpenShelter.Dtos.Models;
using OpenShelter.Models;

namespace OpenShelter.Dtos.Mapping;

public static class AnimalMapper
{
    public static AnimalResponse ToResponse(this Animal animal)
    {
        return new AnimalResponse
        {
            Id = animal.Id,
            Name = animal.Name
        };
    }

    public static List<AnimalResponse> ToResponse(this List<Animal> animals)
    {
        return animals.Select(ToResponse).ToList();
    }

    public static Animal ToModel(this AnimalRequest animalRequest)
    {
        return new Animal
        {
            Id = animalRequest.Id,
            Name = animalRequest.Name
        };
    }
}
