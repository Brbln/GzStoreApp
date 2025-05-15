using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<User, GamzeDbContext>, IUserDal
    {

        public User GetById(int id)
        {
            return Get(u => u.UserId == id);
        }
        public User GetByEmail(string email)
        {
            return Get(u => u.Email.ToLower() == email.ToLower());
        }
        public User GetByUserName(string userName)
        {
            return Get(u => u.UserName.ToLower() == userName.ToLower());
        }

        public bool IsEmailExists(string email)
        {
            return GetAll(u => u.Email.ToLower() == email.ToLower()).Any();
        }

        public bool IsUNameExists(string uName)
        {
            return GetAll(u => u.UserName.ToLower() == uName.ToLower()).Any();
        }
    }
}
