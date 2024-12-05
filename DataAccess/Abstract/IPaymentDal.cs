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
        // Belirli bir ödeme durumuna göre ödeme listesi getirir (örneğin, "Başarılı", "Başarısız")
        List<Payment> GetByStatus(string status);

        // Belirli bir tarih aralığında yapılan ödemeleri getirir
        List<Payment> GetByDateRange(DateTime startDate, DateTime endDate);

        // Kullanıcının yaptığı ödemeleri getirir (eğer UserId varsa, ilişkili bir User ile ödeme yapılmışsa)
        List<Payment> GetByUserId(int userId);
    }
}
