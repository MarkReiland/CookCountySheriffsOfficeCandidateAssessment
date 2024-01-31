using CandidateAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateAssessment.Services
{
    public class StudentOrganizationService
    {
        private CandidateAssessmentContext _dbContext;

        public StudentOrganizationService(CandidateAssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<StudentOrganization> GetStudentOrganizations()
        {
            return _dbContext.StudentOrganizations;
        }
    }
}



