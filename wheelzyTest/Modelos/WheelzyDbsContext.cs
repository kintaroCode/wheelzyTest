using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wheelzyTest.Modelos;

public partial class WheelzyDbsContext : DbContext
{
    public WheelzyDbsContext()
    {
    }

    public WheelzyDbsContext(DbContextOptions<WheelzyDbsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amount> Amounts { get; set; }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<StatusLog> StatusLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MNTS6IJ\\SQLEXPRESS;Database=wheelzyDBs;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amount>(entity =>
        {
            entity.HasKey(e => e.AmountId).HasName("PK__Amount__63BA0EB156B71136");

            entity.ToTable("Amount");

            entity.Property(e => e.Amount1)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("Amount");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Amounts)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK_Amount_Buyer");

            entity.HasOne(d => d.Location).WithMany(p => p.Amounts)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Amount__Location__534D60F1");
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.BuyerId).HasName("PK__Buyer__4B81C62A4A004C57");

            entity.ToTable("Buyer");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Location).WithMany(p => p.Buyers)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Buyer_Location");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Car__68A0342EBB7D2DBC");

            entity.ToTable("Car", tb => tb.HasTrigger("trg_InsertNewCar"));

            entity.Property(e => e.Make).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Plate).HasMaxLength(10);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Submodel).HasMaxLength(50);

            entity.HasOne(d => d.Location).WithMany(p => p.Cars)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Car_Location");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA49747181F49");

            entity.ToTable("Location");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusLog>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__StatusLo__C8EE2063CD9F08A1");

            entity.ToTable("StatusLog");

            entity.Property(e => e.ChangeBy).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StatusDate)
                .HasColumnType("datetime")
                .HasColumnName("statusDate");

            entity.HasOne(d => d.Buyer).WithMany(p => p.StatusLogs)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StatusLog__Buyer__5629CD9C");

            entity.HasOne(d => d.Car).WithMany(p => p.StatusLogs)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatusLog_Car");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
