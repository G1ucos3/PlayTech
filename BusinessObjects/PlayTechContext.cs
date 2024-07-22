using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects;

public partial class PlayTechContext : DbContext
{
    public PlayTechContext()
    {
    }

    public PlayTechContext(DbContextOptions<PlayTechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<CurrentComputer> CurrentComputers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GauDan\\GAUDAN;Database=PlayTech;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.ComputerId).HasName("PK__Computer__0DBB973F8998965D");

            entity.Property(e => e.ComputerId).HasColumnName("computerID");
            entity.Property(e => e.ComputerName)
                .HasMaxLength(50)
                .HasColumnName("computerName");
            entity.Property(e => e.ComputerSpec)
                .HasMaxLength(400)
                .HasColumnName("computerSpec");
            entity.Property(e => e.ComputerStatus).HasColumnName("computerStatus");
        });

        modelBuilder.Entity<CurrentComputer>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ComputerId }).HasName("PK__CurrentC__2B41A5AC3E6CB613");

            entity.ToTable("CurrentComputer");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.ComputerId).HasColumnName("computerID");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");

            entity.HasOne(d => d.Computer).WithMany(p => p.CurrentComputers)
                .HasForeignKey(d => d.ComputerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cc_computer");

            entity.HasOne(d => d.User).WithMany(p => p.CurrentComputers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cc_user");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809337DD04A866D");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.OrderStatus).HasColumnName("orderStatus");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_product");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_user");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__2D10D14A40134268");

            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.ProductImg)
                .HasMaxLength(4000)
                .HasColumnName("productImg");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("productName");
            entity.Property(e => e.ProductPrice).HasColumnName("productPrice");
            entity.Property(e => e.ProductQuantity).HasColumnName("productQuantity");
            entity.Property(e => e.ProductType)
                .HasMaxLength(50)
                .HasColumnName("productType");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CDF725CBB6F");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.UserAvatar)
                .HasMaxLength(4000)
                .HasColumnName("userAvatar");
            entity.Property(e => e.UserBalance).HasColumnName("userBalance");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(150)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .HasColumnName("userPassword");
            entity.Property(e => e.UserRoles).HasColumnName("userRoles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
