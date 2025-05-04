using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPaymentDal : EfRepositoryBase<Payment, GamzeDbContext>, IPaymentDal
    {
        public List<Payment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
