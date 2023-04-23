using Microsoft.AspNetCore.Mvc;
using WebNauAn.Repository;
namespace WebNauAn.ViewComponents
{
    public class NguyenLieuViewComponent: ViewComponent
    {
        private readonly NguyenLieuRepository _repository;
        public NguyenLieuViewComponent(NguyenLieuRepository repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke(int macongthuc)
        {
            var nguyenlieu = _repository.GetNguyenLieuByMaCongThuc(macongthuc);
            return View(nguyenlieu);
        }
    }
}
