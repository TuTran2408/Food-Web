using Microsoft.AspNetCore.Mvc;
using WebNauAn.Models;
namespace WebNauAn.Controllers
{
    public class AccessController : Controller
    {
        WebNauAnContext db = new WebNauAnContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TenDangNhap") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult Login(TaiKhoan user)
        {
            if (HttpContext.Session.GetString("TenDangNhap") == null)
            {
                var u = db.TaiKhoans.Where(x => x.TenDangNhap.Equals(user.TenDangNhap) && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("TenDangNhap", u.TenDangNhap.ToString());
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    return View();
                }
            }

            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TenDangNhap");
            return RedirectToAction("Index", "Home");
        }
    }
}
