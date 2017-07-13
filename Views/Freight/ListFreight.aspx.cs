using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSM.Models;

namespace SSM.Views.Freight
{
    public partial class ListOceanFreight : ViewPage<SSM.Models.FreightModel>
    {
        private FreightServices _freightService;
        protected void Page_Load(object sender, EventArgs e)
        {
            _freightService = new FreightServicesImpl();
        }
        public String getDocumentsBy(long ObjectId) {
            IEnumerable<ServerFile> Files = _freightService.getServerFile(ObjectId, new SSM.Models.Freight().GetType().ToString());
            String link = "";
            if (Files != null && Files.Count() > 0) {
                foreach (ServerFile File1 in Files) {
                    link += "<a class='LinkClass' href='../.." + File1.Path + "' target='_blank'>" + File1.FileName + "</a><br />";
                }
            }
            return link;
        }
    }
}