using IntexWinter2024.Models;
using IntexWinter2024.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IntexWinter2024.Components;

public class ProductCategoriesViewComponent : ViewComponent
{
    private IIntexWinter2024Repository _legoRepo;
    
    // constructor
    public ProductCategoriesViewComponent(IIntexWinter2024Repository temp)
    {
        _legoRepo = temp;
    }

    public IViewComponentResult Invoke()
    {
        var productCategories = _legoRepo.ProductCategories
            .Select(x => x.CategoryName)
            .Distinct()
            .OrderBy(x => x);

        return View(productCategories);
    }
}