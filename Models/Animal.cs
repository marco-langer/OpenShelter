namespace OpenShelter.Models;

public sealed class Animal
{
    public required Guid Id { get; init; }
    public required String Name { get; set; }
}
