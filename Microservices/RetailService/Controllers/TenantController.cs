using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService.Controllers
{
	[ApiController]
	[Route("admin/[controller]")]
	public class TenantController : ControllerBase
    {
        private readonly ILogger<TenantController> _logger;
        private readonly IEnumerable<ITenantEvent> _tenantEvent;
        public TenantController(
            IEnumerable<ITenantEvent> tenantEvent)
        {
            _tenantEvent = tenantEvent;
        }
        [HttpPost("add")]
        public IActionResult AddTenant(string tenantCd)
        {
            _tenantEvent.All(e => {e.AddTenant(tenantCd); return true;});
            return Ok();
        }
        [HttpPost("remove")]
        public IActionResult RemoveTenant(string tenantCd)
        {
            //_tenantEvent.All(e => { e.DeletedTenant(tenantCd); return true; });
            //↓製品コードは、どのサービスの法人削除でエラーが発声したのかわかるようにする。
            foreach(var tenantEvent in _tenantEvent)
			{                
                try
                {
                    _logger.LogInformation("法人追加処理の開始。{type.FullName}{tenantCd}", tenantEvent?.GetType().FullName, tenantCd);
					tenantEvent.RemoveTenant(tenantCd);
                    _logger.LogInformation("法人追加処理の終了。{type.FullName}{tenantCd}", tenantEvent?.GetType().FullName, tenantCd);
                }
                catch (Exception ex)
				{
                    //.NET Coreはまだ構造化ログ対応の例外がないので作成
                    throw new SystemError(ex,"法人削除処理でエラーが発生しました。{type.FullName}{tenantCd}", tenantEvent.GetType().FullName, tenantCd);
				}
			}
            return Ok();
        }
    }
}
