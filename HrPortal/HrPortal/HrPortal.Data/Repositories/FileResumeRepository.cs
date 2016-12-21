using HrPortal.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrPortal.Models.Data;
using System.IO;

namespace HrPortal.Data.Repositories
{
    public class FileResumeRepository : IResumeRepository
    {
        private const string File_Path = @"C:\_repos\bitbucket\nate-betz-individual-work\HrPortal\HrPortal\HrPortal\DataFiles\Applications\";

        public void Add(Applicant applicant)
        {
            applicant.ID = GetAll().Max(a => a.ID) + 1;

            using (StreamWriter sw = new StreamWriter($"{File_Path}{applicant.ID}.txt", false))
            {
                sw.WriteLine($"{applicant.ID}");
                sw.WriteLine($"{applicant.FirstName}");
                sw.WriteLine($"{applicant.LastName}");
                sw.WriteLine($"{applicant.StreetAddress}");
                sw.WriteLine($"{applicant.City}");
                sw.WriteLine($"{applicant.State}");
                sw.WriteLine($"{applicant.ZipCode}");
                sw.WriteLine($"{applicant.PhoneNumber}");
                sw.WriteLine($"{applicant.EmailAddress}");
                sw.WriteLine($"{applicant.PositionApplied}");
                sw.WriteLine($"{applicant.CurrentEmployer}");
            }
        }

        public IEnumerable<Applicant> GetAll()
        {
            foreach (var filePath in Directory.EnumerateFiles(File_Path))
            {
                var idPath = Path.GetFileNameWithoutExtension(filePath);
                int fileID = 0;
                if (int.TryParse(idPath, out fileID))
                {
                    yield return Get(fileID);
                }
            }
        }

        public Applicant Get(int ID) //copy get method from filepolicyrepo
        {
            string[] getApplication = File.ReadAllLines($"{File_Path}{ID}.txt");
            Applicant applicant = new Applicant();
            applicant.ID = ID;
            applicant.FirstName = (getApplication[1]);
            applicant.LastName = (getApplication[2]);
            applicant.StreetAddress = (getApplication[3]);
            applicant.City = (getApplication[4]);
            applicant.State = (getApplication[5]);
            applicant.ZipCode = (getApplication[6]);
            applicant.PhoneNumber = (getApplication[7]);
            applicant.EmailAddress = (getApplication[8]);
            applicant.PositionApplied = (getApplication[9]);
            applicant.CurrentEmployer = (getApplication[10]);
            StringBuilder sb = new StringBuilder();
            for (int i = 11; i < getApplication.Length; i++)
            {
                sb.AppendLine(getApplication[i]);
            }
            return applicant;
        }
    }
}
