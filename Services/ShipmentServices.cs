using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SSM.Common;

using SSM.Models;
using SSM.Utils;

namespace SSM.Services
{
    public interface ShipmentServices : IServices<Shipment>
    {
        #region Shipment
        IEnumerable<Agent> GetAgentBySOA(SOAModel Model1);
        ShipmentModel ConvertShipment(Shipment Shipment1);
        Shipment GetShipmentById(long Id1);
        ShipmentModel GetShipmentModelById(long Id1);
        IEnumerable<Shipment> GetAllShipment(List<long> UserIds);
        IEnumerable<Shipment> GetAllShipment(ShipmentModel SearchModel, List<long> UserIds);
        IEnumerable<Shipment> GetAllShipment(ShipmentModel SearchModel, User User1, List<long> UserIds, long QuickView);
        IQueryable<Shipment> GetQueryShipment(ShipmentModel SearchModel, User User1, List<long> UserIds, long QuickView);
        IEnumerable<Shipment> GetAllShipment(int skip, int take, List<long> UserIds);
        IEnumerable<Shipment> GetAllShipment(int skip, int take, ShipmentModel ShipmentModel1, List<long> UserIds);
        bool InsertShipment(ShipmentModel ShipmentModel1);
        bool UpdateShipment(ShipmentModel ShipmentModel1);
        void UpdateShipment();
        bool DeleteShipment(long Id1);
        void UpdateMainShipmentStatus(long id);
        IEnumerable<Area> getAllAreaByCountry(long CountryId);
        RevenueModel getRevenueModelById(long id);
        bool UpdateRevenue(RevenueModel RevenueModel1);
        Revenue getRevenueById(long id);
        bool UpdateAny();
        IQueryable<Shipment> getAllUnIssuedInvoice(FindInvoice FindInvoice1);
        int GetStepControlNumber(long id);
        ResultCommand AddMemberToConsol(long id, Shipment menber);
        ResultCommand RemoveMemberOfConsol(long id, Shipment menber);
        #endregion
        #region Invoice
        bool insertArriveNotice(ArriveNotice Notice1);
        ArriveNotice getArriveNotice(long Id);
        ArriveNotice getLastArriveNotice();
        AuthorLetter getLastAuthorLetter();
        bool updateArriveNotice();
        BookingNote getBookingNote(long Id);
        BillLanding getBillLanding(long Id);
        bool updateBookingNote();
        bool updateBillLanding();
        bool insertBillLanding(BillLanding Bill);
        bool insertBookingNote(BookingNote Note);
        bool deleteInvoiceByRev(long RevenueId);
        bool insertInvoice(InvoideIssued Invoice1);
        IQueryable<InvoideIssued> searchInvoice(FindInvoice FindInvoice1);
        IEnumerable<InvoideIssued> getInvoiceByRev(long RevenueId);
        bool deleteSOAInvoice(long RevenueId);
        bool insertSOAInvoice(SOAInvoice SOAInvoice1);
        IEnumerable<SOAInvoice> getSOAInvoice(SOAModel Model1);
        IEnumerable<SOAInvoice> getSOAInvoice(long RevenueId);
        IEnumerable<SOAModel> getAgentSOA(SOAModel Model1);
        bool deleteSOAStatistic(long RevenueId);
        bool insertSOAStatistic(SOAStatistic SOAStatistic1);
        IEnumerable<SOAStatistic> getSOAStatistic(SOAModel Model1);
        IEnumerable<SOAStatistic> getSOAStatistic(long RevenueId);

        DebitNote getDebitNote(long Id);
        bool insertDebitNote(DebitNote Debit);
        BookingConfirm getBookingConfirm(long Id);
        bool insertBookingConfirm(BookingConfirm Book1);
        AuthorLetter getAuthorLetter(long Id);
        bool insertAuthorLetter(AuthorLetter Letter1);
        RequestPayment getRequestPayment(long Id);
        bool insertRequestPayment(RequestPayment Payment);
        BookingRequest getBookingRequest(long Id);
        bool insertBookingRequest(BookingRequest Request);
        Manifest getManifest(long Id);
        bool insertManifest(Manifest Manifest1);
        TransitmentAdvised getTransitmentAdvised(long Id);
        bool insertTransitmentAdvised(TransitmentAdvised TransitmentAdvised1);
        HAirWayBill getHAirWayBill(long Id);
        bool insertHAirWayBill(HAirWayBill HAirWayBill1);
        PaymentVoucher getPaymentVoucher(long Id);
        bool insertPaymentVoucher(PaymentVoucher PaymentVoucher1);
        Shipment GetShipmentByOrder(long idOrder);
        List<Shipment> GetListMemberOfConsol(long id);
        #endregion

        bool ChangeSoaPayment(List<long> ids, bool isPayment);
        void UpdateRevenueOfControl(long id);
    }

    public class ShipmentServicesImpl : Services<Shipment>, ShipmentServices
    {
        private DataClasses1DataContext db;
        private List<Country> _countries;

        public ShipmentServicesImpl()
        {
            db = new DataClasses1DataContext();
        }

