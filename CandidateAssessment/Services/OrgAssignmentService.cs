using CandidateAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateAssessment.Services
{
    public class OrgAssignmentService
    {
        private CandidateAssessmentContext _dbContext;

        public OrgAssignmentService(CandidateAssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrgAssignment> GetOrgAssignments()
        {
            return _dbContext.OrgAssignments;
        }

        public void SaveOrgAssignment(OrgAssignment model)
        {
            _dbContext.OrgAssignments.Add(model);
            _dbContext.SaveChanges();
        }
    }
}
