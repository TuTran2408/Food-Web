using System;
using System.Collections.Generic;

namespace WebNauAn.Models;

public partial class Congthuc
{
    public int MaCongThuc { get; set; }

    public string? TenCongThuc { get; set; }

    public string? MoTa { get; set; }

    public int? ThoiGianChuanBi { get; set; }

    public int? TongThoiGianNau { get; set; }

    public int? PhucVu { get; set; }

    public string? TacGia { get; set; }

    public string? Anh { get; set; }

    public string? AnhChiTiet { get; set; }

    public virtual ICollection<Cacbuocnau> Cacbuocnaus { get; set; } = new List<Cacbuocnau>();

    public virtual ICollection<CongthucNguyenlieu> CongthucNguyenlieus { get; set; } = new List<CongthucNguyenlieu>();
}
