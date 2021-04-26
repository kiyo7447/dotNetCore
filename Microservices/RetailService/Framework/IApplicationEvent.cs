using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService
{
	public interface IApplicationEvent
	{
		public void Wakeup();

		public bool Live();

		public bool Helth();

		public object Version();

//		public 
	}
}
