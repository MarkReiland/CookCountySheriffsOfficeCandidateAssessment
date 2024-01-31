using CandidateAssessment.Models;
using CandidateAssessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CandidateAssessment.Controllers
{
    public class RecordsController : Controller
    {
        private StudentService _studentService;
        private SchoolService _schoolService;
        private StudentOrganizationService _studentOrganizationService;
        private OrgAssignmentService _orgAssignmentService;
        public RecordsController(StudentService studentService, SchoolService schoolService, StudentOrganizationService studentOrganizationService, OrgAssignmentService orgAssignmentService)
        {
            _studentService = studentService;
            _schoolService = schoolService;
            _studentOrganizationService = studentOrganizationService;
            _orgAssignmentService = orgAssignmentService;
        }

        public IActionResult Students()
        {
            ViewBag.AgeList = CreateAgeList();
            ViewBag.SchoolList = CreateSchoolDropdownList();
            ViewBag.OrgList = CreateStudentOrgDropdown();

            var model = _studentService.GetStudents().OrderBy(s => s.LastName);
            return View(model);
        }

        public IActionResult Schools()
        {
            var model = _schoolService.GetSchools().OrderBy(s => s.Name);
            return View(model);
        }

        public IActionResult SchoolRoster(int schoolId, string schoolName)
        {
            ViewBag.SchoolName = schoolName;

            var model = _studentService.GetStudents().Where(s => s.SchoolId == schoolId).OrderBy(s => s.LastName);
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveStudent(Student model)
        {
            // replace this code with code that actually saves the model

            _studentService.SaveStudent(model);

            // convert the SelectedOrgs property of the model to OrgAssignments
            var OrgAssignments = new List<OrgAssignment>();

                if (model.SelectedOrgs != null)
                {
                    foreach (int orgId in model.SelectedOrgs)
                    {
                        _orgAssignmentService.SaveOrgAssignment(new OrgAssignment { StudentId = model.StudentId, StudentOrgId = orgId });
                    }
                }

            return RedirectToAction("Students");
        }

        private List<SelectListItem> CreateAgeList()
        {
            var ageList = new List<SelectListItem>();
            for (int i = 18; i < 100; i++)
            {
                ageList.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            return ageList;
        }

        private List<SelectListItem> CreateSchoolDropdownList()
        {
            // replace this code with code to grab the schools and create a List<SelectListItem> object from them.

            var schools = _schoolService.GetSchools().OrderBy(s => s.Name);
            var options = new List<SelectListItem>();

            foreach (School school in schools) {
                options.Add(new SelectListItem { Text = school.Name, Value = school.SchoolId.ToString() });
            }

            return options;
        }

        private MultiSelectList CreateStudentOrgDropdown()
        {
            var orgs = _studentOrganizationService.GetStudentOrganizations().OrderBy(s => s.OrgName);
            var options = new List<SelectListItem>();

            foreach (StudentOrganization studentOrganization in orgs)
            {
                options.Add(new SelectListItem { Text = studentOrganization.OrgName, Value = studentOrganization.Id.ToString() });
            }

            return new MultiSelectList(options, "Value", "Text");
        }
    }
}