using Microsoft.EntityFrameworkCore;
using WebNauAn.Models;
using WebNauAn.Repository;
namespace WebNauAn.Repository
{
    public class LoaiMonAnRepository
    {
        WebNauAnContext db = new WebNauAnContext();
        public LoaiMonAnRepository() { }
        public LoaiMonAnRepository(WebNauAnContext context)
        {
            this.db = context;
        }
        public IEnumerable<Loaimonan> GetAllLoaiMonAn()
        {
            return db.Loaimonans;
        }

    }
}
