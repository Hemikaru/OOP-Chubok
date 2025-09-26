using System;

namespace Lab4v6
{
    // Інтерфейс транзакції
    public interface ITransaction
    {
        void Execute(BankAccount account, decimal amount);
    }

    // Абстрактний клас рахунку
    public abstract class BankAccount
    {
        public string Owner { get; set; }
        public decimal Balance { get; set; }

        public BankAccount(string owner, decimal balance)
        {
            Owner = owner;
            Balance = balance;
        }

        public abstract void PrintInfo();
    }

    // Реалізація звичайного рахунку
    public class StandardAccount : BankAccount
    {
        public StandardAccount(string owner, decimal balance) : base(owner, balance) { }

        public override void PrintInfo()
        {
            Console.WriteLine($"Власник: {Owner}, Баланс: {Balance} грн");
        }
    }

    // Операція поповнення
    public class Deposit : ITransaction
    {
        public void Execute(BankAccount account, decimal amount)
        {
            account.Balance += amount;
            Console.WriteLine($"Поповнення на {amount} грн. Новий баланс: {account.Balance} грн");
        }
    }

    // Операція зняття
    public class Withdraw : ITransaction
    {
        public void Execute(BankAccount account, decimal amount)
        {
            if (account.Balance >= amount)
            {
                account.Balance -= amount;
                Console.WriteLine($"Знято {amount} грн. Новий баланс: {account.Balance} грн");
            }
            else
            {
                Console.WriteLine($"❌ Недостатньо коштів для зняття {amount} грн! Баланс: {account.Balance} грн");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            BankAccount account = new StandardAccount("Іван", 500);
            account.PrintInfo();

            ITransaction deposit = new Deposit();
            ITransaction withdraw = new Withdraw();

            deposit.Execute(account, 300);   // поповнення
            withdraw.Execute(account, 200);  // зняття
            withdraw.Execute(account, 2000); // перевищення ліміту
        }
    }
}
