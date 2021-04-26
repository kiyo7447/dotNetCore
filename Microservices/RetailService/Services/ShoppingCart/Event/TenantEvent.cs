using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService.Services.ShoppingCart
{
	public class TenantEvent : ITenantEvent
	{
		public void AddTenant(string tenantCd)
		{
			try {

			}catch (Exception ex)
			{
				throw new SystemError(ex, "法人追加処理でエラーが発生しました。{tenantCd}", tenantCd);
			}
			throw new NotImplementedException();
		}

		public void RemoveTenant(string tenantCd)
		{
			throw new NotImplementedException();
		}
	}
}
