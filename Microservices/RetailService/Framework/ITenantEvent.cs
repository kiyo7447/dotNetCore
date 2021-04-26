using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService
{
	public interface ITenantEvent
	{
		public void AddTenant(string tenantCd);

		public void RemoveTenant(string tenantCd);
	}
}
