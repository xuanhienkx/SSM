using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSM.Common;


namespace SSM.Models
{

    public interface FreightServices {
        IEnumerable<ServerFile> getServerFile(long ObjectId, String ObjectType);
        bool insertServerFile(ServerFile file);
        ServerFile getServerFile(long id);
        bool deleteServerFile(long id);
        IEnumerable<Freight> getAllFreightByType(String FreightType);
        IEnumerable<Freight> getAllFreight(FreightModel FreightModel1, String FreightType);
        IEnumerable<Freight> getAllFreight(long Departure, long Destination);
        IEnumerable<Freight> getAllFreight(String AirlineCode);
        Freight getFreightById(long id);
        FreightModel getFreightModelById(long id);
        Freight CreateFreight(FreightModel FreightModel1);
        bool UpdateFreight(FreightModel FreightModel1);
        bool DeleteFreight(long id);
    }
    public class FreightServicesImpl : FreightServices
    {
        private DataClasses1DataContext db;
        public FreightServicesImpl()
        {
            db = new DataClasses1DataContext();
        }
        public IEnumerable<ServerFile> getServerFile(long ObjectId, String ObjectType) {
            try { 
                return (from ServerFile1 in db.ServerFiles
                            where ServerFile1.ObjectId !=null && ServerFile1.ObjectId.Value == ObjectId
                            && ObjectType.Equals(ServerFile1.ObjectType)
                            select ServerFile1);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e.Message);
            }
            return null;
        }
        public bool insertServerFile(ServerFile file) {
            try {
                db.ServerFiles.InsertOnSubmit(file);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e) {
                Logger.LogError(e);
                System.Console.Out.Write(e.Message);
            }
            return false;
        }

        public ServerFile getServerFile(long id)
        {
            try
            {
               return db.ServerFiles.Single(s=>s.Id==id);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e.Message);
            }
            return null;
        }

        public bool deleteServerFile(long id)
        {
            try
            {
                ServerFile file = getServerFile(id);
                db.ServerFiles.DeleteOnSubmit(file);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e.Message);
            }
            return false;
        }

        public IEnumerable<Freight> getAllFreightByType(String FreightType) {
            try
            {
                return (from Freight1 in db.Freights
                        where (Freight1.Type != null && Freight1.Type.Contains(FreightType))
                        select Freight1);
            }catch(Exception e) {
                return null;
            }
        }
        public IEnumerable<Freight> getAllFreight(FreightModel FreightModel1, String FreightType) {
            try
            {
                return (from Freight1 in db.Freights
                        where (Freight1.Type == null || Freight1.Type.Contains(FreightType))
                        && (FreightModel1.AirlineName == null || (Freight1.CarrierAirLine != null && Freight1.CarrierAirLine.CarrierAirLineName.Contains(FreightModel1.AirlineName)))
                        && (FreightModel1.DepartureId == 0 || (Freight1.DepartureId != null && Freight1.DepartureId.Value == FreightModel1.DepartureId))
                        && (FreightModel1.DestinationId == 0 || (Freight1.DestinationId != null && Freight1.DestinationId.Value == FreightModel1.DestinationId))
                        select Freight1);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public IEnumerable<Freight> getAllFreight(long Departure, long Destination) {
            return (from Freight1 in db.Freights
                    where (Departure == null || Departure == 0 || Freight1.DepartureId == Departure) && (Destination == null || Destination == 0 || Freight1.DestinationId == Destination)
                    select Freight1);
        }
        public IEnumerable<Freight> getAllFreight(String AirlineCode) {
            return (from Freight1 in db.Freights
                    where Freight1.CarrierAirLine.CarrierAirLineName != null && Freight1.CarrierAirLine.CarrierAirLineName.Contains(AirlineCode)
                    select Freight1).ToList();
        }
        public Freight getFreightById(long id) {
            try {
                return db.Freights.Single(f => f.Id == id);
            }
            catch (Exception e) {
                return null;
            }
        }
        public FreightModel getFreightModelById(long id)
        {
            try
            {
                return FreightModel.ConvertFreight(db.Freights.Single(f => f.Id == id));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Freight CreateFreight(FreightModel FreightModel1)
        {
            try {
                Freight Freight1 = new Freight();
                FreightModel.ConvertFreight(FreightModel1,Freight1);
                db.Freights.InsertOnSubmit(Freight1);
                db.SubmitChanges();
                return Freight1;
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e.Message);

                return null;
            }
        }
        public bool UpdateFreight(FreightModel FreightModel1) {
            try
            {
                Freight Freight1 = getFreightById(FreightModel1.Id);
                FreightModel.ConvertFreight(FreightModel1, Freight1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
        public bool DeleteFreight(long id) {
            try
            {
                Freight Freight1 = getFreightById(id);
                db.Freights.DeleteOnSubmit(Freight1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}