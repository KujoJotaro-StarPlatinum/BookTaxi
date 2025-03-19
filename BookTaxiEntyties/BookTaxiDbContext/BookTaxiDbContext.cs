using BookTaxiEntyties.Entyties;
using Microsoft.EntityFrameworkCore;

namespace BookTaxiEntyties.Context;

public class BookTaxiDbContext:DbContext
{
    public BookTaxiDbContext(DbContextOptions<BookTaxiDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Cars> Cars { get; set; }
    public DbSet<CarInfo> CarInfos { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Payments> Paymets { get; set; }
    public DbSet<SeatInfo> SeatInfos { get; set; }
    public DbSet<UserCars> UserCars { get; set; }
    public DbSet<UserCarsOrders> UserCarOrders { get; set; }
    public DbSet<Sms> Sms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cars>()
            .HasOne(c => c.Destination)
            .WithOne(d => d.Car)
            .HasForeignKey<Destination>(d => d.CarId); // Указываем зависимую сторону и внешний ключ

        modelBuilder.Entity<UserCarsOrders>()
            .HasOne(u => u.Paymets) // связь "один-к-одному"
            .WithOne(p => p.CarOrder)
            .HasForeignKey<UserCarsOrders>(u => u.PaymentId); // указываем внешний ключ для зависимости
    }
}