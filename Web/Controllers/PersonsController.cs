using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class PersonsController : Controller
    {
        // GET: PersonsController
        public async Task<IActionResult> Index(int page = 1, string filter = null)
        {
            PersonResultViewModel personResultViewModel = new PersonResultViewModel();

            using (var httpClient = new HttpClient())
            {
                try
                {

                    if (!string.IsNullOrEmpty(filter))
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:44317/Api/Persons/Search/" + filter + "?page=" + page))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            personResultViewModel = JsonConvert.DeserializeObject<PersonResultViewModel>(apiResponse);
                        }
                    }
                    else
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:44317/Api/Persons/GetAll/?page=" + page))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            personResultViewModel = JsonConvert.DeserializeObject<PersonResultViewModel>(apiResponse);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
            
            return View(personResultViewModel);

        }

        // GET: PersonsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PersonViewModel PersonViewModel = new PersonViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Persons/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        PersonViewModel = JsonConvert.DeserializeObject<PersonViewModel>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(PersonViewModel);
        }

        // GET: PersonsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel model)
        {
            PersonViewModel personViewModel = new PersonViewModel();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                
                try
                {

                    using (var response = await httpClient.PostAsync("https://localhost:44317/Api/Persons/Add/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        personViewModel = JsonConvert.DeserializeObject<PersonViewModel>(apiResponse);
                    }
                }
                catch(Exception e) { }
            }
            return View(personViewModel);
        }

        // GET: PersonsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            PersonViewModel personViewModel = new PersonViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Persons/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    personViewModel = JsonConvert.DeserializeObject<PersonViewModel>(apiResponse);
                }
            }
            return View(personViewModel);
        }

        // POST: PersonsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonViewModel model)
        {
            PersonViewModel personViewModel = new PersonViewModel();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(model.ID.ToString()), "ID");
                content.Add(new StringContent(model.FirstName), "FirstName");
                content.Add(new StringContent(model.LastName), "LastName");
                content.Add(new StringContent(model.PersonalNumber), "PersonalNumber");
                content.Add(new StringContent(model.BirthDay.ToString()), "BirthDay");
                content.Add(new StringContent(model.GenderID.ToString()), "GenderID");
                content.Add(new StringContent(model.AddressID.ToString()), "AddressID");
                content.Add(new StringContent(model.AvatarID.ToString()), "AvatarID");

                using (var response = await httpClient.PutAsync("https://localhost:44317/Api/Persons/edit/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    personViewModel = JsonConvert.DeserializeObject<PersonViewModel>(apiResponse);
                }
            }
            return View(personViewModel);
        }

        // GET: PersonsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
