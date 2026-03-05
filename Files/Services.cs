namespace laba2rpm3k2s.Files
{
    using System;
    /*
     * Нарушение принципа DIP – классы создают зависимости вместо того, чтобы получать их извне
     * Решение: изменить классы OrderService, NotificationService так, чтобы они получали зависимости через методы
     */
    public class EmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Sending email to {to}: {subject}");
        }
    }

    public class SmsService
    {
        public void SendSms(string phoneNumber, string message)
        {
            Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
        }
    }

    public class OrderService
    {
        public OrderService() { }
        public void PlaceOrder(Order order, SmsService sms, EmailService email)
        {
            email.SendEmail(order.Customer.Email, "Order Confirmation", "Your order has been placed");
            sms.SendSms(order.Customer.Phone, "Your order has been placed");
        }
    }

    public class NotificationService
    {
        public NotificationService() { }

        public void SendPromotion(string email, string promotion, EmailService service)
        {
            service.SendEmail(email, "Special Promotion", promotion);
        }
    }
}
