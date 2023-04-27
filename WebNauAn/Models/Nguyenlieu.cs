using System;
using System.Collections.Generic;

namespace WebNauAn.Models;

public partial class Nguyenlieu
{
    public int MaNguyenLieu { get; set; }

    public int MaLoaiNguyenLieu { get; set; }

    public string? TenNguyenLieu { get; set; }

    public double? SoLuong { get; set; }

    public string? DonVi { get; set; }

    public virtual ICollection<CongthucNguyenlieu> CongthucNguyenlieus { get; set; } = new List<CongthucNguyenlieu>();

    public virtual Loainguyenlieu MaLoaiNguyenLieuNavigation { get; set; } = null!;
}
