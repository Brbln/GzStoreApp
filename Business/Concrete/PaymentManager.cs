using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentdal;
        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentdal = paymentDal;
        }
        public void Add(Payment payment)
        {
            payment.PaymentDate = DateTime.Now;
            payment.Status = "Success";
            _paymentdal.Add(payment);
        }

        public void Delete(Payment payment)
        {
            _paymentdal.Delete(payment);
        }

        public List<Payment> GetAll()
        {
            return _paymentdal.GetAll();
        }

        public List<Payment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                Console.WriteLine("Başlangıç tarihi bitiş tarihinden önce olmalıdır.");
                return new List<Payment>(); // boş liste dön
            }
            return _paymentdal.GetAll(a => a.PaymentDate >= startDate && a.PaymentDate <= endDate);
        }

        public Payment GetById(int id)
        {
            return _paymentdal.Get(a => a.PaymentId == id);
        }

        public List<Payment> GetByStatus(string status)
        {
            return _paymentdal.GetAll(a => a.Status == status);
        }

        public List<Payment> GetByUserId(int userId)
        {
            return _paymentdal.GetAll(a => a.Order.UserId == userId);
        }

        public void Update(Payment payment)
        {
            _paymentdal.Update(payment);
        }
    }
}
