using System;
using System.Collections.Generic;

namespace WebNauAn.Models;

public partial class CongthucLoaimonan
{
    public int MaCongThuc { get; set; }

    public int MaLoaiMonAn { get; set; }

    public virtual Congthuc MaCongThucNavigation { get; set; } = null!;

    public virtual Loaimonan MaLoaiMonAnNavigation { get; set; } = null!;
}
