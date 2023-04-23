using Microsoft.AspNetCore.Mvc;
using WebNauAn.Repository;
namespace WebNauAn.ViewComponents
{
    public class CacBuocNauViewComponent : ViewComponent
    {
        private readonly BuocNauAnRepository _buocNauAnRepository;
        public CacBuocNauViewComponent(BuocNauAnRepository buocNauAnRepository)
        {
            _buocNauAnRepository = buocNauAnRepository;
        }
        public IViewComponentResult Invoke(int macongthuc)
        {
            var cacbuocnau = _buocNauAnRepository.GetCacbuocnaubymacongthuc(macongthuc);
            return View(cacbuocnau);
        }
    }
}
