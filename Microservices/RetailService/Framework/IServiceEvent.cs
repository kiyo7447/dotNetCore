using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService
{
	public interface IServiceEvent
	{
		/// <summary>
		/// スキーマの更新、データの更新を行う。
		/// </summary>
		public void Migration();


	}
}
