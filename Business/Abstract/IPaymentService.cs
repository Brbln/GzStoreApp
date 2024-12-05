using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        public void Add(Payment payment);
        public void Update(Payment payment);
        public void Delete(Payment payment);
        List<Payment> GetAll();
        Payment GetById(int id);
        List<Payment> GetByStatus(string status);
        List<Payment> GetByDateRange(DateTime startDate, DateTime endDate);
        List<Payment> GetByUserId(int userId);
    }
}
