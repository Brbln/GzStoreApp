using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<User, GamzeDbConcext>, IUserDal
    {
        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
