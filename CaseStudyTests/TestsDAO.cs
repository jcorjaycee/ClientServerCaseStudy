using Xunit;
using HelpdeskDAL;
using System.Collections.Generic;

namespace CaseStudyTests
{
    public class TestsDAO
    {
        [Fact]
        public void Employee_GetByEmailTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees selectedEmployee = dao.GetByEmail("bs@abc.com");
            Assert.NotNull(selectedEmployee);
        }


        [Fact]
        public void Employee_GetByIdTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees selectedEmployee = dao.GetById(4);
            Assert.NotNull(selectedEmployee);
        }

        [Fact]
        public void Employee_GetAllTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            List<Employees> selectedEmployeesList = dao.GetAll();
            Assert.True(selectedEmployeesList.Count > 0);
        }

        [Fact]
        public void Employee_AddTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees newEmployee = new Employees
            {
                FirstName = "test",
                LastName = "Leckie",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DepartmentId = 100,
                Email = "sl@abc.com"
            };
            int newEmployeeId = dao.Add(newEmployee);
            Assert.True(newEmployeeId > 1);
        }

        [Fact]
        public void Employee_UpdateTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees EmployeeForUpdate = dao.GetByEmail("sl@abc.com");

            if (EmployeeForUpdate != null)
            {
                string oldPhoneNo = EmployeeForUpdate.PhoneNo;
                string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-5555" : "519-555-1234";
                EmployeeForUpdate.PhoneNo = newPhoneNo;
            }
            Assert.True(dao.Update(EmployeeForUpdate) == UpdateStatus.Ok);
        }

        [Fact]
        public void Employee_DeleteTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            int EmployeeDeleted = dao.Delete(dao.GetByEmail("Leckie").Id);
            Assert.True(EmployeeDeleted != -1);
        }

        [Fact]
        public void Employee_ConcurrencyTest()
        {
            EmployeeDAO dao1 = new EmployeeDAO();
            EmployeeDAO dao2 = new EmployeeDAO();
            Employees EmployeeForUpdate1 = dao1.GetByEmail("sl@abc.com");
            Employees EmployeeForUpdate2 = dao2.GetByEmail("sl@abc.com");

            if (EmployeeForUpdate1 != null)
            {
                string oldPhoneNo = EmployeeForUpdate1.PhoneNo;
                string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-5555" : "519-555-1234";
                EmployeeForUpdate1.PhoneNo = newPhoneNo;
                if (dao1.Update(EmployeeForUpdate1) == UpdateStatus.Ok)
                {
                    EmployeeForUpdate2.PhoneNo = "666-666-6666";
                    Assert.True(dao2.Update(EmployeeForUpdate2) == UpdateStatus.Stale);
                }
                else
                    Assert.True(false);
            }
        }

        [Fact]
        public void Employee_LoadPicsTest()
        {
            DALUtil util = new DALUtil();
            Assert.True(util.AddEmployeePicsToDb());
        }

    }
}
