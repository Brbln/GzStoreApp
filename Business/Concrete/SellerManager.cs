using Business.Abstract;
using Core.Utilities.Security;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SellerManager : ISellerService
    {
        ISellerDal _sellerDal;

        public SellerManager(ISellerDal sellerDal)
        {
            _sellerDal = sellerDal;
        }

        public void Add(Seller seller)
        {
            seller.PasswordHash = HashHelper.Hash(seller.PasswordHash);
            _sellerDal.Add(seller);
        }

        public void Delete(Seller seller)
        {
            _sellerDal.Delete(seller);  
        }

        public List<Seller> GetAll()
        {
           return _sellerDal.GetAll();
        }

        public Seller GetById(int id)
        {
            return _sellerDal.Get(a=>a.Id ==id);
        }

        public void Update(Seller seller)
        {
            if (!string.IsNullOrWhiteSpace(seller.PasswordHash))
            {
                seller.PasswordHash = HashHelper.Hash(seller.PasswordHash);
            }
            _sellerDal.Update(seller);
        }

        public Seller? ValidateSeller(string email, string password)
        {
            var seller = _sellerDal.Get(e => e.Email == email);
            if (seller == null) return null;
            return VerifyPassword(password, seller.PasswordHash) ? seller : null;
        }
        private bool VerifyPassword(string password, string passwordHash)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashed = Convert.ToBase64String(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            return hashed == passwordHash;
        }
    }
}
