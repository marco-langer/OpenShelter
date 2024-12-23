using Microsoft.EntityFrameworkCore;
using OpenShelter.Models;

namespace OpenShelter;

public sealed class ShelterDbContext(DbContextOptions<ShelterDbContext> options) : DbContext(options)
{
    public DbSet<Shelter> Shelters { get; init; } = null!;
    public DbSet<Animal> Animals { get; init; } = null!;
}
