using System;
using System.Collections.Generic;

namespace WebNauAn.Models;

public partial class Cacbuocnau
{
    public int MaBuoc { get; set; }

    public int? MaCongThuc { get; set; }

    public int? BuocThucHien { get; set; }

    public string? HuongDan { get; set; }

    public virtual Congthuc? MaCongThucNavigation { get; set; } = null!;
}
