namespace laba2rpm3k2s.Files
{
    /*
     * Нарушен принцип ISP – интерфейс раздут.
     * Решение: разделить ответственность на несколько интерфейсов.
    */
    public interface IOrderOperations
    {
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }
    public interface IPaymentProcessing
    {
        void ProcessPayment(Order order);
    }
    public interface IShippingProcessing
    {
        void ShipOrder(Order order);
    }
    public interface IInvoicing
    {
        void GenerateInvoice(Order order);
    }
    public interface INotificationService
    {
        void SendNotification(Order order);
    }
    public interface IReporting
    {
        void GenerateReport(DateTime from, DateTime to);
        void ExportToExcel(string filePath);
    }
    public interface IDatabaseAdministration
    {
        void BackupDatabase();
        void RestoreDatabase();
    }

    public class OrderManager : 
        IOrderOperations,
        IPaymentProcessing,
        IReporting,
        IDatabaseAdministration,
        IInvoicing,
        INotificationService
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine("Order created");
        }

        public void UpdateOrder(Order order)
        {
            Console.WriteLine("Order updated");
        }

        public void DeleteOrder(int orderId)
        {
            Console.WriteLine("Order deleted");
        }

        public void ProcessPayment(Order order)
        {
            Console.WriteLine("Payment processed");
        }

        public void ShipOrder(Order order)
        {
            Console.WriteLine("Order shipped");
        }

        public void GenerateInvoice(Order order)
        {
            Console.WriteLine("Invoice generated");
        }

        public void SendNotification(Order order)
        {
            Console.WriteLine("Notification sent");
        }

        public void GenerateReport(DateTime from, DateTime to)
        {
            Console.WriteLine("Report generated");
        }

        public void ExportToExcel(string filePath)
        {
            Console.WriteLine("Exported to Excel");
        }

        public void BackupDatabase()
        {
            Console.WriteLine("Database backed up");
        }

        public void RestoreDatabase()
        {
            Console.WriteLine("Database restored");
        }
    }

    public class CustomerPortal : IOrderOperations
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine("Order created by customer");
        }

        public void UpdateOrder(Order order)
        {
            Console.WriteLine("Order updated by customer");
        }

        public void DeleteOrder(int orderId)
        {
            Console.WriteLine("Order deleted by customer");
        }
    }
}
