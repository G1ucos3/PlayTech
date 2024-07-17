﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:PlayTech"];
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.ComputerId).HasName("PK__Computer__0DBB973FFB526FB5");

            entity.Property(e => e.ComputerId).HasColumnName("computerID");
            entity.Property(e => e.ComputerName)
                .HasMaxLength(50)
                .HasColumnName("computerName");
            entity.Property(e => e.ComputerStatus).HasColumnName("computerStatus");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809337DBB2B1394");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_order_product");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_order_user");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__2D10D14A942F99AD");

            entity.Property(e => e.ProductId).HasColumnName("productID");
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

            entity.HasMany(d => d.Computers).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "CurrentComputer",
                    r => r.HasOne<Computer>().WithMany()
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_cc_computer"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_cc_user"),
                    j =>
                    {
                        j.HasKey("UserId", "ComputerId").HasName("PK__CurrentC__2B41A5ACD123375F");
                        j.ToTable("CurrentComputer");
                        j.IndexerProperty<int>("UserId").HasColumnName("userID");
                        j.IndexerProperty<int>("ComputerId").HasColumnName("computerID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
