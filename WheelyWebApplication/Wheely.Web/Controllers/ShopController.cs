using Microsoft.AspNetCore.Mvc;
using System;
using Wheely.Service.Wheels;
using Wheely.Web.Factories.ShopFactories;

namespace Wheely.Web.Controllers
{
    public sealed class ShopController : BaseController
    {
        #region Fields
        private readonly IWheelService _wheelService;
        private readonly IShopModelFactory _shopModelFactory;
        #endregion

        #region Constructor
        public ShopController(
            IWheelService wheelService, 
            IShopModelFactory shopModelFactory)
        {
            _wheelService = wheelService;
            _shopModelFactory = shopModelFactory;
        }
        #endregion

        #region Actions
        [HttpGet]
        public IActionResult Detail()
        {
            throw new ArgumentNullException();

            var result = _wheelService.GetWheelById(1);
            if (!result.Success)
            {

            }

            var wheelDetailViewModel = _shopModelFactory.PrepareWheelDetailViewModel(result.Data);
            return View(wheelDetailViewModel);
        }
        #endregion
    }
}
