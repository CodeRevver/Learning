using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApplication4.Controllers
{
    public class AsyncAwaitController : ApiController
    {
        public async Task<IHttpActionResult> Get(bool continueOnContext)
        {
            var beforeRunningValue = HttpContext.Current != null;
            var whileRunningValue = await ExecuteExampleAsync(continueOnContext).ConfigureAwait(continueOnContext);
            var afterRunningValue = HttpContext.Current != null;
            var test = RequestContext.Principal;
            return Ok(new
            {
                ContinueOnCapturedContext = continueOnContext,
                BeforeRunningValue = beforeRunningValue,
                WhileRunningValue = whileRunningValue,
                AfterRunningValue = afterRunningValue,
                SameBeforeAndAfter = beforeRunningValue == afterRunningValue
            });
        }

        private async Task<bool> ExecuteExampleAsync(bool continueOnContext)
        {
            return await Task.Delay(TimeSpan.FromMilliseconds(10)).ContinueWith((task) =>
            {
                var hasHttpContext = HttpContext.Current != null;
                return hasHttpContext;
            }).ConfigureAwait(continueOnContext);
        }
    }
}