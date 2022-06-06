using CateringSedapAPI.Entitties;
using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Food> Foods { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Delivery> Deliveries { get; set; } = null!;
        public DbSet<ReservationFood> ReservationFoods { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>();
            builder.Entity<Admin>();
            builder.Entity<Driver>();


            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
