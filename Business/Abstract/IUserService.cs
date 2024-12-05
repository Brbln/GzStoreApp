using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        public void Add(User user);
        public void Update(User user);
        public void Delete(User user);
        List<User> GetAll();
        User GetById(int id); 
        User GetByUserId(int userId);         
        User GetByEmail(string email);
        User GetByUserName(string userName);
    }
}
