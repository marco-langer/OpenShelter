using OpenShelter.Dtos.Models;
using OpenShelter.Models;

namespace OpenShelter.Dtos.Mapping;

public static class ShelterMapper
{
    public static ShelterResponse ToResponse(this Shelter shelter)
    {
        return new ShelterResponse
        {
            Id = shelter.Id,
            Name = shelter.Name
        };
    }

    public static List<ShelterResponse> ToResponse(this List<Shelter> shelters)
    {
        return shelters.Select(ToResponse).ToList();
    }

    public static Shelter ToModel(this ShelterRequest shelterRequest)
    {
        return new Shelter
        {
            Id = shelterRequest.Id,
            Name = shelterRequest.Name
        };
    }
}
