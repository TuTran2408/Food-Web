using System;
using System.Collections.Generic;

namespace WebNauAn.Models;

public partial class CongthucNguyenlieu
{
    public int IdCongThucNguyenLieu { get; set; }

    public int? MaCongThuc { get; set; }

    public int? MaNguyenLieu { get; set; }

    public virtual Congthuc? MaCongThucNavigation { get; set; } = null!;

    public virtual Nguyenlieu? MaNguyenLieuNavigation { get; set; } = null!;
}
