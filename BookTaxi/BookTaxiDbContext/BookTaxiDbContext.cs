using BookTaxiEntyties.Entyties;
using Microsoft.EntityFrameworkCore;

namespace BookTaxiEntyties.BookTaxiDbContext;

public class BookTaxiDbContext:DbContext
{
    public BookTaxiDbContext(DbContextOptions<BookTaxiDbContext> options) : base(options)
    { }
    public DbSet<CarInfo> CarInfos { get; set; }
    public DbSet<Cars> Cars { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<SeatInfo> SeatInfos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCarOrders> UserCarOrders { get; set; }
    public DbSet<UserCars> UserCars { get; set; }
}