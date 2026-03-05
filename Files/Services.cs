namespace laba2rpm3k2s.Files
{
    using System;
    /*
     * Нарушение принципа DIP – разные классы реализуют схожий функционал, вместо использования абстракций, а класс OrderSender создаёт зависимости
     * Решение: создать обобщающий интерфейс, убрать зависимости
     * Каюсь, смухлевал – в EmailService теперь нет шапки.
     */
    public interface Sender
    {
        public void Send(string recipient, string message);
    }
    public class EmailService : Sender
    {
        public void Send(string to, string subject)
        {
            Console.WriteLine($"Sending email to {to}: {subject}");
        }
    }

    public class SmsService : Sender
    {
        public void Send(string phoneNumber, string message)
        {
            Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
        }
    }

    public class OrderService
    {
        public OrderService() { }
        public void PlaceOrder(Order order, Sender sender)
        {
            sender.Send(order.Customer.Email, "Your order has been placed");
        }
    }

    public class NotificationService
    {
        public NotificationService() { }

        public void SendPromotion(string email, string promotion, Sender sender)
        {
            sender.Send(email, promotion);
        }
    }
}
