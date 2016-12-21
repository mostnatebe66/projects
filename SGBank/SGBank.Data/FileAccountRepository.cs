using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        public Account LoadAccount(string accountNumber)
        {
            Account account = null;

            string[] lines = File.ReadAllLines("accounts.txt");
            foreach (var line in lines)
            {
                string[] splitLines = line.Split(',');

                if (splitLines[0] == accountNumber)
                {
                    decimal number = decimal.Parse(splitLines[2]);

                    account = new Account();
                    account.AccountNumber = splitLines[0];
                    account.Name = splitLines[1];
                    account.Balance = number;
                    if (splitLines[3] == "F")
                    {
                        account.Type = AccountType.Free;
                    }
                    if (splitLines[3] == "B")
                    {
                        account.Type = AccountType.Basic;
                    }
                    if (splitLines[3] == "P")
                    {
                        account.Type = AccountType.Premium;
                    }
                }
            }
            return account;
        }

        public void SaveAccount(Account account)
        {
            string[] lines = File.ReadAllLines("accounts.txt");

            for (int index = 0; index < lines.Length; index++)
            {
                string[] splitLines = lines[index].Split(',');

                if (splitLines[0] == account.AccountNumber)
                {
                    splitLines[0] = account.AccountNumber;
                    splitLines[1] = account.Name;
                    splitLines[2] = account.Balance.ToString();
                    splitLines[3] = account.Type.ToString().Substring(0, 1);

                    lines[index] = string.Join(",", splitLines);
                }
            }
            File.WriteAllLines("accounts.txt", lines);
        }
    }
}