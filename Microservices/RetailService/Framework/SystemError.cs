using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService
{
	public class SystemError : Exception
	{
		public SystemError(Exception ex, string message, params object[] arg): base(message, ex)
		{
			
		}
	}
}
