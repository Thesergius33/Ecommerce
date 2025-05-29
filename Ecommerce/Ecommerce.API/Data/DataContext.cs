using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSets organizados alfabéticamente
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones por entidad (orden alfabético)
            ConfigureCategory(modelBuilder);
            ConfigureCity(modelBuilder);
            ConfigureCountry(modelBuilder);
            ConfigureProduct(modelBuilder);
            ConfigureProductCategory(modelBuilder);
            ConfigureProductImage(modelBuilder);
            ConfigureState(modelBuilder);
            ConfigureUser(modelBuilder);
        }

        private void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();

                entity.HasOne(c => c.ProductCategory)
                    .WithMany(pc => pc.Categories)
                    .HasForeignKey(c => c.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureCity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(c => new { c.Name, c.StateId }).IsUnique();
            });
        }

        private void ConfigureCountry(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
            });
        }

        private void ConfigureProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();

                entity.HasOne(p => p.ProductCategory)
                    .WithMany(pc => pc.Products)
                    .HasForeignKey(p => p.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureProductCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasIndex(pc => pc.Name).IsUnique();
            });
        }

        private void ConfigureProductImage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasIndex(pi => new { pi.Image, pi.ProductId }).IsUnique();
            });
        }

        private void ConfigureState(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>(entity =>
            {
                entity.HasIndex(s => new { s.Name, s.CountryId }).IsUnique();
            });
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => new { u.Document, u.CityId }).IsUnique();
            });
        }
    }
}