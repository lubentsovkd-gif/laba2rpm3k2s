using System;
using System.Collections.Generic;
using System.Text;

namespace laba2rpm3k2s.Files
{
    using laba2rpm3k2s.Files;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    /*
     * Нарушен принцип SRP – один класс реализует слишком много разных возможностей
     * Решение: разделение на классы с единственной ответственностью
     */
    // классы, лишь описывающие функционал
    public class ReceiptGenerator
    {
        public void GenerateReceipt(Order order) { }
    }
    public class InventoryManager
    {
        public void UpdateInventory(List<string> items) { }
    }
    public class MailService
    {
        public void SendEmail(string to, string message) { }
    }
    public class Logger
    {
        public void LogToDatabase(string message) { }

        public void LogToFile(string message) { }
    }
    public class PaymentProcessor
    {
        public void ProcessPayment(string paymentMethod, decimal amount) { }
    }
    public class OrderRepository // класс для хранения заказов
    {
        private List<Order> orders = new List<Order>();

        public void AddOrder(Order order)
        {
            orders.Add(order);
            Console.WriteLine($"Order {order.Id} added");
        }

        public Order GetOrder(int orderId)
        {
            return orders.FirstOrDefault(o => o.Id == orderId);
        }

        public List<Order> GetAllOrders()
        {
            return orders.ToList();
        }
    }
    
    public class ReportGenerator
    {
        public void GenerateMonthlyReport(List<Order> orders)
        {
            decimal totalRevenue = orders.Sum(o => o.TotalAmount);
            int totalOrders = orders.Count;
            Console.WriteLine($"Monthly Report: {totalOrders} orders, Revenue: {totalRevenue:C}");
        }
    }
    public class ExcelExporter
    {
        public void ExportToExcel(List<Order> orders, string filePath)
        {
            Console.WriteLine($"Exporting orders to {filePath}");
        }
    }

    // Основной класс обработки заказов (теперь с одной ответственностью)
    public class OrderProcessor
    {
        private readonly OrderRepository _orderRepository;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly InventoryManager _inventoryManager;
        private readonly MailService _mailService;
        private readonly Logger _logger;
        private readonly ReceiptGenerator _receiptGenerator;

        public OrderProcessor(
            OrderRepository orderRepository,
            PaymentProcessor paymentProcessor,
            InventoryManager inventoryManager,
            MailService mailService,
            Logger logger,
            ReceiptGenerator receiptGenerator)
        {
            _orderRepository = orderRepository;
            _paymentProcessor = paymentProcessor;
            _inventoryManager = inventoryManager;
            _mailService = mailService;
            _logger = logger;
            _receiptGenerator = receiptGenerator;
        }

        public void ProcessOrder(int orderId)
        {
            var order = _orderRepository.GetOrder(orderId);
            if (order != null)
            {
                Console.WriteLine($"Processing order {orderId}");

                if (order.TotalAmount <= 0)
                    throw new Exception("Invalid order amount");

                _paymentProcessor.ProcessPayment(order.Payment.Method, order.TotalAmount);
                _inventoryManager.UpdateInventory(order.Items);
                _mailService.SendEmail(order.Customer.Email, $"Order {orderId} processed");
                _logger.LogToDatabase($"Order {orderId} processed at {DateTime.Now}");
                _receiptGenerator.GenerateReceipt(order);
            }
        }
    }
}
