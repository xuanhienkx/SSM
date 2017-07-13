using System;
using System.Collections.Generic;
using System.Linq;
using SSM.Common;

using SSM.Models;
using SSM.ViewModels.Shared;

namespace SSM.Services
{
    public interface ICustomerServices : IServices<Customer>
    {
        Customer GetById(long id);
        CustomerModel GetModelById(long id);
        IEnumerable<Customer> GetAll(Customer customer, SSM.Services.SortField sortField, out int totalRows, int page, int pageSize, User user);
        Customer InsertCustomer(CustomerModel model);
        bool UpdateCustomer(CustomerModel model);
        bool DeleteCustomer(long id);
    }

    public class CustomerServices : Services<Customer>, ICustomerServices
    {
        public Customer GetById(long id)
        {
            return FindEntity(x => x.Id == id);
        }

        public CustomerModel GetModelById(long id)
        {
            return ConvertCustomer(GetById(id));
        }

        public IEnumerable<Customer> GetAll(Customer customer, SSM.Services.SortField sortField, out int totalRows, int page, int pageSize, User user)
        {
            customer = customer ?? new Customer();
            var qr = GetQuery(x =>
                (string.IsNullOrEmpty(customer.CompanyName) || x.CompanyName.Contains(customer.CompanyName))
                && (string.IsNullOrEmpty(customer.FullName) || x.FullName.Contains(customer.FullName))
                && (string.IsNullOrEmpty(customer.Address) || x.Address.Contains(customer.Address))
                && (string.IsNullOrEmpty(customer.CustomerType) || x.CustomerType.Equals(customer.CustomerType))
                );
            if (!user.IsAdmin())
            {
                qr = qr.Where(x => x.IsHideUser == false);
            }
            qr = qr.OrderBy(sortField);
            totalRows = qr.Count();
            var list = GetListPager(qr, page, pageSize);
            return list;
        }

        public Customer InsertCustomer(CustomerModel model)
        {
            try
            {
                var customer = ConvertCustomer(model);
                var nCus = (Customer)Insert(customer);
                return nCus;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool UpdateCustomer(CustomerModel model)
        {

            try
            {
                var customer = GetById(model.Id);
                ConvertCustomer(model, customer);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool DeleteCustomer(long id)
        {
            try
            {
                var customer = GetById(id);
                Delete(customer); 
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        private CustomerModel ConvertCustomer(Customer customer)
        {
            var model = new CustomerModel
            {
                Id = customer.Id,
                Address = customer.Address,
                CompanyName = customer.CompanyName,
                Description = customer.Description,
                Email = customer.Email,
                Fax = customer.Fax,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Type = customer.CustomerType,
                UserId = customer.UserId == null ? 0 : customer.UserId.Value,
                IsSee = customer.IsSee,
                IsMove = customer.IsMove,
                CrmCusId = customer.CrmCusId == null ? 0: customer.CrmCusId.Value,
                MovedByUser = customer.User1,
                MovedUserId = customer.MovedBy ?? 0
            };
            return model;
        }
        private Customer ConvertCustomer(CustomerModel customer)
        {
            var dbCus = new Customer
            {
                Address = customer.Address,
                CompanyName = customer.CompanyName,
                Description = customer.Description,
                Email = customer.Email,
                Fax = customer.Fax,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                CustomerType = customer.Type,
                UserId = customer.UserId,
                IsSee = customer.IsSee,
                IsMove = customer.IsMove,
                CrmCusId = customer.CrmCusId,
                MovedBy = customer.MovedUserId
            };
            if (customer.CrmCusId > 0)
            {
                dbCus.CrmCusId = customer.CrmCusId;
            }
            return dbCus;
        }
        private void ConvertCustomer(CustomerModel model, Customer customer)
        {
            customer.Address = model.Address;
            customer.CompanyName = model.CompanyName;
            customer.Description = model.Description;
            customer.Email = model.Email;
            customer.Fax = model.Fax;
            customer.FullName = model.FullName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.CustomerType = model.Type;
            customer.IsMove = model.IsMove;
            customer.IsSee = model.IsSee;
            customer.CrmCusId = model.CrmCusId;
            customer.MovedBy = model.MovedUserId == 0 ? (long?)null : model.MovedUserId;
            if (customer.CrmCusId > 0)
            {
                customer.CrmCusId = model.CrmCusId;
            }
        }
    }
}