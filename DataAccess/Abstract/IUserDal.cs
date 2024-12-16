using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        
        // Kullanıcı bilgilerini e-posta ile getirir
        User GetByEmail(string email);

        // Kullanıcı bilgilerini kullanıcı adı ile getirir
        User GetByUserName(string userName);
    }
}
