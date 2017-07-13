using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SSM.Common;
using SSM.Models;

namespace SSM.Services
{
    public interface IServerFileService : IServices<ServerFile>
    {
        List<ServerFile> GetServerFile(long objectId, string objectType);
        ServerFile GetById(long id);
    }
    public class ServerFileService : Services<ServerFile>, IServerFileService
    {
        public List<ServerFile> GetServerFile(long objectId, string objectType)
        {
            var qr = GetQuery(x => x.ObjectId == objectId && x.ObjectType == objectType);
            qr = qr.OrderBy(x => x.FileName);
            return qr.ToList();
        }


        public ServerFile GetById(long id)
        {
            return FindEntity(x => x.Id == id);
        }
    }
}