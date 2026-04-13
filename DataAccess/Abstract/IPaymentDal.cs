using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPaymentDal:IEntityRepository<Payment>
    { 
        List<Payment> GetByStatus(string status);
         
        List<Payment> GetByDateRange(DateTime startDate, DateTime endDate);
         
        List<Payment> GetByUserId(int userId);
    }
}
