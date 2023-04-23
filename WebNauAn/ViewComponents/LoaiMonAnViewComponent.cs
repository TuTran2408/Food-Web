using Microsoft.AspNetCore.Mvc;
using WebNauAn.Repository;
namespace WebNauAn.ViewComponents
{
    public class LoaiMonAnViewComponent: ViewComponent
    {
        private readonly LoaiMonAnRepository _repository;
        public LoaiMonAnViewComponent(LoaiMonAnRepository repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            var loaimonan = _repository.GetAllLoaiMonAn().OrderBy(x=>x.TenLoaiMonAn);
            return View(loaimonan);
        }
    }
}
