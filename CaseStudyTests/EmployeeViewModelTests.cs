using Xunit;
using HelpdeskViewModels;
using System.Collections.Generic;

namespace CaseStudyTests
{
    public class EmployeeViewModelTests
    {
        [Fact]
        public void Employee_GetByMailTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "sc@abc.com" };
            vm.GetByEmail();
            Assert.NotNull(vm.Firstname);
        }

        [Fact]
        public void Employee_GetByIdTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Id = 2 };
            vm.GetById();
            Assert.NotNull(vm.Firstname);
        }

        [Fact]
        public void Employee_GetAllTest()
        {
            // I'm sure there's a better way to do this but couldn't figure it out,
            // I'm fully open to feedback on this
            EmployeeViewModel test = new EmployeeViewModel();
            List<EmployeeViewModel> vm = test.GetAll();
            Assert.True(vm.Count > 1);
        }


        [Fact]
        public void Employee_AddTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel
            {
                Title = "Mr.",
                Firstname = "Sean",
                Lastname = "Leckie",
                Phoneno = "(555)555-1234",
                Email = "mm@abc.ca",
                DepartmentId = 100
            };
            vm.Add();
            Assert.True(vm.Id > -1);
        }

        [Fact]
        public void Employee_UpdateTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "mm@abc.ca" };
            vm.GetByEmail();
            vm.Phoneno = vm.Phoneno == "(555)555-1234" ? "(555)555-1235" : "(555)555-1234";
            Assert.True(vm.Update() == HelpdeskDAL.UpdateStatus.Ok);
        }

        [Fact]
        public void Employee_DeleteTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "mm@abc.ca" };
            vm.GetByEmail();
            Assert.True(vm.Delete() == 1);
        }
    }
}
