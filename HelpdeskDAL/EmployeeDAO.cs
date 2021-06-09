using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskDAL
{
    public class EmployeeDAO
    {
        readonly IRepository<Employees> repository;

        public EmployeeDAO()
        {
            repository = new HelpdeskRepository<Employees>();
        }

        public Employees GetByEmail(string email)
        {
            List<Employees> selectedEmployees = null;

            try
            {
                selectedEmployees = repository.GetByExpression(emp => emp.Email == email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedEmployees.FirstOrDefault();
        }
        public Employees GetById(int id)
        {
            Employees selectedEmployee = null;

            try
            {
                selectedEmployee = repository.GetByExpression(emp => emp.Id == id).FirstOrDefault();
            } catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee;
        }

        public List<Employees> GetAll()
        {
            List<Employees> allEmployees = new List<Employees>();

            try
            {
                allEmployees = repository.GetAll();
            } catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allEmployees;
        }

        public int Add(Employees newEmployee)
        {
            try
            {
                repository.Add(newEmployee);
            } catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newEmployee.Id;
        }

        public UpdateStatus Update(Employees updatedEmployee)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                operationStatus = repository.Update(updatedEmployee);
            } catch (DbUpdateConcurrencyException)
            {
                operationStatus = UpdateStatus.Stale;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }
            return operationStatus;
        }

        public int Delete(int id)
        {
            int employeesDeleted = -1;

            try
            {
                employeesDeleted = repository.Delete(id);
            } catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return employeesDeleted;
        }
    }
}
