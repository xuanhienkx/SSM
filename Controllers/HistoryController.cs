using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Services;

namespace SSM.Controllers
{
    public class HistoryController:BaseController
    {
        private IHistoryService historyService;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            historyService = new HistoryService();
        }

        public ActionResult GetListHistory(long id, string type)
        {
            var list = historyService.GetQuery(x => x.ObjectId == id && x.ObjectType == type).OrderByDescending(x=>x.CreateTime).ToList();
            return PartialView("_listForHist", list);
        }
    }
}