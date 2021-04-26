using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RetailService.Services.ShoppingCart.Controllers
{

	//[Route("/shoppingcart")]
	[ApiController]
	[Route("[controller]")]
	public class ShoppingCartController : ControllerBase
	{
		/*
                private readonly IShoppingCartStore shoppingCartStore;

                 public ShoppingCartController(IShoppingCartStore shoppingCartStore)
                {
                    this.shoppingCartStore = shoppingCartStore;
                }
        [HttpGet("{userId:int}")]
        public ShoppingCart Get(int userId) =>
          this.shoppingCartStore.Get(userId);

        [HttpGet("{userId:int}")]
        public ShoppingCart Get(int userId) =>
          this.shoppingCartStore.Get(userId);
        */
		[HttpGet]
		public object Get()
		{
			return new { DisplayValue = "Hello World!", };
		}

	}
}

