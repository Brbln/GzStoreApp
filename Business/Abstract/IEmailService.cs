using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string toEmail, string resetCode);
        Task SendOrderConfirmationEmail(string toEmail, int orderId, decimal totalAmount);
        Task SendOrderStatusEmail(string toEmail, int orderId, string newStatus);
    }
}
