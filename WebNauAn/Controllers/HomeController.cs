using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebNauAn.Models;
using X.PagedList;

namespace WebNauAn.Controllers
{
    public class HomeController : Controller
    {
        WebNauAnContext db = new WebNauAnContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
                int pageSize = 12;
                int pageNumber = page == null || page < 0 ? 1 : page.Value;
                var lstcongthuc = db.Congthucs.AsNoTracking().OrderBy(x => x.MaCongThuc);
                PagedList<Congthuc> lst = new PagedList<Congthuc>(lstcongthuc, pageNumber, pageSize);
                return View(lst);
            
        }
        public IActionResult searchFood(String searchText) 
        {
            var listSearchFood = db.Congthucs.Where(x => x.TenCongThuc.Contains(searchText)).ToList();
            return View(listSearchFood); 
        }
        public IActionResult ChiTietCongThuc(int macongthuc)
        {
            ViewBag.macongthuc = macongthuc;
            var congthuc = db.Congthucs.SingleOrDefault(x => x.MaCongThuc == macongthuc);
            return View(congthuc);
        }
        public IActionResult CongThucTheoLoaiMonAn(int maloaimonan,int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = (from a in db.Loaimonans
                              join b in db.CongthucLoaimonans on a.MaLoaiMonAn equals b.MaLoaiMonAn
                              join c in db.Congthucs on b.MaCongThuc equals c.MaCongThuc
                              select new congthucLoaimonan
                              {
                                  MaLoaiMonAn = a.MaLoaiMonAn,
                                  MaCongThuc = c.MaCongThuc,
                                  TenCongThuc = c.TenCongThuc,
                                  TacGia = c.TacGia,
                                  Anh = c.Anh
                              }).Where(x=>x.MaLoaiMonAn==maloaimonan);
            PagedList<congthucLoaimonan> lst = new PagedList<congthucLoaimonan>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
public class congthucLoaimonan
{
    public int MaCongThuc { get; set; }
    public int MaLoaiMonAn { get; set; }
    public string? TenCongThuc { get; set; }
    public string? TacGia { get; set; }
    public string? Anh { get; set; }


}