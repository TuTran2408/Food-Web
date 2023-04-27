using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics;
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
        public IActionResult ThemSanPhamMoi(Congthuc sanpham, IFormFile anh, IFormFile anhchitiet)
        {
            Debug.WriteLine("File name: " + anh.FileName); // Debugging line
            Debug.WriteLine("File name: " + anhchitiet.FileName); // Debugging line
            if (ModelState.IsValid)
            {
                if (anh != null && anh.Length > 0)
                {
                    var fileName = Path.GetFileName(anh.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images_NAUAN", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        anh.CopyTo(fileStream);
                    }
                    sanpham.Anh = fileName;
                }
                if (anhchitiet != null && anhchitiet.Length > 0)
                {
                    var fileName = Path.GetFileName(anhchitiet.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images_NAUAN", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        anhchitiet.CopyTo(fileStream);
                    }
                    sanpham.AnhChiTiet = fileName;
                }
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

        //CAC BUOC NAU
        [Route("getAllCongthucBuocNau")]
        [HttpGet]
        public IActionResult getAllCongthucBuocNau(int macongthuc)
        {
            var listBuoc = (from a in db.Cacbuocnaus
                                  where a.MaCongThuc == macongthuc
                                  select new listBuoc
                                  {
                                      maBuoc = a.MaBuoc,
                                      buocThucHien = a.BuocThucHien,
                                      huongDan = a.HuongDan,
                                      maCongThuc = a.MaCongThuc
                                  }).OrderBy(x => x.buocThucHien).ToList();
            ViewBag.MaCongThuc = macongthuc;
            return View(listBuoc);
        }
        public class listBuoc
        {
            public int? maBuoc;
            public int? buocThucHien;
            public String? huongDan;
            public int? maCongThuc;
        }
        [Route("congthucbuocnau")]
        public IActionResult CongThucBuocNau()
        {
            var lstcongthuc = db.Congthucs.ToList();
            return View(lstcongthuc);
        }
        [Route("ThemBuocNau")]
        [HttpGet]
        public IActionResult ThemBuocNau(int macongthuc)
        {
            //ViewBag.MaCongThuc = Int32.Parse(macongthuc);
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.Where(x => x.MaCongThuc == macongthuc).ToList(), "MaCongThuc", "TenCongThuc");
            return View();
        }
        [Route("ThemBuocNau")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemBuocNau(Cacbuocnau cacbuocnau)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Cacbuocnaus.Add(cacbuocnau);
                    db.SaveChanges();
                    return RedirectToAction("congthucbuocnau");
                }
                catch (Exception ex)
                {
                    // Log the error message or display it on the page.
                    ModelState.AddModelError("", "An error occurred while saving the data: " + ex.Message);

                    // Get the inner exception, if any.
                    Exception innerException = ex.InnerException;
                    while (innerException != null)
                    {
                        // Log or display the inner exception message.
                        // For example:
                        ModelState.AddModelError("", "Inner exception message: " + innerException.Message);

                        innerException = innerException.InnerException;
                    }
                }
            }

            // If there are validation errors, return the view with the model to display the errors.
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.ToList(), "MaCongThuc", "TenCongThuc");
            return View(cacbuocnau);
        }
        [Route("SuaBuocNau")]
        [HttpGet]
        public IActionResult SuaBuocNau(int mabuocnau)
        {
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.ToList(), "MaCongThuc", "TenCongThuc");
            var buocnau = db.Cacbuocnaus.Find(mabuocnau);
            return View(buocnau);
        }
        [Route("SuaBuocNau")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaBuocNau(Cacbuocnau cacbuocnau)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(cacbuocnau).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("getAllCongthucBuocNau");
                }
                catch (Exception ex)
                {
                    // Log the error message or display it on the page.
                    ModelState.AddModelError("", "An error occurred while saving the data: " + ex.Message);

                    // Get the inner exception, if any.
                    Exception innerException = ex.InnerException;
                    while (innerException != null)
                    {
                        // Log or display the inner exception message.
                        // For example:
                        ModelState.AddModelError("", "Inner exception message: " + innerException.Message);

                        innerException = innerException.InnerException;
                    }
                }
            }

            // If there are validation errors, return the view with the model to display the errors.
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.ToList(), "MaCongThuc", "TenCongThuc");
            return View(cacbuocnau);
        }
        [Route("XoaBuocNau")]
        [HttpGet]
        public IActionResult XoaBuocNau(int mabuocnau)
        {
            TempData["Message"] = "";
            db.Remove(db.Cacbuocnaus.Find(mabuocnau));
            db.SaveChanges();
            TempData["Message"] = "Đã được xóa";
            return RedirectToAction("congthucbuocnau", "HomeAdmin");

        }

        //CONGTHUC-NGUYENLIEU
        [Route("getAllCongthucNguyenLieu")]
        [HttpGet]
        public IActionResult getAllCongthucNguyenLieu(int macongthuc)
        {
            {

            };
            var listNguyenLieu = (from a in db.CongthucNguyenlieus
                                  join b in db.Nguyenlieus on a.MaNguyenLieu equals b.MaNguyenLieu
                                  where a.MaCongThuc == macongthuc
                                  select new listNguyenLieu
                                  {
                                      MaNguyenLieu = a.MaNguyenLieu,
                                      TenNguyenLieu = b.TenNguyenLieu,
                                      idCongThucNguyenLieu = a.IdCongThucNguyenLieu
                                  }).ToList();
            return View(listNguyenLieu);
        }
        public class listNguyenLieu
        {
            public int? MaNguyenLieu;
            public String? TenNguyenLieu;
            public int? idCongThucNguyenLieu;
        }
        [Route("congthucnguyenlieu")]
        public IActionResult CongThucNguyenLieu()
        {
            var lstcongthuc = db.Congthucs.ToList();
            return View(lstcongthuc);
        }
        [Route("ThemNguyenLieuCongThuc")]
        [HttpGet]
        public IActionResult ThemNguyenLieuCongThuc(int macongthuc)
        {
            //ViewBag.MaCongThuc = Int32.Parse(macongthuc);
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.Where(x => x.MaCongThuc == macongthuc).ToList(), "MaCongThuc", "TenCongThuc");
            ViewBag.MaNguyenLieu = new SelectList(db.Nguyenlieus.ToList(), "MaNguyenLieu", "TenNguyenLieu");
            return View();
        }
        [Route("ThemNguyenLieuCongThuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNguyenLieuCongThuc(CongthucNguyenlieu congthucNguyenlieu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CongthucNguyenlieus.Add(congthucNguyenlieu);
                    db.SaveChanges();
                    return RedirectToAction("congthucnguyenlieu");
                }
                catch (Exception ex)
                {
                    // Log the error message or display it on the page.
                    ModelState.AddModelError("", "An error occurred while saving the data: " + ex.Message);
                }
            }

            // If there are validation errors, return the view with the model to display the errors.
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.ToList(), "MaCongThuc", "TenCongThuc");
            ViewBag.MaNguyenLieu = new SelectList(db.Nguyenlieus.ToList(), "MaNguyenLieu", "TenNguyenLieu");
            return View(congthucNguyenlieu);
        }
        [Route("SuaCongThucNguyenLieu")]
        [HttpGet]
        public IActionResult SuaCongThucNguyenLieu(int idcongthucnguyenlieu)
        {
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.ToList(), "MaCongThuc", "TenCongThuc");
            ViewBag.MaNguyenLieu = new SelectList(db.Nguyenlieus.ToList(), "MaNguyenLieu", "TenNguyenLieu");
            var congthucnguyenlieu = db.CongthucNguyenlieus.Find(idcongthucnguyenlieu);
            return View(congthucnguyenlieu);
        }
        [Route("SuaCongThucNguyenLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCongThucNguyenLieu(CongthucNguyenlieu congthucnguyenlieu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(congthucnguyenlieu).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("getAllCongthucBuocNau");
                }
                catch (Exception ex)
                {
                    // Log the error message or display it on the page.
                    ModelState.AddModelError("", "An error occurred while saving the data: " + ex.Message);

                    // Get the inner exception, if any.
                    Exception innerException = ex.InnerException;
                    while (innerException != null)
                    {
                        // Log or display the inner exception message.
                        // For example:
                        ModelState.AddModelError("", "Inner exception message: " + innerException.Message);

                        innerException = innerException.InnerException;
                    }
                }
            }

            // If there are validation errors, return the view with the model to display the errors.
            ViewBag.MaCongThuc = new SelectList(db.Congthucs.ToList(), "MaCongThuc", "TenCongThuc");
            return View(congthucnguyenlieu);
        }
        [Route("XoaCongThucNguyenLieu")]
        [HttpGet]
        public IActionResult XoaCongThucNguyenLieu(int idcongthucnguyenlieu)
        {
            TempData["Message"] = "";
            db.Remove(db.CongthucNguyenlieus.Find(idcongthucnguyenlieu));
            db.SaveChanges();
            TempData["Message"] = "Đã được xóa";
            return RedirectToAction("congthucnguyenlieu", "HomeAdmin");

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
    }
}
