using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    public class PremiumAccountTests
    {
        [TestCase("44444", "Premium Account", 100, AccountType.Free, 250, false)]
        [TestCase("44444", "Premium Account", 100, AccountType.Basic, -100, false)]
        [TestCase("44444", "Premium Account", 100, AccountType.Premium, 250, true)]
        [TestCase("44444", "Premium Account", 600, AccountType.Premium, 250, true)]
        [TestCase("44466", "Premium Account", 100, AccountType.Premium, -250, false)]
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Type = accountType;
            account.Balance = balance;

            AccountDepositResponse response = deposit.Deposit(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("44444", "Premium Account", 1500, AccountType.Premium, -2001, false)]
        [TestCase("44444", "Premium Account", 100, AccountType.Free, -100, false)]
        [TestCase("44444", "Premium Account", 100, AccountType.Premium, 100, false)]
        [TestCase("44444", "Premium Account", 150, AccountType.Premium, -500, true)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawalRule();
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Type = accountType;
            account.Balance = balance;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}