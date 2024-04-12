using IntexWinter2024.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntexWinter2024.Components;
public class ProductColorsViewComponent : ViewComponent
{
    private IIntexWinter2024Repository _repo;

    // constructor
    public ProductColorsViewComponent(IIntexWinter2024Repository repo)
    {
        _repo = repo;
    }

    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedProductColor = RouteData?.Values["productColor"];

        var productColors = _repo.Products
            .Select(x => x.PrimaryColor)
            .Distinct()
            .OrderBy(x => x);

        return View(productColors);
    }
}
