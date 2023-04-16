using Microsoft.EntityFrameworkCore;
using ToDoWPF.Model;

namespace ToDoWPF.AppData;

public partial class UsersContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }

    public UsersContext() { }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=users;Username=postgres;Password=masj109ia4002");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Pass).HasColumnName("pass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
