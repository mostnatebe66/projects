using BaseballLeague.Data.Interfaces;
using BaseballLeague.Data.TestRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.BLL
{
    public class Factory
    {
        public static IBaseballRepository CreateBaseballRepository()
        {
            string mode = (ConfigurationManager.AppSettings["Mode"].ToString());
            switch (mode)
            {
                case "test":
                    return new MockBaseballRepository();
                case "Dapper":
                    return new DapperRepository();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}

