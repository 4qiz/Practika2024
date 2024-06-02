using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextoptions) : base(dbContextoptions)
        {

        }

        public virtual DbSet<IssueRequest> IssueRequests { get; set; }

        public virtual DbSet<IssueRequestHasMedicine> IssueRequestHasMedicines { get; set; }

        public virtual DbSet<Medicine> Medicines { get; set; }

        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseHasMedicine> WarehouseHasMedicines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            //apteka
            modelBuilder.Entity<IssueRequest>(entity =>
            {
                entity.HasKey(e => e.IssueRequestId);

                entity.ToTable("IssueRequest");

                entity.Property(e => e.Purpose);
            });

            modelBuilder.Entity<IssueRequestHasMedicine>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_IssueRequestHasMedicine_1");

                entity.ToTable("IssueRequestHasMedicine");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IssueRequest).WithMany(p => p.IssueRequestHasMedicines)
                    .HasForeignKey(d => d.IssueRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueRequestHasMedicine_IssueRequest");

                entity.HasOne(d => d.Medicine).WithMany(p => p.IssueRequestHasMedicines)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueRequestHasMedicine_Medicine");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.HasKey(e => e.MedicineId);

                entity.ToTable("Medicine");

                entity.Property(e => e.Image).HasMaxLength(500);
                entity.Property(e => e.Manufacturer).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Price).HasColumnType("decimal(13, 2)");
                entity.Property(e => e.TradeName).HasMaxLength(50);
            });
            modelBuilder.Entity<Medicine>()
                .HasMany(e => e.Warehouses)
                .WithMany(e => e.Medicines)
                .UsingEntity<WarehouseHasMedicine>();


            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            List<Warehouse> warehouses = new List<Warehouse>()
            {
                new Warehouse
                {
                    WarehouseId = 1,
                    Name = "Склад №2",
                },
                new Warehouse
                {
                    WarehouseId = 2,
                    Name = "Склад №2",
                },
                new Warehouse
                {
                    WarehouseId = 3,
                    Name = "Склад №3",
                },
            };

            modelBuilder.Entity<Warehouse>().HasData(warehouses);

            List<Medicine> medicine = new List<Medicine>()
            {
                new Medicine
                {
                    MedicineId = 1,
                    Name = "Вольтарен 25мг/мл 3мл 5 шт. раствор для внутримышечного введения",
                    TradeName = "Вольтарен",
                    Manufacturer = "Новартис Фарма АГ",
                    Image = "https://imgs.asna.ru/iblock/2d7/2d71cac199086932e4b68e6ae633eca8/100082.jpg",
                    Price = 79,
                },
                new Medicine
                {
                    MedicineId = 2,
                    Name = "Кальцекс 500мг 10 шт. таблетки татхимфарм",
                    TradeName = "Кальцекс",
                    Manufacturer = "Татхимфармпрепараты АО",
                    Image = "https://imgs.asna.ru/iblock/177/177882ef988b42be05abd45dbb7d5fba/816f88b93afa5c096afbeec679ffd4c0.jpg",
                    Price = 42,
                },
            };

            modelBuilder.Entity<Medicine>().HasData(medicine);

            List<WarehouseHasMedicine> warehouseHasMedicines = new List<WarehouseHasMedicine>()
            {
                new WarehouseHasMedicine
                {
                    WarehouseId = 2,
                    MedicineId = 1,
                    Quantity = 41,
                },
            };

            modelBuilder.Entity<WarehouseHasMedicine>().HasData(warehouseHasMedicines);
        }

    }
}