        #region AuthorLetter
        public AuthorLetter getAuthorLetter(long Id)
        {
            try
            {
                return db.AuthorLetters.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool insertAuthorLetter(AuthorLetter Letter1)
        {
            try
            {
                db.AuthorLetters.InsertOnSubmit(Letter1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        #endregion

        #region RequestPayment
        public RequestPayment getRequestPayment(long Id)
        {
            try
            {
                return db.RequestPayments.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }

        public bool insertRequestPayment(RequestPayment Payment)
        {
            try
            {
                db.RequestPayments.InsertOnSubmit(Payment);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        #endregion

        #region BookingRequest
        public BookingRequest getBookingRequest(long Id)
        {
            try
            {
                return db.BookingRequests.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool insertBookingRequest(BookingRequest Request)
        {
            try
            {
                db.BookingRequests.InsertOnSubmit(Request);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        #endregion

        #region Manifest
        public Manifest getManifest(long Id)
        {
            try
            {
                return db.Manifests.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool insertManifest(Manifest Manifest1)
        {
            try
            {
                db.Manifests.InsertOnSubmit(Manifest1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }

        public TransitmentAdvised getTransitmentAdvised(long Id)
        {
            try
            {
                return db.TransitmentAdviseds.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }

        public bool insertTransitmentAdvised(TransitmentAdvised TransitmentAdvised1)
        {
            try
            {
                db.TransitmentAdviseds.InsertOnSubmit(TransitmentAdvised1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }

        public HAirWayBill getHAirWayBill(long Id)
        {
            try
            {
                return db.HAirWayBills.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool insertHAirWayBill(HAirWayBill HAirWayBill1)
        {
            try
            {
                db.HAirWayBills.InsertOnSubmit(HAirWayBill1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }

        public PaymentVoucher getPaymentVoucher(long Id)
        {
            try
            {
                return db.PaymentVouchers.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool insertPaymentVoucher(PaymentVoucher PaymentVoucher1)
        {
            try
            {
                db.PaymentVouchers.InsertOnSubmit(PaymentVoucher1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        #endregion

        #region Booking
        public BookingConfirm getBookingConfirm(long Id)
        {
            try
            {
                return db.BookingConfirms.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }

        public bool insertBookingConfirm(BookingConfirm Book1)
        {
            try
            {
                db.BookingConfirms.InsertOnSubmit(Book1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public DebitNote getDebitNote(long Id)
        {
            try
            {
                return db.DebitNotes.FirstOrDefault(d => d.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool insertDebitNote(DebitNote Debit)
        {
            try
            {
                db.DebitNotes.InsertOnSubmit(Debit);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public bool deleteSOAStatistic(long RevenueId)
        {
            try
            {
                IEnumerable<SOAStatistic> SOAList = getSOAStatistic(RevenueId);
                db.SOAStatistics.DeleteAllOnSubmit(SOAList);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public bool insertSOAStatistic(SOAStatistic SOAStatistic1)
        {
            try
            {
                db.SOAStatistics.InsertOnSubmit(SOAStatistic1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public IEnumerable<SOAStatistic> getSOAStatistic(SOAModel Model1)
        {
            try
            {
                return (from Invoice1 in db.SOAStatistics
                        where (Model1.AgentId == 0 || (Invoice1.AgentId == Model1.AgentId))
                         //&& (Model1.DateFrom == "" || (Invoice1.DateUpdate >= DateUtils.Convert2DateTime(Model1.DateFrom)))
                         //&& (Model1.DateTo == "" || (Invoice1.DateUpdate <= DateUtils.Convert2DateTime(Model1.DateTo)))
                         && (Model1.DateFrom == "" || (Invoice1.DateShp >= DateUtils.Convert2DateTime(Model1.DateFrom)))
                        && (Model1.DateTo == "" || (Invoice1.DateShp <= DateUtils.Convert2DateTime(Model1.DateTo)))

                        select Invoice1);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public IEnumerable<SOAStatistic> getSOAStatistic(long RevenueId)
        {
            try
            {
                return (from Invoice1 in db.SOAStatistics
                        where Invoice1.ShipmentId == RevenueId
                        select Invoice1);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public IEnumerable<Agent> GetAgentBySOA(SOAModel Model1)
        {
            Nullable<DateTime> From = StringUtils.isNullOrEmpty(Model1.DateFrom) ? null : (Nullable<DateTime>)DateUtils.Convert2DateTime(Model1.DateFrom);
            Nullable<DateTime> To = StringUtils.isNullOrEmpty(Model1.DateTo) ? null : (Nullable<DateTime>)DateUtils.Convert2DateTime(Model1.DateTo);
            try
            {
                IEnumerable<long> listAgentIds = (from Invoice1 in db.SOAStatistics
                                                      // where (From == null || Invoice1.DateUpdate >= From)
                                                      // && (To == null || Invoice1.DateUpdate <= To)
                                                  where (From == null || Invoice1.DateShp >= From)
                                                  && (To == null || Invoice1.DateShp <= To)
                                                  select Invoice1.AgentId).Distinct();

                return from Agent1 in db.Agents
                       where listAgentIds.Contains(Agent1.Id)
                       select Agent1;

            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public IEnumerable<SOAModel> getAgentSOA(SOAModel Model1)
        {
            DateTime? From = StringUtils.isNullOrEmpty(Model1.DateFrom) ? null : (DateTime?)DateUtils.Convert2DateTime(Model1.DateFrom);
            DateTime? To = StringUtils.isNullOrEmpty(Model1.DateTo) ? null : (DateTime?)DateUtils.Convert2DateTime(Model1.DateTo);
            try
            {
                return (from Invoice1 in db.SOAStatistics
                        where Invoice1.AgentId == Model1.AgentId
                         //&& (From == null || Invoice1.DateUpdate >= From)
                         //&& (To == null || Invoice1.DateUpdate <= To)
                         && (From == null || Invoice1.DateShp >= From)
                        && (To == null || Invoice1.DateShp <= To)
                        //&& (Invoice1.IsPayment == Model1.IsPayment)
                        group Invoice1 by new { Invoice1.Currency, Invoice1.TypeNote }
                            into _group
                        select new SOAModel(_group.Key.Currency, _group.Key.TypeNote, Convert.ToDouble(_group.Sum(i => i.Amount))
                     ));
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool deleteSOAInvoice(long RevenueId)
        {
            try
            {
                IEnumerable<SOAInvoice> SOAList = getSOAInvoice(RevenueId);
                db.SOAInvoices.DeleteAllOnSubmit(SOAList);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public bool insertSOAInvoice(SOAInvoice SOAInvoice1)
        {
            try
            {
                db.SOAInvoices.InsertOnSubmit(SOAInvoice1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public IEnumerable<SOAInvoice> getSOAInvoice(long RevenueId)
        {
            try
            {
                return (from Invoice1 in db.SOAInvoices
                        where Invoice1.RevenueId == RevenueId
                        select Invoice1);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public IEnumerable<SOAInvoice> getSOAInvoice(SOAModel Model1)
        {
            try
            {
                return (from Invoice1 in db.SOAInvoices
                        where (Model1.AgentId == 0 || (Invoice1.AgentId == Model1.AgentId))
                        && (Model1.DateFrom == "" || (Invoice1.DateShp.Value >= DateUtils.Convert2DateTime(Model1.DateFrom)))
                        && (Model1.DateTo == "" || (Invoice1.DateShp.Value <= DateUtils.Convert2DateTime(Model1.DateTo)))
                        && (!Model1.IsPayment || Invoice1.IsPayment == false)
                        select Invoice1);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public IEnumerable<InvoideIssued> getInvoiceByRev(long RevenueId)
        {
            try
            {
                return (from Invoice1 in db.InvoideIssueds
                        where Invoice1.Revenue.Id == RevenueId
                        select Invoice1);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool deleteInvoiceByRev(long RevenueId)
        {
            try
            {
                IEnumerable<InvoideIssued> Invoices = getInvoiceByRev(RevenueId);
                db.InvoideIssueds.DeleteAllOnSubmit(Invoices);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public bool insertInvoice(InvoideIssued Invoice1)
        {
            try
            {
                db.InvoideIssueds.InsertOnSubmit(Invoice1);
                return true;
            }
            catch (Exception e) { Logger.LogError(e); }
            return false;
        }
        public IQueryable<InvoideIssued> searchInvoice(FindInvoice findInvoice)
        {
            var results = db.InvoideIssueds.Where(iv =>
                 (string.IsNullOrEmpty(findInvoice.InvoiceNo) || (iv.InvoiceNo != null && iv.InvoiceNo.Contains(findInvoice.InvoiceNo)))
                 && (findInvoice.ShipmentId == 0 || findInvoice.ShipmentId == iv.ShipmentId)
                 );
            if (findInvoice.ShipmentPriod == 1)
            {
                results = results.Where(iv => (string.IsNullOrEmpty(findInvoice.DateFrom) ||
                                         iv.Shipment.DateShp >= DateUtils.Convert2DateTime(findInvoice.DateFrom))
                                        &&
                                        (string.IsNullOrEmpty(findInvoice.DateTo) ||
                                         iv.Shipment.DateShp <= DateUtils.Convert2DateTime(findInvoice.DateTo)));

            }
            else
            {
                results = results.Where(iv => (string.IsNullOrEmpty(findInvoice.DateFrom) ||
                                         iv.Date >= DateUtils.Convert2DateTime(findInvoice.DateFrom))
                                        &&
                                        (string.IsNullOrEmpty(findInvoice.DateTo) ||
                                         iv.Date <= DateUtils.Convert2DateTime(findInvoice.DateTo)));
            }
            var list = results.OrderByDescending(i => i.Date);
            return list;


            //try
            //{
            //    if (FindInvoice1.ShipmentPriod == 1)
            //    {
            //        results = from Invoice1 in db.InvoideIssueds
            //                  where (Invoice1.InvoiceNo != null && Invoice1.InvoiceNo.Contains(invoiceNo))
            //                  select Invoice1;

            //        if (!FindInvoice1.DateFrom.Trim().Equals(""))
            //        {
            //            results = results.Where(s => s.Shipment.DateShp >= DateUtils.Convert2DateTime(FindInvoice1.DateFrom));
            //        }
            //        if (!FindInvoice1.DateTo.Trim().Equals(""))
            //        {
            //            results = results.Where(s => s.Shipment.DateShp <= DateUtils.Convert2DateTime(FindInvoice1.DateTo));
            //        }
            //    }
            //    else
            //    {
            //        results = from Invoice1 in db.InvoideIssueds
            //                  where (Invoice1.InvoiceNo != null && Invoice1.InvoiceNo.Contains(invoiceNo))
            //                  select Invoice1;
            //        if (!FindInvoice1.DateFrom.Trim().Equals(""))
            //        {
            //            results = results.Where(s => s.Date >= DateUtils.Convert2DateTime(FindInvoice1.DateFrom));
            //        }
            //        if (!FindInvoice1.DateTo.Trim().Equals(""))
            //        {
            //            results = results.Where(s => s.Date <= DateUtils.Convert2DateTime(FindInvoice1.DateTo));
            //        }
            //    }
            //    return results.OrderByDescending(x => x.Date);
            //}
            //catch (Exception e)
            //{
            //    Logger.LogError(e);
            //}
            //return null;
        }
        public BookingNote getBookingNote(long Id)
        {
            try
            {
                return db.BookingNotes.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public BillLanding getBillLanding(long Id)
        {
            try
            {
                return db.BillLandings.FirstOrDefault(b => b.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool updateBookingNote()
        {
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;

        }
        public bool updateBillLanding()
        {
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;

        }
        public bool insertBillLanding(BillLanding Bill)
        {
            try
            {
                db.BillLandings.InsertOnSubmit(Bill);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;

        }
        public bool insertBookingNote(BookingNote Note)
        {
            try
            {
                db.BookingNotes.InsertOnSubmit(Note);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;

        }
        public ArriveNotice getArriveNotice(long Id)
        {
            try
            {
                return db.ArriveNotices.FirstOrDefault(a => a.Id == Id);
            }
            catch (Exception e) { Logger.LogError(e); }
            return null;
        }
        public ArriveNotice getLastArriveNotice()
        {
            try
            {
                return db.ArriveNotices.OrderByDescending(m => m.Id).First();
            }
            catch (Exception e) { Logger.LogError(e); }
            return new ArriveNotice();
        }
        public AuthorLetter getLastAuthorLetter()
        {
            try
            {
                return db.AuthorLetters.OrderByDescending(m => m.Id).First();
            }
            catch (Exception e) { Logger.LogError(e); }
            return new AuthorLetter();
        }
        public bool updateArriveNotice()
        {
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }

        public int GetStepControlNumber(long id)
        {
            var data = db.Shipments.Where(x => x.IsControl && x.ShipmentRef == id).OrderByDescending(x => x.ControlStep).FirstOrDefault();
            if (data != null)
            {
                return data.ControlStep ?? 0 + 1;
            }
            return 0;
        }

        public ResultCommand AddMemberToConsol(long id, Shipment menber)
        {
            var consol = GetShipmentById(id);
            if (consol == null)
            {
                return new ResultCommand()
                {
                    IsFinished = false,
                    Message = "Consol not found with ref " + id
                };
            }
            try
            {
                menber.IsControl = true;
                menber.ShipmentRef = consol.Id;
                UpdateShipment();
                UpdateSubByMainShipment(consol);
                UpdateRevenueOfControl(consol.Id);
                UpdateMainShipmentStatus(consol.Id);
                UpdateShipment();
                var message = string.Format("Add ref {0} to consol {1} successfully!!", menber.Id, consol.Id);
                Logger.Log(message);
                return new ResultCommand()
                {
                    Message = message,
                    IsFinished = true
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new ResultCommand()
                {
                    IsFinished = false,
                    Message = string.Format("Add ref {0} to consol {1} failure. Error: {2}", menber.Id, consol.Id, ex.Message)
                };
            }
        }

        public ResultCommand RemoveMemberOfConsol(long id, Shipment menber)
        {
            var consol = GetShipmentById(id);
            if (consol == null)
            {
                return new ResultCommand()
                {
                    IsFinished = false,
                    Message = "Consol not found with ref " + id
                };
            }
            try
            {
                menber.IsControl = false;
                menber.ShipmentRef = null;
                UpdateShipment();
                UpdateSubByMainShipment(consol);
                UpdateRevenueOfControl(consol.Id);
                UpdateMainShipmentStatus(consol.Id);
                UpdateShipment();
                var message = string.Format("Remove ref {0} from consol {1} successfully!!", menber.Id, consol.Id);
                Logger.Log(message);
                return new ResultCommand()
                {
                    Message = message,
                    IsFinished = true
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new ResultCommand()
                {
                    IsFinished = false,
                    Message = string.Format("remove ref {0} from consol {1} failure. Error: {2}", menber.Id, consol.Id, ex.Message)
                };
            }
        }

        public bool insertArriveNotice(ArriveNotice Notice1)
        {
            try
            {
                db.ArriveNotices.InsertOnSubmit(Notice1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }

        public bool UpdateAny()
        {
            try
            {

                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public bool UpdateRevenue(RevenueModel RevenueModel1)
        {
            try
            {
                Revenue Revenue1 = getRevenueById(RevenueModel1.Id);
                var shipment = GetShipmentById(RevenueModel1.Id);
                if (Revenue1 != null)
                {
                    RevenueModel.ConvertModel(RevenueModel1, Revenue1);
                }
                else
                {
                    Revenue1 = new Revenue();
                    RevenueModel.ConvertModel(RevenueModel1, Revenue1);
                    db.Revenues.InsertOnSubmit(Revenue1);
                    db.SubmitChanges();
                    Revenue1 = getRevenueById(RevenueModel1.Id);

                }
                if (shipment.IsMainShipment == true)
                {
                    Revenue1.IsControl = true;

                }
                if (Revenue1.Shipment.ShipmentRef != null)
                {
                    UpdateRevenueOfControl(Revenue1.Shipment.ShipmentRef.Value);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public RevenueModel getRevenueModelById(long id)
        {
            RevenueModel RevenueModel1 = null;
            Revenue Revenue1 = null;
            try
            {
                Revenue1 = db.Revenues.FirstOrDefault(rv => rv.Id == id);
            }
            catch (Exception e)
            {
                Revenue1 = null;
            }
            if (Revenue1 != null)
            {
                RevenueModel1 = RevenueModel.ConvertModel(Revenue1);
            }
            return RevenueModel1;
        }
        public Revenue getRevenueById(long id)
        {
            Revenue Revenue1 = null;
            try
            {
                Revenue1 = db.Revenues.FirstOrDefault(rv => rv.Id == id);
            }
            catch (Exception e)
            {
                Revenue1 = null;
            }
            return Revenue1;
        }
        #endregion

        #region Shipment
        public IQueryable<Shipment> getAllUnIssuedInvoice(FindInvoice FindInvoice1)
        {
            var list = db.Shipments.Where(s => s.IsInvoiced == false
                                                   && ShipmentModel.RevenueStatusCollec.Submited.ToString().Equals(s.RevenueStatus)
                                                   && s.Revenue.InvoiceCustom > 0
                                                   && (FindInvoice1.ShipmentId == 0 || FindInvoice1.ShipmentId == s.Id)
                                                   && (string.IsNullOrEmpty(FindInvoice1.DateFrom) || s.DateShp.Value >= DateUtils.Convert2DateTime(FindInvoice1.DateFrom))
                                                   && (string.IsNullOrEmpty(FindInvoice1.DateTo) || s.DateShp.Value <= DateUtils.Convert2DateTime(FindInvoice1.DateTo))
                    ).OrderByDescending(s => s.DateShp);
            return list;

        }
        public Shipment GetShipmentByOrder(long idOrder)
        {
            return db.Shipments.FirstOrDefault(b => b.VoucherId == idOrder);
        }

        public List<Shipment> GetListMemberOfConsol(long id)
        {
            var list = GetQuery(x => !x.IsMainShipment && x.ShipmentRef == id).ToList();
            return list;
        }

        public bool ChangeSoaPayment(List<long> ids, bool isPayment)
        {
            if (ids.Count <= 0) return true;
            foreach (var soa in ids.Select(id => db.SOAInvoices.FirstOrDefault(x => x.Id == id)).Where(soa => soa != null))
            {
                soa.IsPayment = isPayment;

            }
            db.SubmitChanges();
            return true;
        }
        #region comvert and mapping model
        public ShipmentModel ConvertShipment(Shipment Shipment1)
        {
            ShipmentModel ShipmentModel1 = new ShipmentModel();
            ShipmentModel1.Id = Shipment1.Id;
            ShipmentModel1.DepartmentId = Shipment1.DeptId;
            ShipmentModel1.AgentId = Shipment1.AgentId != null ? Shipment1.AgentId.Value : 0;
            ShipmentModel1.AgentName = Shipment1.Agent != null ? Shipment1.Agent.AgentName : "";
            ShipmentModel1.CneeId = Shipment1.CneeId != null ? Shipment1.CneeId.Value : 0;
            ShipmentModel1.CneeName = Shipment1.Customer != null ? Shipment1.Customer.CompanyName : "";
            ShipmentModel1.CneeFullName = Shipment1.Customer != null ? Shipment1.Customer.FullName : "";
            ShipmentModel1.Dateshp = Shipment1.DateShp != null ? Shipment1.DateShp.Value.ToString("dd/MM/yyyy") : "";
            ShipmentModel1.CreateDate = Shipment1.CreateDate != null ? Shipment1.CreateDate.Value.ToString("dd/MM/yyyy") : "";
            ShipmentModel1.UpdateDate = Shipment1.UpdateDate != null ? Shipment1.UpdateDate.Value.ToString("dd/MM/yyyy") : "";
            ShipmentModel1.LockDate = Shipment1.LockDate != null ? Shipment1.LockDate.Value.ToString("dd/MM/yyyy") : "";
            ShipmentModel1.LockShipment = Shipment1.IsLock != null && Shipment1.IsLock.Value;
            ShipmentModel1.Description = Shipment1.Description;
            ShipmentModel1.QtyName = Shipment1.QtyName;
            ShipmentModel1.QtyNumber = Shipment1.QtyNumber;
            ShipmentModel1.QtyUnit = Shipment1.QtyUnit;
            ShipmentModel1.RevenueStatus = Shipment1.RevenueStatus;
            ShipmentModel1.SaleId = Shipment1.SaleId != null ? Shipment1.SaleId.Value : 0;
            ShipmentModel1.SaleName = Shipment1.User != null ? Shipment1.User.FullName : "";
            ShipmentModel1.SaleType = Shipment1.SaleType;
            ShipmentModel1.ServiceName = Shipment1.ServiceName;
            ShipmentModel1.ShipperId = Shipment1.ShipperId != null ? Shipment1.ShipperId.Value : 0;
            ShipmentModel1.ShipperName = Shipment1.Customer1 != null ? Shipment1.Customer1.FullName : "";
            ShipmentModel1.CompanyId = Shipment1.CompanyId != null ? Shipment1.CompanyId.Value : 0;
            ShipmentModel1.CompanyName = Shipment1.Company != null ? Shipment1.Company.CompanyName : "";
            ShipmentModel1.DepartureId = Shipment1.DepartureId != null ? Shipment1.DepartureId.Value : 0;
            ShipmentModel1.DepartureName = Shipment1.Area != null ? Shipment1.Area.AreaAddress : "";
            ShipmentModel1.DestinationId = Shipment1.DestinationId != null ? Shipment1.DestinationId.Value : 0;
            ShipmentModel1.DestinationName = Shipment1.Area1 != null ? Shipment1.Area1.AreaAddress : "";
            ShipmentModel1.HouseNum = Shipment1.HouseNum;
            ShipmentModel1.MasterNum = Shipment1.MasterNum;
            ShipmentModel1.SFreights = Shipment1.SFreights;
            ShipmentModel1.CarrierAirId = Shipment1.CarrierAirId != null ? Shipment1.CarrierAirId.Value : 0;
            ShipmentModel1.CarrierAirName = Shipment1.CarrierAirLine != null ? Shipment1.CarrierAirLine.CarrierAirLineName : "";
            ShipmentModel1.isDelivered = Shipment1.isDelivered;
            ShipmentModel1.DeliveredDate = Shipment1.DeliveredDate != null ? Shipment1.DeliveredDate.Value.ToString("dd/MM/yyyy") : "";
            ShipmentModel1.HouseNumCheck = string.IsNullOrEmpty(Shipment1.HouseNum) ||
                                           Shipment1.HouseNum.ToUpper().Equals("CHUA BILL");
            ShipmentModel1.MasterNumCheck = string.IsNullOrEmpty(Shipment1.MasterNum) ||
                                           Shipment1.MasterNum.ToUpper().Equals("CHUA BILL");
            ShipmentModel1.ServiceId = Shipment1.ServiceId;
            ShipmentModel1.TypeServices = Shipment1.ServicesType;
            //Get Order
            if (Shipment1.VoucherId != null)
            {
                ShipmentModel1.Order = Shipment1.MT81;
                ShipmentModel1.VoucherId = Shipment1.VoucherId ?? 0;
                ShipmentModel1.IsTrading = true;
            }
            else
            {
                ShipmentModel1.IsTrading = false;
            }
            // Main controll
            ShipmentModel1.UserListInControl = new List<long>();
            ShipmentModel1.IsMainControl = Shipment1.IsMainShipment;
            ShipmentModel1.ShipmentRef = Shipment1.ShipmentRef;
            ShipmentModel1.IsControl = Shipment1.IsControl;
            ShipmentModel1.ControlStep = Shipment1.ControlStep;
            if (ShipmentModel1.IsMainControl)
            {
                ShipmentModel1.UserListInControlDb = Shipment1.UserListInControl;
                if (!string.IsNullOrEmpty(Shipment1.UserListInControl))
                    ShipmentModel1.UserListInControl = Shipment1.UserListInControl.Split(';').Select(s => long.Parse(s)).ToList();
                else
                    ShipmentModel1.UserListInControl = new List<long>();
            }

            return ShipmentModel1;
        }
        private Shipment ConvertShipment(ShipmentModel ShipmentModel1)
        {
            Shipment Shipment1 = new Shipment();
            Shipment1.DeptId = ShipmentModel1.DepartmentId;
            Shipment1.AgentId = ShipmentModel1.AgentId;
            Shipment1.CneeId = ShipmentModel1.CneeId;
            Shipment1.DateShp = DateUtils.Convert2DateTime(ShipmentModel1.Dateshp);
            Shipment1.Description = ShipmentModel1.Description;
            Shipment1.QtyName = ShipmentModel1.QtyName;
            Shipment1.QtyNumber = ShipmentModel1.QtyNumber;
            Shipment1.QtyUnit = ShipmentModel1.QtyUnit;
            Shipment1.SaleId = ShipmentModel1.SaleId;
            Shipment1.SaleType = ShipmentModel1.SaleType;
            Shipment1.ShipperId = ShipmentModel1.ShipperId;
            Shipment1.CompanyId = ShipmentModel1.CompanyId;
            Shipment1.DepartureId = ShipmentModel1.DepartureId;
            Shipment1.DestinationId = ShipmentModel1.DestinationId;
            Shipment1.HouseNum = ShipmentModel1.HouseNum;
            Shipment1.MasterNum = ShipmentModel1.MasterNum;
            Shipment1.SFreights = ShipmentModel1.SFreights;
            Shipment1.CarrierAirId = ShipmentModel1.CarrierAirId;
            Shipment1.isDelivered = ShipmentModel1.isDelivered;
            Shipment1.RevenueStatus = ShipmentModel1.RevenueStatus;
            Shipment1.DeliveredDate = DateUtils.Convert2DateTime(ShipmentModel1.DeliveredDate);
            Shipment1.ServiceId = ShipmentModel1.ServiceId;
            var servicesType = db.ServicesTypes.FirstOrDefault(x => x.Id == Shipment1.ServiceId);
            if (servicesType != null)
            {
                var serivceName = servicesType.SerivceName;
                if (serivceName != null)
                    Shipment1.ServiceName = serivceName;
            }
            //Add shippment for order 
            Shipment1.VoucherId = ShipmentModel1.VoucherId;
            Shipment1.IsTrading = ShipmentModel1.IsTrading;

            // Main shipment control
            Shipment1.IsMainShipment = ShipmentModel1.IsMainControl;
            if (ShipmentModel1.IsMainControl && ShipmentModel1.UserListInControl.Any())
            {
                Shipment1.UserListInControl = string.Join(";", ShipmentModel1.UserListInControl);
            }
            Shipment1.ShipmentRef = ShipmentModel1.ShipmentRef;
            Shipment1.IsControl = ShipmentModel1.IsControl;
            Shipment1.ControlStep = ShipmentModel1.ControlStep;
            return Shipment1;
        }
        private void ConvertShipment(ShipmentModel ShipmentModel1, Shipment Shipment1)
        {
            Shipment1.DeptId = ShipmentModel1.DepartmentId;
            Shipment1.AgentId = ShipmentModel1.AgentId;
            Shipment1.CneeId = ShipmentModel1.CneeId;
            Shipment1.DateShp = DateUtils.Convert2DateTime(ShipmentModel1.Dateshp);
            Shipment1.Description = ShipmentModel1.Description;
            Shipment1.QtyName = ShipmentModel1.QtyName;
            Shipment1.QtyNumber = ShipmentModel1.QtyNumber;
            Shipment1.QtyUnit = ShipmentModel1.QtyUnit;
            Shipment1.SaleId = ShipmentModel1.SaleId;
            Shipment1.SaleType = ShipmentModel1.SaleType;
            Shipment1.ServiceName = ShipmentModel1.ServiceName;
            Shipment1.ShipperId = ShipmentModel1.ShipperId;
            Shipment1.CompanyId = ShipmentModel1.CompanyId;
            Shipment1.DepartureId = ShipmentModel1.DepartureId;
            Shipment1.DestinationId = ShipmentModel1.DestinationId;
            Shipment1.HouseNum = ShipmentModel1.HouseNum;
            Shipment1.MasterNum = ShipmentModel1.MasterNum;
            Shipment1.SFreights = ShipmentModel1.SFreights;
            Shipment1.CarrierAirId = ShipmentModel1.CarrierAirId;
            Shipment1.RevenueStatus = ShipmentModel1.RevenueStatus;
            Shipment1.isDelivered = ShipmentModel1.isDelivered;
            Shipment1.DeliveredDate = DateUtils.Convert2DateTime(ShipmentModel1.DeliveredDate);
            Shipment1.IsLock = ShipmentModel1.LockShipment;
            Shipment1.VoucherId = ShipmentModel1.VoucherId;
            Shipment1.IsTrading = ShipmentModel1.IsTrading;
            Shipment1.ShipmentRef = ShipmentModel1.ShipmentRef;
            Shipment1.IsMainShipment = ShipmentModel1.IsMainControl;
            Shipment1.ServiceId = ShipmentModel1.ServiceId;
            var servicesType = db.ServicesTypes.FirstOrDefault(x => x.Id == Shipment1.ServiceId);
            if (servicesType != null)
                Shipment1.ServiceName = servicesType.SerivceName;
            ShipmentModel1.IsControl = Shipment1.IsControl;
            ShipmentModel1.ControlStep = Shipment1.ControlStep;
            if (ShipmentModel1.UserListInControl != null && ShipmentModel1.UserListInControl.Any())
                Shipment1.UserListInControl = string.Join(";", ShipmentModel1.UserListInControl);
        }
        #endregion
        #region Shipment Gets
        public Shipment GetShipmentById(long Id1)
        {
            try
            {
                return db.Shipments.FirstOrDefault(e => e.Id == Id1);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }

        public ShipmentModel GetShipmentModelById(long Id1)
        {
            try
            {
                return ConvertShipment(db.Shipments.FirstOrDefault(e => e.Id == Id1));
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }

        public IEnumerable<Shipment> GetAllShipment(List<long> UserIds)
        {
            try
            {
                return (from Shipment1 in db.Shipments
                        where UserIds.Contains(Shipment1.SaleId.Value)
                        select Shipment1);
            }
            catch (Exception e) { Logger.LogError(e); }
            return null;
        }
        public IEnumerable<Shipment> GetAllShipment(ShipmentModel SearchModel, User user, List<long> UserIds, long QuickView)
        {
            if (SearchModel.SaleId != 0)
            {
                UserIds = new List<long> { SearchModel.SaleId };
            }
            try
            {
                {
                    IEnumerable<Shipment> shipments = (from shipment1 in db.Shipments
                                                       where
                                                           (string.IsNullOrEmpty(SearchModel.RevenueStatus) ||
                                                            shipment1.RevenueStatus == SearchModel.RevenueStatus)
                                                           &&
                                                           (string.IsNullOrEmpty(SearchModel.SaleType) || shipment1.SaleType == SearchModel.SaleType)
                                                           && (SearchModel.ServiceId == 0 || shipment1.ServiceId == SearchModel.ServiceId)
                                                           && (SearchModel.ShipperId == 0 || shipment1.ShipperId.Value == SearchModel.ShipperId)
                                                           && (SearchModel.AgentId == 0 || shipment1.AgentId.Value == SearchModel.AgentId)
                                                           && (SearchModel.AgentId == 0 || shipment1.AgentId.Value == SearchModel.AgentId)
                                                           && (SearchModel.CarrierAirId == 0 || shipment1.CarrierAirId.Value == SearchModel.CarrierAirId)
                                                           && (SearchModel.CneeId == 0 || shipment1.CneeId.Value == SearchModel.CneeId)
                                                           && (string.IsNullOrEmpty(SearchModel.HouseNum) || shipment1.HouseNum.Contains(SearchModel.HouseNum))
                                                           && (string.IsNullOrEmpty(SearchModel.MasterNum) || shipment1.MasterNum.Contains(SearchModel.MasterNum))
                                                           && (SearchModel.Id == 0 || shipment1.Id == SearchModel.Id)
                                                           && (UserIds.Count == 0 || UserIds.Contains(shipment1.SaleId.Value))
                                                           || (user.IsOperations() &&
                                                               (user.AllowTrackAirIn &&
                                                                shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.AirInbound.ToString()))
                                                               ||
                                                               (user.AllowTrackAirOut &&
                                                                shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.AirOutbound.ToString()))
                                                               ||
                                                               (user.AllowTrackSeaIn &&
                                                                shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.SeaInbound.ToString()))
                                                               ||
                                                               (user.AllowTrackSeaOut &&
                                                                shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.SeaOutbound.ToString()))
                                                               ||
                                                               (user.AllowTrackInlandSer &&
                                                                shipment1.ServicesType.SerivceName.Equals(
                                                                    ShipmentModel.Services.InlandService.ToString()))
                                                               ||
                                                               (user.AllowTrackProjectSer &&
                                                                shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.Other.ToString()))
                                                               )
                                                       select shipment1);
                    if (QuickView == 1)
                    {
                        DateTime ToDay = DateTime.Now;
                        DateTime DateFrom =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(ToDay.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(ToDay.Month.ToString()) + "/" + ToDay.Year);
                        //ToDay = ToDay.AddDays(1);
                        DateTime DateTo =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(ToDay.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(ToDay.Month.ToString()) + "/" + ToDay.Year);
                        shipments = shipments.Where(m => m.DateShp >= DateFrom && m.DateShp <= DateTo);
                    }
                    else if (QuickView == 2)
                    {
                        DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
                        DateTime dateFrom =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(monday.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(monday.Month.ToString()) + "/" + monday.Year);
                        monday = monday.AddDays(6);
                        DateTime dateTo =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(monday.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(monday.Month.ToString()) + "/" + monday.Year);
                        shipments = shipments.Where(m => m.DateShp >= dateFrom && m.DateShp <= dateTo);
                    }
                    else if (QuickView == 3)
                    {
                        DateTime toDay = DateTime.Now;
                        toDay = toDay.AddDays(1);
                        DateTime dateTo =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(toDay.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(toDay.Month.ToString()) + "/" + toDay.Year);
                        shipments = shipments.Where(m => m.DateShp >= dateTo);
                    }
                    if (!user.IsTrading())
                    {
                        shipments = shipments.Where(m => m.VoucherId == null);
                    }

                    return shipments;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }


        }

        public IQueryable<Shipment> GetQueryShipment(ShipmentModel SearchModel, User user, List<long> UserIds, long QuickView)
        {
            if (SearchModel.SaleId != 0)
            {
                UserIds = new List<long> { SearchModel.SaleId };
            }
            var shipments = db.Shipments.Where(shipment1 =>
                        (string.IsNullOrEmpty(SearchModel.RevenueStatus) || shipment1.RevenueStatus == SearchModel.RevenueStatus)
                        && (string.IsNullOrEmpty(SearchModel.SaleType) || shipment1.SaleType == SearchModel.SaleType)
                        && (SearchModel.ServiceId == 0 || shipment1.ServiceId == SearchModel.ServiceId)
                        && (SearchModel.ShipperId == 0 || shipment1.ShipperId.Value == SearchModel.ShipperId)
                        && (SearchModel.AgentId == 0 || shipment1.AgentId.Value == SearchModel.AgentId)
                        && (SearchModel.AgentId == 0 || shipment1.AgentId.Value == SearchModel.AgentId)
                        && (SearchModel.CarrierAirId == 0 || shipment1.CarrierAirId.Value == SearchModel.CarrierAirId)
                        && (SearchModel.CneeId == 0 || shipment1.CneeId.Value == SearchModel.CneeId)
                        && (string.IsNullOrEmpty(SearchModel.HouseNum) || shipment1.HouseNum.Contains(SearchModel.HouseNum))
                        && (string.IsNullOrEmpty(SearchModel.MasterNum) || shipment1.MasterNum.Contains(SearchModel.MasterNum))
                        && (SearchModel.Id == 0 || shipment1.Id == SearchModel.Id)

                        );
            if (SearchModel.SaleId != 0)
            {
                shipments = shipments.Where(shipment1 => shipment1.SaleId.Value == SearchModel.SaleId);
            }
            else if (user.IsOperations())
            {
                shipments = shipments.Where(shipment1 =>
                      (UserIds.Count == 0 || UserIds.Contains(shipment1.SaleId.Value))
                        ||
                      (
                        (user.AllowTrackAirIn == true && shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.AirInbound.ToString()))
                        ||
                        (user.AllowTrackAirOut == true && shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.AirOutbound.ToString()))
                        ||
                        (user.AllowTrackSeaIn == true && shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.SeaInbound.ToString()))
                        ||
                        (user.AllowTrackSeaOut == true && shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.SeaOutbound.ToString()))
                        ||
                        (user.AllowTrackInlandSer == true && shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.InlandService.ToString()))
                        ||
                        (user.AllowTrackProjectSer == true && shipment1.ServicesType.SerivceName.Equals(ShipmentModel.Services.Other.ToString()))
                     ));
            }
            else
            {
                shipments = shipments.Where(shipment1 =>
                    (UserIds.Count == 0 || UserIds.Contains(shipment1.SaleId.Value)));
            }

            switch (QuickView)
            {
                case 1:
                    {
                        DateTime toDay = DateTime.Now;
                        DateTime dateFrom =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(toDay.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(toDay.Month.ToString()) + "/" + toDay.Year);
                        //ToDay = ToDay.AddDays(1);
                        DateTime dateTo =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(toDay.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(toDay.Month.ToString()) + "/" + toDay.Year);
                        shipments = shipments.Where(m => m.DateShp >= dateFrom && m.DateShp <= dateTo);
                    }
                    break;
                case 2:
                    {
                        DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
                        DateTime dateFrom =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(monday.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(monday.Month.ToString()) + "/" + monday.Year);
                        monday = monday.AddDays(6);
                        DateTime dateTo =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(monday.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(monday.Month.ToString()) + "/" + monday.Year);
                        shipments = shipments.Where(m => m.DateShp >= dateFrom && m.DateShp <= dateTo);
                    }
                    break;
                case 3:
                    {
                        DateTime toDay = DateTime.Now;
                        toDay = toDay.AddDays(1);
                        DateTime dateTo =
                            DateUtils.Convert2DateTime(DateUtils.ConvertDay(toDay.Day.ToString()) + "/" +
                                                       DateUtils.ConvertDay(toDay.Month.ToString()) + "/" + toDay.Year);
                        shipments = shipments.Where(m => m.DateShp >= dateTo);
                    }
                    break;
            }
            if (!user.IsTrading())
            {
                shipments = shipments.Where(m => m.VoucherId == null);
            }
            return shipments.AsQueryable();
        }

        public IEnumerable<Shipment> GetAllShipment(ShipmentModel SearchModel, List<long> UserIds)
        {
            if (SearchModel.SaleId != 0)
            {
                UserIds = new List<long> { SearchModel.SaleId };
            }
            try
            {
                return (from shipment1 in db.Shipments
                        where (string.IsNullOrEmpty(SearchModel.RevenueStatus) || shipment1.RevenueStatus == SearchModel.RevenueStatus)
                              && (UserIds.Count == 0 || UserIds.Contains(shipment1.SaleId.Value))
                              && (string.IsNullOrEmpty(SearchModel.SaleType) || shipment1.SaleType == SearchModel.SaleType)
                              && (SearchModel.ServiceId == 0 || shipment1.ServiceName == SearchModel.ServiceName)
                              && ((SearchModel.ShipperId == 0) || shipment1.ShipperId.Value == SearchModel.ShipperId)
                              && ((SearchModel.AgentId == 0) || shipment1.AgentId.Value == SearchModel.AgentId)
                              && ((SearchModel.CarrierAirId == 0) || shipment1.CarrierAirId.Value == SearchModel.CarrierAirId)
                              && ((SearchModel.CneeId == 0) || shipment1.CneeId.Value == SearchModel.CneeId)
                              && (string.IsNullOrEmpty(SearchModel.HouseNum) || (shipment1.HouseNum != null && shipment1.HouseNum.Contains(SearchModel.HouseNum)))
                              && (string.IsNullOrEmpty(SearchModel.MasterNum) || (shipment1.MasterNum != null && shipment1.MasterNum.Contains(SearchModel.MasterNum)))
                              && (SearchModel.Id == 0 || shipment1.Id == SearchModel.Id)
                        select shipment1);
            }
            catch (Exception e) { Logger.LogError(e); }
            return null;
        }

        public IEnumerable<Shipment> GetAllShipment(int skip, int take, List<long> UserIds)
        {
            try
            {
                return (from Shipment1 in db.Shipments
                        where UserIds.Contains(Shipment1.SaleId.Value)
                        select Shipment1).Skip(skip).Take(take);
            }
            catch (Exception e) { Logger.LogError(e); }
            return null;
        }

        public IEnumerable<Shipment> GetAllShipment(int skip, int take, ShipmentModel SearchModel, List<long> UserIds)
        {
            try
            {
                if (SearchModel != null)
                {
                    return (from Shipment1 in db.Shipments
                            where ((SearchModel.RevenueStatus == null || SearchModel.RevenueStatus.Equals("")) || Shipment1.RevenueStatus == SearchModel.RevenueStatus)
                            && (UserIds.Contains(Shipment1.SaleId.Value))
                            && ((SearchModel.SaleType == null || SearchModel.SaleType.Equals("")) || Shipment1.SaleType == SearchModel.SaleType)
                            && (SearchModel.ServiceId == 0 || Shipment1.ServiceId == SearchModel.ServiceId)
                            && ((SearchModel.ShipperId == null) || (SearchModel.ShipperId == 0) || Shipment1.ShipperId.Value == SearchModel.ShipperId)
                            select Shipment1).Skip(skip).Take(take);
                }
                else
                {
                    return (from Shipment1 in db.Shipments
                            where UserIds.Contains(Shipment1.SaleId.Value)
                            select Shipment1);
                }
            }
            catch (Exception e) { Logger.LogError(e); }
            return null;
        }
        #endregion
        #region Shipment sets
        public bool InsertShipment(ShipmentModel ShipmentModel1)
        {
            try
            {
                Shipment Shipment1 = ConvertShipment(ShipmentModel1);
                Shipment1.CreateDate = DateTime.Now;
                Shipment1.LockDate = DateTime.Now.AddDays(60);
                Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString();
                db.Shipments.InsertOnSubmit(Shipment1);
                db.SubmitChanges();
                long id = Shipment1.Id;

                if (ShipmentModel1.IsMainControl)
                {
                    Shipment1.ShipmentRef = id;
                    CreateShipmentchildOfMainControl(id, ShipmentModel1);
                    UpdateShipment();
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                e.GetBaseException();
                return false;
            }
        }
        private void CreateShipmentchildOfMainControl(long shipId, ShipmentModel ShipmentModel1)
        {
            if (ShipmentModel1 == null || !ShipmentModel1.IsMainControl || !ShipmentModel1.UserListInControl.Any())
                return;
            var step = GetStepControlNumber(shipId);
            foreach (var userId in ShipmentModel1.UserListInControl)
            {
                Shipment Shipment1 = ConvertShipment(ShipmentModel1);
                Shipment1.IsMainShipment = false;
                Shipment1.ShipmentRef = shipId;
                Shipment1.UserListInControl = null;
                Shipment1.CreateDate = DateTime.Now;
                Shipment1.LockDate = DateTime.Now.AddDays(60);
                Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString();
                Shipment1.SaleId = userId;
                Shipment1.IsControl = true;
                Shipment1.ControlStep = step;
                db.Shipments.InsertOnSubmit(Shipment1);
                step++;
            }
            db.SubmitChanges();
        }
        public bool UpdateShipment(ShipmentModel ShipmentModel1)
        {
            Shipment Shipment1 = GetShipmentById(ShipmentModel1.Id);
            if (Shipment1 == null) return false;
            var userList = new List<long>();
            if (ShipmentModel1.IsMainControl && !string.IsNullOrEmpty(Shipment1.UserListInControl))
                userList = Shipment1.UserListInControl.Split(';').Select(s => long.Parse(s)).ToList();
            ConvertShipment(ShipmentModel1, Shipment1);
            Shipment1.UpdateDate = DateTime.Now;
            if (Shipment1.IsControl && !Shipment1.IsMainShipment)
                UpdateMainShipmentStatus(Shipment1.ShipmentRef.Value);
            db.SubmitChanges();

            if (!ShipmentModel1.IsMainControl) return true;

            if (ShipmentModel1.UserListInControl == null)
                ShipmentModel1.UserListInControl = new List<long>();

            var newUserList = ShipmentModel1.UserListInControl.ToList();
            var newList = userList.Aggregate(newUserList.ToList(), (l, e) => { l.Remove(e); return l; }).ToArray(); // setB - setA
            var delLiset = newUserList.Aggregate(userList.ToList(), (l, e) => { l.Remove(e); return l; }).ToArray(); // setB - setA


            //var delLiset = userList.Except(newUserList).ToList();
            //var newList = newUserList.Except(userList).ToList();
            //if (!delLiset.Any() && !newList.Any() && userList.Count == newUserList.Count) return true;
            if (delLiset.Any())
            {

                foreach (var idUser in delLiset)
                {
                    var shipmentSub =
                        db.Shipments.FirstOrDefault(
                            x =>
                                x.ShipmentRef.Value == Shipment1.Id && x.SaleId.Value == idUser && x.IsMainShipment == false &&
                                !x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString()));
                    if (shipmentSub != null)
                        db.Shipments.DeleteOnSubmit(shipmentSub);
                    UpdateShipment();
                }
            }

            //if (!newList.Any() && !delLiset.Any() && newUserList.Any()) newList = newUserList;
            if (newList.Any())
            {
                ShipmentModel1.UserListInControl = newList;
                CreateShipmentchildOfMainControl(ShipmentModel1.Id, ShipmentModel1);
            }
            UpdateSubByMainShipment(Shipment1);
            UpdateShipment();
            UpdateMainShipmentStatus(Shipment1.Id);
            return true;
        }

        public void UpdateSubByMainShipment(Shipment Shipment1)
        {
            var shipsub = db.Shipments.Where(x => x.ShipmentRef == Shipment1.Id && x.IsMainShipment == false).ToList();
            if (shipsub.Any())
            {
                var userincontrol = shipsub.Select(x => x.SaleId != null ? x.SaleId.Value : 0);
                Shipment1.UserListInControl = string.Join(";", userincontrol.Where(x => x != 0).ToList());
                foreach (var item in shipsub)
                {
                    item.DateShp = Shipment1.DateShp;
                    item.CarrierAirId = Shipment1.CarrierAirId;
                    item.DepartureId = Shipment1.DepartureId;
                    item.DestinationId = Shipment1.DestinationId;
                    item.AgentId = Shipment1.AgentId;
                    item.ServiceId = Shipment1.ServiceId;
                    item.ServiceName = Shipment1.ServiceName;
                    item.MasterNum = Shipment1.MasterNum;
                }
                Shipment1.HouseNum = string.Format(@"has {0} hbl", shipsub.Count);
            }
            else
            {
                Shipment1.UserListInControl = null;
                Shipment1.HouseNum = string.Format(@"has {0} hbl", shipsub.Count);
            }
        }
        public void UpdateShipment()
        {
            db.SubmitChanges();
        }
        public bool DeleteShipment(long Id1)
        {
            Shipment Shipment1 = GetShipmentById(Id1);
            if (Shipment1 != null &&
                !Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString()))
            {
                try
                {

                    if (Shipment1.IsMainShipment == true)
                    {
                        var listShipments = db.Shipments.Where(x => x.ShipmentRef == Shipment1.Id && x.IsMainShipment == false
                            && !x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString())
                            ).ToList();
                        db.Shipments.DeleteAllOnSubmit(listShipments);
                        UpdateShipment();
                        listShipments = db.Shipments.Where(x => x.ShipmentRef == Shipment1.Id && x.IsMainShipment == false).ToList();
                        UpdateSubByMainShipment(Shipment1);
                        UpdateMainShipmentStatus(Id1);
                        if (listShipments.Count <= 0)
                        {
                            db.Shipments.DeleteOnSubmit(Shipment1);
                            UpdateShipment();
                        }
                        UpdateRevenueOfControl(Shipment1.Id);

                        return true;
                    }
                    else if (Shipment1.ShipmentRef != null)
                    {
                        db.Shipments.DeleteOnSubmit(Shipment1);
                        UpdateShipment();
                        var mainShipment = db.Shipments.FirstOrDefault(x => x.Id == Shipment1.ShipmentRef && x.IsMainShipment == true);
                        if (mainShipment != null)
                        {
                            var userList = mainShipment.UserListInControl.Split(';').Select(s => long.Parse(s)).ToList();
                            userList.Remove(Shipment1.SaleId.Value);
                            userList = userList.Where(x => x != 0).ToList();
                            mainShipment.UserListInControl = string.Join(";", userList);
                            mainShipment.HouseNum = string.Format(@"has {0} hbl", userList.Count);
                            UpdateShipment();
                            UpdateRevenueOfControl(mainShipment.Id);
                        }
                        UpdateMainShipmentStatus(Id1);
                        return true;
                    }
                    else
                    {
                        db.Shipments.DeleteOnSubmit(Shipment1);
                        UpdateShipment();
                        return true;
                    }

                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }

            }
            else
            {
                if (Shipment1 == null)
                {
                    Logger.LogError(string.Format(@" shipment Id {0} không tồn tại", Id1));
                }
                else
                {
                    Logger.LogError(string.Format(@"Không xoá được shipment Id {0} với status {1}", Shipment1.Id, Shipment1.RevenueStatus));
                }

                return false;
            }
            return true;
        }
        public void UpdateMainShipmentStatus(long id)
        {
            var ships = db.Shipments.Where(x => x.ShipmentRef == id && x.IsMainShipment == false).ToList();
            var mainShip = GetShipmentById(id);
            if (mainShip == null) return;
            mainShip.RevenueStatus = ships.All(x =>
                x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString())) ?
                ShipmentModel.RevenueStatusCollec.Approved.ToString()
                : ShipmentModel.RevenueStatusCollec.Pending.ToString();
            UpdateShipment();
        }
        public void UpdateRevenueOfControl(long id)
        {
            var revenueControl = getRevenueById(id);
            var revenues =
                db.Revenues.Where(
                    x => x.Shipment != null && x.Shipment.ShipmentRef == id && x.Shipment.IsMainShipment == false)
                    .ToList();
            if (!revenues.Any()) return;
            var first = revenues.FirstOrDefault();
            var isNew = false;
            if (revenueControl == null)
            {
                revenueControl = new Revenue()
                {
                    Id = id,
                    IsControl = true,
                    ARate = first.ARate,
                    AccInv1 = first.AccInv1,
                    AccInv2 = first.AccInv2,
                    AccInv3 = first.AccInv3,
                    AccInv4 = first.AccInv4,
                    AccInvDate1 = first.AccInvDate1,
                    AccInvDate2 = first.AccInvDate2,
                    AccInvDate3 = first.AccInvDate3,
                    AccInvDate4 = first.AccInvDate4,
                };
                isNew = true;
            }
            revenueControl.Income = revenues.Sum(x => x.Income ?? 0);
            revenueControl.INVI = revenues.Sum(x => x.INVI ?? 0);
            revenueControl.INOS = revenues.Sum(x => x.INOS ?? 0);
            revenueControl.INTransportRate = revenues.Sum(x => x.INTransportRate ?? 0);
            revenueControl.INTransportRate_OS = revenues.Sum(x => x.INTransportRate_OS ?? 0);
            revenueControl.INInlandService = revenues.Sum(x => x.INInlandService ?? 0);
            revenueControl.INInlandService_OS = revenues.Sum(x => x.INInlandService_OS ?? 0);
            revenueControl.INCreditDebit = revenues.Sum(x => x.INCreditDebit ?? 0);
            revenueControl.INCreditDebit_OS = revenues.Sum(x => x.INCreditDebit_OS ?? 0);
            revenueControl.INDocumentFee = revenues.Sum(x => x.INDocumentFee ?? 0);
            revenueControl.INDocumentFee_OS = revenues.Sum(x => x.INDocumentFee_OS ?? 0);
            revenueControl.INHandingFee = revenues.Sum(x => x.INHandingFee ?? 0);
            revenueControl.INHandingFee_OS = revenues.Sum(x => x.INHandingFee_OS ?? 0);
            revenueControl.INTHC = revenues.Sum(x => x.INTHC ?? 0);
            revenueControl.INCFS = revenues.Sum(x => x.INCFS ?? 0);
            revenueControl.INAutoValue1 = revenues.Sum(x => x.INAutoValue1 ?? 0);
            revenueControl.INAutoValue1_OS = revenues.Sum(x => x.INAutoValue1_OS ?? 0);
            revenueControl.INAutoValue2 = revenues.Sum(x => x.INAutoValue2 ?? 0);
            revenueControl.INAutoValue2_OS = revenues.Sum(x => x.INAutoValue2_OS ?? 0);
            revenueControl.Expense = revenues.Sum(x => x.Expense ?? 0);
            revenueControl.EXVI = revenues.Sum(x => x.EXVI ?? 0);
            revenueControl.EXOS = revenues.Sum(x => x.EXOS ?? 0);
            revenueControl.EXTransportRate = revenues.Sum(x => x.EXTransportRate ?? 0);
            revenueControl.EXTransportRate_OS = revenues.Sum(x => x.EXTransportRate_OS ?? 0);
            revenueControl.EXInlandService = revenues.Sum(x => x.EXInlandService ?? 0);
            revenueControl.EXInlandService_OS = revenues.Sum(x => x.EXInlandService_OS ?? 0);
            revenueControl.EXCommision2Shipper = revenues.Sum(x => x.EXCommision2Shipper ?? 0);
            revenueControl.EXCommision2Carrier = revenues.Sum(x => x.EXCommision2Carrier ?? 0);
            revenueControl.EXTax = revenues.Sum(x => x.EXTax ?? 0);
            revenueControl.EXCreditDebit = revenues.Sum(x => x.EXCreditDebit ?? 0);
            revenueControl.EXCreditDebit_OS = revenues.Sum(x => x.EXCreditDebit_OS ?? 0);
            revenueControl.EXDocumentFee = revenues.Sum(x => x.EXDocumentFee ?? 0);
            revenueControl.EXHandingFee = revenues.Sum(x => x.EXHandingFee ?? 0);
            revenueControl.EXTHC = revenues.Sum(x => x.EXTHC ?? 0);
            revenueControl.EXCFS = revenues.Sum(x => x.EXCFS ?? 0);
            revenueControl.EXManualValue1 = revenues.Sum(x => x.EXManualValue1 ?? 0);
            revenueControl.EXmanualValue1_OS = revenues.Sum(x => x.EXmanualValue1_OS ?? 0);
            revenueControl.ExManualValue2 = revenues.Sum(x => x.ExManualValue2 ?? 0);
            revenueControl.EXmanualValue2_OS = revenues.Sum(x => x.EXmanualValue2_OS ?? 0);
            revenueControl.Earning = revenues.Sum(x => x.Earning ?? 0);
            revenueControl.EarningVI = revenues.Sum(x => x.EarningVI ?? 0);
            revenueControl.EarningOS = revenues.Sum(x => x.EarningOS);
            revenueControl.EarningOS = revenues.Sum(x => x.EarningOS);
            revenueControl.InvAmount1 = revenues.Sum(x => x.InvAmount1);
            revenueControl.InvAmount2 = revenues.Sum(x => x.InvAmount2 ?? 0);
            revenueControl.OUTAutoValue1 = revenues.Sum(x => x.OUTAutoValue1 ?? 0);
            revenueControl.OUTAutoValue1 = revenues.Sum(x => x.OUTAutoValue1 ?? 0);
            revenueControl.EXAutoValue1_OS = revenues.Sum(x => x.EXAutoValue1_OS ?? 0);
            revenueControl.EXAutoValue2_OS = revenues.Sum(x => x.EXAutoValue2_OS ?? 0);
            revenueControl.InvoiceCustom = revenues.Sum(x => x.InvoiceCustom ?? 0);
            revenueControl.IsControl = true;
            if (isNew)
            {
                db.Revenues.InsertOnSubmit(revenueControl);
            }
            db.SubmitChanges();
        }
        #endregion
        #endregion

        #region Area Services
        public IEnumerable<Area> getAllAreaByCountry(long CountryId)
        {
            return (from Area1 in db.Areas
                    where Area1.IsSee == true && Area1.IsHideUser == false
                    where Area1.CountryId.Value == CountryId
                    select Area1);
        }
        #endregion


    }

}