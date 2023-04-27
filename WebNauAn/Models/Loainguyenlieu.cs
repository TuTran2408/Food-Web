using System;
using System.Collections.Generic;

namespace WebNauAn.Models;

public partial class Loainguyenlieu
{
    public int MaLoaiNguyenLieu { get; set; }

    public string TenLoai { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<Nguyenlieu> Nguyenlieus { get; set; } = new List<Nguyenlieu>();
}
