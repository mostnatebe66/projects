using SGBank.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SG Bank Application");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Lookup an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");

                Console.WriteLine("\nQ to quit");
                Console.Write("\nEnter selection: ");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        AccountLookupWorkflow lookUpExecute = new AccountLookupWorkflow();
                        lookUpExecute.Execute();
                        break;
                    case "2":
                        DepositWorkflow depositExecute = new DepositWorkflow();
                        depositExecute.Execute();
                        break;
                    case "3":
                        WithdrawWorkflow withdrawExecute = new WithdrawWorkflow();
                        withdrawExecute.Execute();
                        break;
                    case "Q":
                        return;
                }
            }
        }
    }
}
