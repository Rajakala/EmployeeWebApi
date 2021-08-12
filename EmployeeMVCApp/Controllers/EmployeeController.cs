using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EmployeeWebApi.Models;

namespace EmployeeMVCApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public async Task< ActionResult> Index()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            employees = await List();
            return View(employees);
        }

        public ActionResult Add()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var employees = await List();
            var editableEmployee = employees.FirstOrDefault(e => e.ID == id);
            return View(editableEmployee);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var employees = await List();
            var deletableEmployee = employees.FirstOrDefault(e => e.ID == id);
            return View(deletableEmployee);
        }

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.DeleteAsync("api/Employee/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return null;
        }

        public async Task<ActionResult> UpdateEmployee(EmployeeModel employee)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PutAsJsonAsync("api/Employee", employee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return null;
        }

        public async Task<ActionResult> AddEmployee(EmployeeModel employee)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("api/Employee",employee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return null;
        }

        public async Task<List<EmployeeModel>> List()
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("api/Employee");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<EmployeeModel>>();
            }
            return null;
        }
    }
}