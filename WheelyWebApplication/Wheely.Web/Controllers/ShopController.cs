using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Wheely.Core.Data;
using Wheely.Service.Categories;
using Wheely.Service.Wheels;
using Wheely.Web.Factories.ShopFactories;

namespace Wheely.Web.Controllers
{
    public sealed class ShopController : BaseController
    {
        #region Fields
        private readonly IWheelService _wheelService;
        private readonly ICategoryService _categoryService;
        private readonly IShopModelFactory _shopModelFactory;
        #endregion

        #region Constructor
        public ShopController(
            IWheelService wheelService,
            ICategoryService categoryService,
            IShopModelFactory shopModelFactory)
        {
            _wheelService = wheelService;
            _categoryService = categoryService;
            _shopModelFactory = shopModelFactory;
        }
        #endregion

        #region Actions
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var result = _wheelService.GetWheelById(1);
            if (!result.Succeeded)
            {

            }

            var wheelDetailViewModel = _shopModelFactory.PrepareWheelDetailViewModel(result.Data);
            return View(wheelDetailViewModel);
        }

        //[HttpGet("/iletisim")]
        //[Route("/iletisim")]
        //[HttpGet("[controller]/[action]")]
        public IActionResult Test()
        {
            var wheel = _wheelService.GetWheelById(1).Data;
            wheel.Categories.Clear();
            List<int> categoryIds = new() { 1, 2 };

            var categories = _categoryService.GetCategoriesByCategoryIds(categoryIds);
            if (categories.Succeeded)
            {
                wheel.Categories.AddRange(categories.Data);
            }

            try
            {
                _wheelService.Update(wheel);
            }
            catch (System.Exception ex)
            {

                throw;
            }

            return View();
        }
        #endregion
    }
}
