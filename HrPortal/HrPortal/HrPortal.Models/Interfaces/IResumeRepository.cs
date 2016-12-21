using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Models.Interfaces
{
    public interface IResumeRepository
    {
        void Add(Applicant applicant);
        IEnumerable<Applicant> GetAll();
        Applicant Get(int ID);
    }
}
