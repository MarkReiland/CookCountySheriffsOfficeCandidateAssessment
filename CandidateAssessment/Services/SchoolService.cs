using CandidateAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateAssessment.Services
{
    public class SchoolService
    {
        private CandidateAssessmentContext _dbContext;

        public SchoolService(CandidateAssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<School> GetSchools()
        {
            return _dbContext.Schools
                .Include(s => s.Students);
        }
    }
}
