using Microsoft.EntityFrameworkCore;
using WebNauAn.Models;
using WebNauAn.Repository;
namespace WebNauAn.Repository
{
    public class BuocNauAnRepository
    {
        WebNauAnContext db = new WebNauAnContext();
        public BuocNauAnRepository() { }
        public BuocNauAnRepository(WebNauAnContext context)
        {
            this.db = context;
        }
        public IEnumerable<Cacbuocnau> GetCacbuocnaubymacongthuc(int macongthuc)
        {
            var result = from a in db.Cacbuocnaus
                         where a.MaCongThuc == macongthuc
                         select new Cacbuocnau
                         {
                             MaBuoc = a.MaBuoc,
                             MaCongThuc = a.MaCongThuc,
                             BuocThucHien = a.BuocThucHien,
                             HuongDan = a.HuongDan
                         };
            return result;
        }
    }
}
