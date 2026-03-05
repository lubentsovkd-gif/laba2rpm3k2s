namespace laba2rpm3k2s.Files
{
    /*
     * Грубо нарушается принцип OCP (аж глаза вытекают)
     * Решение: использовать классы вместо if-else и switch-case конструкций.
    */
    public class DiscountCalculator
    {
        public decimal CalculateDiscount(Customer customer, decimal orderAmount)
        {
            try { return orderAmount * customer.Discount; }
            catch { return 0; }
        }
    }
    public abstract class Customer
    {
        public abstract decimal Discount { get; }
    }

    public class Regular : Customer
    {
        public override decimal Discount => 0.05m;
    }

    public class Premium : Customer
    {
        public override decimal Discount => 0.10m;
    }

    public class VIP : Customer
    {
        public override decimal Discount => 0.15m;
    }

    public class Student : Customer
    {
        public override decimal Discount => 0.08m;
    }

    public class Senior : Customer
    {
        public override decimal Discount => 0.07m;
    }
    public class ShippingCalculator
    {
        public decimal CalculateShippingCost(ShippingMethod method, decimal weight)
        {
            return method.CalculateCost(weight);
        }
    }
    public abstract class ShippingMethod
    {
        public abstract decimal CalculateCost(decimal weight);
    }

    public class StandardShipping : ShippingMethod
    {
        public override decimal CalculateCost(decimal weight) => 5.00m + weight * 0.5m;
    }

    public class ExpressShipping : ShippingMethod
    {
        public override decimal CalculateCost(decimal weight) => 15.00m + weight * 1.0m;
    }

    public class OvernightShipping : ShippingMethod
    {
        public override decimal CalculateCost(decimal weight) => 25.00m + weight * 2.0m;
    }
    public class InternationalShipping : ShippingMethod
    {
        private readonly string _destination;

        public InternationalShipping(string destination)
        {
            _destination = destination;
        }
        public override decimal CalculateCost(decimal weight)
        {
            decimal baseCost = _destination switch
            {
                "USA" => 30.00m,
                "Europe" => 35.00m,
                "Asia" => 40.00m,
                _ => 50.00m
            };
            return baseCost;
        }
    }
}
