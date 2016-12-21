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
    public class FilePolicyRepository : IPolicyRepository
    {
        private const string File_Path = @"C:\_repos\bitbucket\nate-betz-individual-work\HrPortal\HrPortal\HrPortal\DataFiles\Policies\";

        public void Add(Policy policy)
        {
            policy.ID = GetAll().Max(p => p.ID) + 1;

            using (StreamWriter sw = new StreamWriter($"{File_Path}{policy.ID}.txt", false))
            {
                sw.WriteLine($"ID  {policy.ID}");
                sw.WriteLine($"Name  {policy.Name}");
                sw.WriteLine($"CategoryID  {policy.CategoryID}");
                sw.WriteLine($"Description  {policy.Description}");
            }
        }

        public IEnumerable<Policy> GetAll()
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

        public Policy Get(int ID)
        {
            string[] getPolicy = File.ReadAllLines($"{File_Path}{ID}.txt");
            Policy policy = new Policy();
            policy.ID = int.Parse(getPolicy[0].Substring(3));
            policy.Name = (getPolicy[1].Substring(4));
            policy.CategoryID = (getPolicy[2].Substring(12)).Trim();
            policy.Description = getPolicy[3];
            StringBuilder sb = new StringBuilder();
            for (int i = 4; i < getPolicy.Length; i++)
            {
                sb.AppendLine(getPolicy[i]);
            }
            //policy.Description = sb.ToString();

            return policy;
        }

        public void Delete(int ID)
        {            
            File.Delete($"{File_Path}{ID}.txt");
        }
    }
}
