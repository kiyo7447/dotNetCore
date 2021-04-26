using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HelloController : ControllerBase
	{
		[HttpGet]
		public object Get()
		{
			return new { DisplayValue = "Hello World!", };
		}

	}
}
