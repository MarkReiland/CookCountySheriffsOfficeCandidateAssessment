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
        public RecordsController(StudentService studentService, SchoolService schoolService)
        {
            _studentService = studentService;
            _schoolService = schoolService;
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

        [HttpPost]
        public IActionResult SaveStudent(Student model)
        {
            // replace this code with code that actually saves the model

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
            return new List<SelectListItem> { new SelectListItem { Text = "Replace this code", Value = "" } };
        }

        private MultiSelectList CreateStudentOrgDropdown()
        {
            // replace this code with code to grab the student orgs and create a List<SelectListItem> object from them.
            var options = new List<SelectListItem>
            {
                new SelectListItem { Text = "Option 1", Value = "1" },
                new SelectListItem { Text = "Option 2", Value = "2" },
                new SelectListItem { Text = "Option 3", Value = "3" },
                new SelectListItem { Text = "Option 4", Value = "4" }
            };

            return new MultiSelectList(options, "Value", "Text");
        }
    }
}