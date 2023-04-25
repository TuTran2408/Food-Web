using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNauAn.Models;

namespace WebNauAn.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        WebNauAnContext db = new WebNauAnContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        // CONGTHUC
        [Route("danhmuccongthuc")]
        public IActionResult DanhMucCongThuc()
        {
            var lstcongthuc = db.Congthucs.ToList();
            return View(lstcongthuc);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(Congthuc sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Congthucs.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("DanhMucCongThuc");
            }
            return View(sanpham);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int macongthuc)
        {
          
            var congthuc = db.Congthucs.Find(macongthuc);
            return View(congthuc);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Congthuc congthuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congthuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucCongThuc", "HomeAdmin");
            }
            return View(congthuc);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int macongthuc)
        {
            TempData["Message"] = "";
            var cacbuocnau = db.Cacbuocnaus.Where(x => x.MaCongThuc == macongthuc).ToList();
            if (cacbuocnau.Count() > 0)
            {
                TempData["Message"] = "Không xóa được công thức này";
                return RedirectToAction("DanhMucCongThuc", "HomeAdmin");
            }
            var congthuc_loaimonan = db.CongthucLoaimonans.Where(x => x.MaCongThuc == macongthuc).ToList();
            if (congthuc_loaimonan.Count() > 0)
            {
                TempData["Message"] = "Không xóa được công thức này";
                return RedirectToAction("DanhMucCongThuc", "HomeAdmin");
            }
            var congthuc_nguyenlieu = db.CongthucNguyenlieus.Where(x => x.MaCongThuc == macongthuc).ToList();
            if (congthuc_nguyenlieu.Count() > 0)
            {
                TempData["Message"] = "Không xóa được công thức này";
                return RedirectToAction("DanhMucCongThuc", "HomeAdmin");
            }
            db.Remove(db.Congthucs.Find(macongthuc));
            db.SaveChanges();
            TempData["Message"] = "Công thức đã được xóa";
            return RedirectToAction("DanhMucCongThuc", "HomeAdmin");

        }

        //NGUYENLIEU
        [Route("danhmucnguyenlieu")]
        public IActionResult DanhMucNguyenLieu()
        {
            var lstnguyenlieu = db.Nguyenlieus.ToList();
            return View(lstnguyenlieu);
        }
        [Route("ThemNguyenLieuMoi")]
        [HttpGet]
        public IActionResult ThemNguyenLieuMoi()
        {
            ViewBag.MaLoaiNguyenLieu = new SelectList(db.Loainguyenlieus.ToList(), "MaLoaiNguyenLieu", "TenLoai");
            return View();
        }
        [Route("ThemNguyenLieuMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNguyenLieuMoi(Nguyenlieu nguyenlieu)
        {
            if (ModelState.IsValid)
            {
                db.Nguyenlieus.Add(nguyenlieu);
                db.SaveChanges();
                return RedirectToAction("DanhMucNguyenLieu");
            }
            return View(nguyenlieu);
        }
        [Route("SuaNguyenLieu")]
        [HttpGet]
        public IActionResult SuaNguyenLieu(int manguyenlieu)
        {
            ViewBag.MaLoaiNguyenLieu = new SelectList(db.Loainguyenlieus.ToList(), "MaLoaiNguyenLieu", "TenLoai");
            var nguyenlieu = db.Nguyenlieus.Find(manguyenlieu);
            return View(nguyenlieu);
        }
        [Route("SuaNguyenLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNguyenLieu(Nguyenlieu nguyenlieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguyenlieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucNguyenLieu", "HomeAdmin");
            }
            return View(nguyenlieu);
        }


        [Route("XoaNguyenLieu")]
        [HttpGet]
        public IActionResult XoaNguyenLieu(int manguyenlieu)
        {
            TempData["Message"] = "";
            var congthuc_nguyenlieu = db.CongthucNguyenlieus.Where(x => x.MaNguyenLieu == manguyenlieu).ToList();
            if (congthuc_nguyenlieu.Count() > 0)
            {
                TempData["Message"] = "Không xóa được nguyên liệu này";
                return RedirectToAction("DanhMucNguyenLieu", "HomeAdmin");
            }
           
            db.Remove(db.Nguyenlieus.Find(manguyenlieu));
            db.SaveChanges();
            TempData["Message"] = "Nguyên Liệu đã được xóa";
            return RedirectToAction("DanhMucNguyenLieu", "HomeAdmin");

        }


        //LOAINGUYENLIEU
        [Route("danhmucloainguyenlieu")]
        public IActionResult DanhMucLoaiNguyenLieu()
        {
            var lstloainguyenlieu = db.Loainguyenlieus.ToList();
            return View(lstloainguyenlieu);
        }
        [Route("ThemLoaiNguyenLieu")]
        [HttpGet]
        public IActionResult ThemLoaiNguyenLieu()
        {
            return View();
        }
        [Route("ThemLoaiNguyenLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoaiNguyenLieu(Loainguyenlieu maloai)
        {
            if (ModelState.IsValid)
            {
                db.Loainguyenlieus.Add(maloai);
                db.SaveChanges();
                return RedirectToAction("DanhMucLoaiNguyenLieu");
            }
            return View(maloai);
        }

        [Route("SuaLoaiNguyenLieu")]
        [HttpGet]
        public IActionResult SuaLoaiNguyenLieu(int maloainguyenlieu)
        {
            var loainguyenlieu = db.Loainguyenlieus.Find(maloainguyenlieu);
            return View(loainguyenlieu);
        }
        [Route("SuaLoaiNguyenLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoaiNguyenLieu(Loainguyenlieu loainguyenlieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loainguyenlieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucLoaiNguyenLieu", "HomeAdmin");
            }
            return View(loainguyenlieu);
        }


        [Route("XoaLoaiNguyenLieu")]
        [HttpGet]
        public IActionResult XoaLoaiNguyenLieu(int maloainguyenlieu)
        {
            TempData["Message"] = "";
            db.Remove(db.Loainguyenlieus.Find(maloainguyenlieu));
            db.SaveChanges();
            TempData["Message"] = "Loại nguyên liệu đã được xóa";
            return RedirectToAction("DanhMucLoaiNguyenLieu", "HomeAdmin");

        }

        //CONGTHUC_NGUYENLIEU
        // lỗi k tìm thấy primary key k thêm đc
        [Route("danhmuccongthucnguyenlieu")]
        public IActionResult DanhMucCongThucNguyenLieu()
        {
            var lst = db.CongthucNguyenlieus.ToList();
            return View(lst);
        }

        [Route("ThemCongThucNguyenLieu")]
        [HttpGet]
        public IActionResult ThemCongThucNguyenLieu()
        {
            return View();
        }
        [Route("ThemCongThucNguyenLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemCongThucNguyenLieu(CongthucNguyenlieu congthucnguyenlieu)
        {
            if (ModelState.IsValid)
            {
                db.CongthucNguyenlieus.Add(congthucnguyenlieu);
                db.SaveChanges();
                return RedirectToAction("DanhMucCongThucNguyenLieu");
            }
            return View(congthucnguyenlieu);
        }

        //CacBuocNau
        [Route("danhmuccacbuocnau")]
        public IActionResult DanhMucCacBuocNau()
        {
            var lst = db.Cacbuocnaus.ToList();
            return View(lst);
        }

        [Route("ThemCacBuocNau")]
        [HttpGet]
        public IActionResult ThemCacBuocNau()
        {
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.ToList(), "MaCongThuc", "TenCongThuc");
            return View();
        }
        [Route("ThemCongThucNguyenLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemCacBuocNau(Cacbuocnau cacbuocnau)
        {
            if (ModelState.IsValid)
            {
                db.Cacbuocnaus.Add(cacbuocnau);
                db.SaveChanges();
                return RedirectToAction("DanhMucCacBuocNau");
            }
            return View(cacbuocnau);
        }

        [Route("SuaCacBuocNau")]
        [HttpGet]
        public IActionResult SuaCacBuocNau(int mabuocnau)
        {
            var cacbuocnau = db.Cacbuocnaus.Find(mabuocnau);
            return View(cacbuocnau);
        }
        [Route("SuaCacBuocNau")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCacBuocNau(Cacbuocnau cacbuocnau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cacbuocnau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucCacBuocNau", "HomeAdmin");
            }
            return View(cacbuocnau);
        }

        //chưa có xóa các bước nấu vì còn khúc mắc :))
    }
}
