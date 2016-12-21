using HrPortal.Data;
using HrPortal.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace HrPortal.BLL
{
    public class PolicyManagerFactory
    {
        public static HrManager Create()
        {
            string mode = ConfigurationManager.AppSettings["mode"].ToString();

            switch (mode)
            {
                case "MockDataTest":
                    return new HrManager(new PolicyRepository(), new CategoryRepository());
                //case "FileDataTest":
                //    return new HrManager(new FilePolicyRepository(), new FileCategoryRepository(), new FileResumeRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
