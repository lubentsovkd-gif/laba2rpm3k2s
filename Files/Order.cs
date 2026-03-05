namespace laba2rpm3k2s.Files
{
    /*
     * Нарушен принцип SRP – класс Order описывает как данные заказа, так и данные оплаты/клиента
     * Решение: разделить данные на разные классы
    */
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public List<string> Items { get; set; }
        public BankCustomer Customer { get; set; }
        public PaymentInfo Payment { get; set; }
    }

    public class BankCustomer // не Customer, потому что в предыдущих файлах уже был
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class PaymentInfo
    {
        public string Method { get; set; }
    }
}
