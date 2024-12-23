using System.ComponentModel.DataAnnotations;

namespace OpenShelter.Dtos.Models;

public sealed record ShelterResponse
{
    [Required]
    public required Guid Id { get; init; }

    [Required]
    public required String Name { get; init; }
}
