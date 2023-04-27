using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebNauAn.Models;

public partial class WebNauAnContext : DbContext
{
    public WebNauAnContext()
    {
    }

    public WebNauAnContext(DbContextOptions<WebNauAnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cacbuocnau> Cacbuocnaus { get; set; }

    public virtual DbSet<Congthuc> Congthucs { get; set; }

    public virtual DbSet<CongthucLoaimonan> CongthucLoaimonans { get; set; }

    public virtual DbSet<CongthucNguyenlieu> CongthucNguyenlieus { get; set; }

    public virtual DbSet<Loaimonan> Loaimonans { get; set; }

    public virtual DbSet<Loainguyenlieu> Loainguyenlieus { get; set; }

    public virtual DbSet<Nguyenlieu> Nguyenlieus { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-TBNEGGC9\\SQLEXPRESS;Initial Catalog=Web_Nau_An;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cacbuocnau>(entity =>
        {
            entity.HasKey(e => e.MaBuoc).HasName("pk_CACBUOCNAU");

            entity.ToTable("CACBUOCNAU");

            entity.Property(e => e.MaBuoc).ValueGeneratedNever();

            entity.HasOne(d => d.MaCongThucNavigation).WithMany(p => p.Cacbuocnaus)
                .HasForeignKey(d => d.MaCongThuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CACBUOCNAU_CONGTHUC");
        });

        modelBuilder.Entity<Congthuc>(entity =>
        {
            entity.HasKey(e => e.MaCongThuc).HasName("pk_CONGTHUC");

            entity.ToTable("CONGTHUC");

            entity.Property(e => e.MaCongThuc).ValueGeneratedNever();
            entity.Property(e => e.Anh).HasMaxLength(200);
            entity.Property(e => e.AnhChiTiet).HasMaxLength(100);
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.TacGia).HasMaxLength(50);
            entity.Property(e => e.TenCongThuc).HasMaxLength(100);
        });

        modelBuilder.Entity<CongthucLoaimonan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CONGTHUC_LOAIMONAN");

            entity.HasOne(d => d.MaCongThucNavigation).WithMany()
                .HasForeignKey(d => d.MaCongThuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONGTHUC_LOAIMONAN_CONGTHUC");

            entity.HasOne(d => d.MaLoaiMonAnNavigation).WithMany()
                .HasForeignKey(d => d.MaLoaiMonAn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONGTHUC_LOAIMONAN_LOAIMONAN");
        });

        modelBuilder.Entity<CongthucNguyenlieu>(entity =>
        {
            entity.HasKey(e => e.IdCongThucNguyenLieu);

            entity.ToTable("CONGTHUC_NGUYENLIEU");

            entity.Property(e => e.IdCongThucNguyenLieu)
                .ValueGeneratedNever()
                .HasColumnName("ID_CongThuc_NguyenLieu");

            entity.HasOne(d => d.MaCongThucNavigation).WithMany(p => p.CongthucNguyenlieus)
                .HasForeignKey(d => d.MaCongThuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONGTHUC_NGUYENLIEU_CONGTHUC");

            entity.HasOne(d => d.MaNguyenLieuNavigation).WithMany(p => p.CongthucNguyenlieus)
                .HasForeignKey(d => d.MaNguyenLieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONGTHUC_NGUYENLIEU_NGUYENLIEU");
        });

        modelBuilder.Entity<Loaimonan>(entity =>
        {
            entity.HasKey(e => e.MaLoaiMonAn).HasName("pk_LOAIMONAN");

            entity.ToTable("LOAIMONAN");

            entity.Property(e => e.MaLoaiMonAn).ValueGeneratedNever();
            entity.Property(e => e.Mota).HasMaxLength(200);
            entity.Property(e => e.TenLoaiMonAn).HasMaxLength(100);
        });

        modelBuilder.Entity<Loainguyenlieu>(entity =>
        {
            entity.HasKey(e => e.MaLoaiNguyenLieu).HasName("pk_LOAINGUYENLIEU");

            entity.ToTable("LOAINGUYENLIEU");

            entity.Property(e => e.MaLoaiNguyenLieu).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(200);
            entity.Property(e => e.TenLoai).HasMaxLength(20);
        });

        modelBuilder.Entity<Nguyenlieu>(entity =>
        {
            entity.HasKey(e => e.MaNguyenLieu).HasName("pk_NGUYENLIEU");

            entity.ToTable("NGUYENLIEU");

            entity.Property(e => e.MaNguyenLieu).ValueGeneratedNever();
            entity.Property(e => e.DonVi).HasMaxLength(20);
            entity.Property(e => e.TenNguyenLieu).HasMaxLength(100);

            entity.HasOne(d => d.MaLoaiNguyenLieuNavigation).WithMany(p => p.Nguyenlieus)
                .HasForeignKey(d => d.MaLoaiNguyenLieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NGUYENLIEU_LOAINGUYENLIEU");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TenDangNhap);

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
