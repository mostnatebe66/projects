using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrPortal.Data.Repositories;
using HrPortal.Models.Interfaces;
using HrPortal.Models.Data;
using HrPortal.Models.Responses;

namespace HrPortal.BLL
{
    public class HrManager
    {
        private IPolicyRepository _policyRepository;
        private ICategoryRepository _categoryRepository;
        private IResumeRepository _resumeRepository;

        public HrManager(IPolicyRepository policyRepository, ICategoryRepository categoryRepository/*, IResumeRepository resumeRepository*/)
        {
            _policyRepository = policyRepository;
            _categoryRepository = categoryRepository;
            //_resumeRepository = resumeRepository; //removed for unit testing of in memory repo
        }

        public IEnumerable<Policy> LookUpPolicies()
        {
            var policyLookUp = _policyRepository.GetAll();
            return policyLookUp;
        }

        public Policy GetPolicy(int id)
        {
            return LookUpPolicies().FirstOrDefault(i => i.ID == id);
        }

        public void AddPolicies(Policy policy)
        {
            _policyRepository.Add(policy);
            //PolicyResponse response = new PolicyResponse();
            //response.Success = policyAdd.Add(policyName, content, CategoryID);
            //return response.Success;
        }
        public void DeletePolicies(int ID)
        {
            _policyRepository.Delete(ID);
        }

        public IEnumerable<Category> LookUpCategories()
        {
            var categoryLookUp = _categoryRepository.GetAll();
            return categoryLookUp;
        }

        public Category GetCategory(string categoryName)
        {
            return LookUpCategories().FirstOrDefault(c => c.Name == categoryName);
        }

        public void AddCategories(string categoryName)
        {
            CategoryRepository categoryAdd = new CategoryRepository(); 
            //FileCategoryRepository categoryAdd = new FileCategoryRepository();
            categoryAdd.Add(categoryName);
        }

        public void DeleteCategories(string categoryName)
        {
            CategoryRepository categoryDelete = new CategoryRepository(); 
            //FileCategoryRepository categoryDelete = new FileCategoryRepository();
            categoryDelete.Delete(categoryName);
        }

        public void AddApplication(Applicant applicant)
        {
            _resumeRepository.Add(applicant);
        }
    }
}


