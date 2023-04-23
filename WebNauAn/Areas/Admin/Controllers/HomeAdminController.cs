using Microsoft.AspNetCore.Mvc;
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
        [Route("danhmuccongthuc")]
        public IActionResult DanhMucCongThuc()
        {
            var lstcongthuc = db.Congthucs.ToList();
            return View(lstcongthuc);
        }
    }
}
