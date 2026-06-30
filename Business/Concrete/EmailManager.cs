using Business.Abstract;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System;

namespace Business.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailManager(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"];
            _fromEmail = configuration["SendGrid:FromEmail"];
            _fromName = configuration["SendGrid:FromName"];
        }

        private async Task SendEmail(string toEmail, string subject, string htmlContent)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                throw new Exception($"Mail gönderilemedi. StatusCode: {response.StatusCode}, Detail: {errorBody}");
            }
        }

        public async Task SendPasswordResetEmail(string toEmail, string resetCode)
        {
            var html = $@"
                <div style='font-family: Arial, sans-serif; max-width: 480px; margin: 0 auto;'>
                    <h2 style='color: #3f2e24;'>Şifre Sıfırlama</h2>
                    <p>Şifrenizi sıfırlamak için aşağıdaki kodu kullanın:</p>
                    <div style='background: #fef3c7; padding: 16px; border-radius: 8px; text-align: center; margin: 16px 0;'>
                        <span style='font-size: 28px; font-weight: 700; letter-spacing: 4px; color: #92400e;'>{resetCode}</span>
                    </div>
                    <p style='color: #888; font-size: 13px;'>Bu kod 15 dakika içinde geçerliliğini kaybedecektir.</p>
                    <p style='color: #888; font-size: 13px;'>Bu talebi siz yapmadıysanız bu e-postayı yok sayabilirsiniz.</p>
                </div>";

            await SendEmail(toEmail, "Şifre Sıfırlama Kodu", html);
        }

        public async Task SendOrderConfirmationEmail(string toEmail, int orderId, decimal totalAmount)
        {
            var html = $@"
                <div style='font-family: Arial, sans-serif; max-width: 480px; margin: 0 auto;'>
                    <h2 style='color: #3f2e24;'>Siparişiniz Alındı 🎉</h2>
                    <p>Sipariş numaranız: <strong>#{orderId}</strong></p>
                    <p>Toplam tutar: <strong>{totalAmount:F2} ₺</strong></p>
                    <p>Ödemeniz onaylandıktan sonra siparişiniz hazırlanmaya başlayacaktır.</p>
                </div>";

            await SendEmail(toEmail, $"Siparişiniz Alındı - #{orderId}", html);
        }

        public async Task SendOrderStatusEmail(string toEmail, int orderId, string newStatus, string? trackingNumber = null)
        {
            var statusText = newStatus switch
            {
                "Processing" => "Hazırlanıyor",
                "Shipped" => "Kargoya Verildi",
                "Delivered" => "Teslim Edildi",
                "Cancelled" => "İptal Edildi",
                _ => newStatus
            };

            var trackingHtml = !string.IsNullOrWhiteSpace(trackingNumber)
        ? $@"<div style='background: #fef3c7; padding: 14px; border-radius: 8px; margin: 12px 0;'>
                <p style='margin:0; font-size:13px; color:#92400e;'>Kargo Takip Numarası</p>
                <p style='margin:4px 0 0; font-size:18px; font-weight:700; color:#3f2e24;'>{trackingNumber}</p>
            </div>"
        : "";

            var html = $@"
        <div style='font-family: Arial, sans-serif; max-width: 480px; margin: 0 auto;'>
            <h2 style='color: #3f2e24;'>Sipariş Durumu Güncellendi</h2>
            <p>Sipariş #{orderId} durumu: <strong>{statusText}</strong></p>
            {trackingHtml}
        </div>";

            await SendEmail(toEmail, $"Sipariş Durumu - #{orderId}", html);
        }
        public async Task SendPaymentConfirmedEmail(string toEmail, int orderId)
        {
            var html = $@"
        <div style='font-family: Arial, sans-serif; max-width: 480px; margin: 0 auto;'>
            <h2 style='color: #16a34a;'>Ödemeniz Onaylandı ✓</h2>
            <p>Sipariş #{orderId} için ödemeniz onaylandı.</p>
            <p>Siparişiniz şimdi hazırlanmaya başlıyor.</p>
        </div>";

            await SendEmail(toEmail, $"Ödeme Onaylandı - #{orderId}", html);
        }

        public async Task SendPaymentRejectedEmail(string toEmail, int orderId, string? reason = null)
        {
            var reasonHtml = !string.IsNullOrWhiteSpace(reason)
                ? $"<p style='color:#888; font-size:13px;'>Sebep: {reason}</p>"
                : "";

            var html = $@"
        <div style='font-family: Arial, sans-serif; max-width: 480px; margin: 0 auto;'>
            <h2 style='color: #dc2626;'>Ödeme Onaylanamadı</h2>
            <p>Sipariş #{orderId} için ödemeniz onaylanamadı ve sipariş iptal edildi.</p>
            {reasonHtml}
            <p>Sorularınız için bize ulaşabilirsiniz.</p>
        </div>";

            await SendEmail(toEmail, $"Ödeme Sorunu - #{orderId}", html);
        }
    }
}