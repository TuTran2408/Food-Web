using Microsoft.EntityFrameworkCore;
using WebNauAn.Models;
using WebNauAn.Repository;
namespace WebNauAn.Repository
{
    public class NguyenLieuRepository
    {
        WebNauAnContext db = new WebNauAnContext();
        public NguyenLieuRepository() { }
        public NguyenLieuRepository(WebNauAnContext context)
        {
            this.db = context;
        }
        public IEnumerable<Nguyenlieu> GetNguyenLieuByMaCongThuc(int macongthuc)
        {
            var ketqua = from a in db.Nguyenlieus
                         join b in db.CongthucNguyenlieus on a.MaNguyenLieu equals b.MaNguyenLieu
                         join c in db.Congthucs on b.MaCongThuc equals c.MaCongThuc
                         where c.MaCongThuc == macongthuc
                         select new Nguyenlieu
                         {
                             TenNguyenLieu = a.TenNguyenLieu,
                             SoLuong= a.SoLuong,
                             DonVi = a.DonVi
                         };
            return ketqua;
        }
    }
}
