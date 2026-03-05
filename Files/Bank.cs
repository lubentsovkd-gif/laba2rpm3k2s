namespace laba2rpm3k2s.Files
{
    /* 
     * В данном файле был нарушен принцип LSP - класс FixedDepositAccount генерировал исключение, не имеющееся в базовом классе
     * План решения: сделать так, чтобы изначальный метод Withdraw возвращал успешность операции и сообщение об ошибке, если операция не прошла.
    */
    public class WithdrawResult // для этого добавим новый класс, выполняющий эту функцию
    {
        public bool Success { get; }
        public string ErrorMessage { get; }
        private WithdrawResult(string errorMessage, bool success) // приватный конструктор
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        // публично доступные варианты класса
        public static WithdrawResult Fail(string message) => new WithdrawResult(message, false);
        public static WithdrawResult Ok() => new WithdrawResult(null, true);
    }
    public abstract class Account
    {
        public decimal Balance { get; protected set; }

        public virtual void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public abstract WithdrawResult Withdraw(decimal amount);

        public virtual decimal CalculateInterest()
        {
            return Balance * 0.01m;
        }
    }

    // далее во всех классах переписываем метод WithDraw под новые реалии
    public class SavingsAccount : Account
    {
        public decimal MinimumBalance { get; } = 100m;

        public override WithdrawResult Withdraw(decimal amount)
        {
            if (Balance - amount < MinimumBalance)
            {
                return WithdrawResult.Fail("Cannot go below minimum balance");
            }
            Balance -= amount;
            return WithdrawResult.Ok();
        }
    }

    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; } = 500m;

        public override WithdrawResult Withdraw(decimal amount)
        {
            if (Balance - amount < -OverdraftLimit)
            {
                return WithdrawResult.Fail("Overdraft limit exceeded");
            }
            Balance -= amount;
            return WithdrawResult.Ok();
        }
    }

    public class FixedDepositAccount : Account
    {
        public DateTime MaturityDate { get; }

        public FixedDepositAccount(DateTime maturityDate)
        {
            MaturityDate = maturityDate;
        }

        public override WithdrawResult Withdraw(decimal amount)
        {
            if (DateTime.Now < MaturityDate)
            {
                return WithdrawResult.Fail("Cannot withdraw before maturity date");
            }

            if (amount > Balance)
            {
                return WithdrawResult.Fail("Insufficient funds");
            }

            Balance -= amount;
            return WithdrawResult.Ok();
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.05m;
        }
    }

    // Теперь разные производные от Account классы не генерируют исключения, не содержащиеся в основном классе (а точнее говоря вообще не генерируют исключения)
    public class Bank
    {
        public void ProcessWithdrawal(Account account, decimal amount)
        {
            var result = account.Withdraw(amount);
            if (!result.Success) {
                Console.WriteLine($"Withdraw failed: {result.ErrorMessage}");
            }
            else Console.WriteLine($"Successful withdrew {amount}");
        }

        public void Transfer(Account from, Account to, decimal amount)
        {
            var result = from.Withdraw(amount);
            if (!result.Success) 
            {
                Console.WriteLine("Error during withdraw: ", result.ErrorMessage);
                return;
            }
            to.Deposit(amount);
        }
    }
}
