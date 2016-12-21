using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    class WithdrawRulesFactory
    {
        public static IWithdraw Create(AccountType type)
        {
            FreeAccountWithdrawRule freeAccountWithdraw = new FreeAccountWithdrawRule();
            BasicAccountWithdrawalRule basicAccountWithdraw = new BasicAccountWithdrawalRule();
            PremiumAccountWithdrawalRule premiumAccountWithdraw = new PremiumAccountWithdrawalRule();

            switch (type)
            {
                case AccountType.Free:
                    return freeAccountWithdraw;
                case AccountType.Basic:
                    return basicAccountWithdraw;
                case AccountType.Premium:
                    return premiumAccountWithdraw;
                default:
                    break;
            }
            throw new Exception("Account type is not supported!");
        }
    }
}
