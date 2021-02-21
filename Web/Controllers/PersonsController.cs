using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            
            return View(personResultViewModel);

        }

        // GET: PersonsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PersonViewModel model = new PersonViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Persons/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<PersonViewModel>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(model);
        }

        // GET: PersonsController/Create
        public async Task<IActionResult> Create()
        {
            AddEditPersonViewModel model = new AddEditPersonViewModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Gender/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Gender = JsonConvert.DeserializeObject<IEnumerable<Gender>>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }

                using (var response = await httpClient.GetAsync("https://localhost:44317/api/PhoneType/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.PhoneType = JsonConvert.DeserializeObject<IEnumerable<PhoneType>>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }

                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Address/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Address = JsonConvert.DeserializeObject<IEnumerable<City>>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }

            return View(model);
        }

        // POST: PersonsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddEditPersonViewModel model)
        {
            if(!string.IsNullOrEmpty(model.PhoneNumber))
            {
                model.Phones = new List<Phone>
                {
                    new Phone { Number = model.PhoneNumber, PhoneTypeID = model.PhoneTypeID }
                };
            }


            if (model.File != null)
            {
                using (var httpClient = new HttpClient())
                {

                    var fileName = ContentDispositionHeaderValue.Parse(model.File.ContentDisposition).FileName.Trim('"');

                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StreamContent(model.File.OpenReadStream())
                        {
                            Headers =
                    {
                        ContentLength = model.File.Length,
                        ContentType = new MediaTypeHeaderValue(model.File.ContentType)
                    }
                        }, "File", fileName);


                        using (var response = await httpClient.PostAsync("https://localhost:44317/api/Persons/FileUpload", content))
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                var fileresult = JsonConvert.DeserializeObject<Attachment>(apiResponse);
                                model.AttachmentID = fileresult.ID;
                            }
                        }
                    }
                }
            }


            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Gender/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Gender = JsonConvert.DeserializeObject<IEnumerable<Gender>>(apiResponse);
                    }
                }

                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Address/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Address = JsonConvert.DeserializeObject<IEnumerable<City>>(apiResponse);
                    }
                }

                using (var response = await httpClient.GetAsync("https://localhost:44317/api/PhoneType/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.PhoneType = JsonConvert.DeserializeObject<IEnumerable<PhoneType>>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }

                using (var response = await httpClient.PostAsync("https://localhost:44317/Api/Persons/Add/", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index");
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var error = response.Content.ReadAsStringAsync();

                        var test = JsonConvert.DeserializeObject<object>(error.Result);
                        return View(model);
                    }
                }
                
            }
            return View(model);
        }

        // GET: PersonsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            AddEditPersonViewModel model = new AddEditPersonViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Persons/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<AddEditPersonViewModel>(apiResponse);
                }
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Gender/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Gender = JsonConvert.DeserializeObject<IEnumerable<Gender>>(apiResponse);
                    }
                }

                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Address/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Address = JsonConvert.DeserializeObject<IEnumerable<City>>(apiResponse);
                    }
                }
            }
            return View(model);
        }

        // POST: PersonsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddEditPersonViewModel model)
        {
            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                model.Phones = new List<Phone>
                {
                    new Phone { Number = model.PhoneNumber, PhoneTypeID = model.PhoneTypeID }
                };
            }


            if (model.File != null)
            {
                using (var httpClient = new HttpClient())
                {

                    var fileName = ContentDispositionHeaderValue.Parse(model.File.ContentDisposition).FileName.Trim('"');

                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StreamContent(model.File.OpenReadStream())
                        {
                            Headers =
                    {
                        ContentLength = model.File.Length,
                        ContentType = new MediaTypeHeaderValue(model.File.ContentType)
                    }
                        }, "File", fileName);


                        using (var response = await httpClient.PostAsync("https://localhost:44317/api/Persons/FileUpload", content))
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                var fileresult = JsonConvert.DeserializeObject<Attachment>(apiResponse);
                                model.AttachmentID = fileresult.ID;
                            }
                        }
                    }
                }
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Gender/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Gender = JsonConvert.DeserializeObject<IEnumerable<Gender>>(apiResponse);
                    }
                }

                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Address/GetAll"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Address = JsonConvert.DeserializeObject<IEnumerable<City>>(apiResponse);
                    }
                }

                using (var response = await httpClient.PostAsync("https://localhost:44317/Api/Persons/Edit/", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return View(model);
                    }
                }

            }
            return View(model);
        }

        // GET: PersonsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public async Task<IActionResult> Contact(int id, int page)
        {
            PersonContactResultViewModel model = new PersonContactResultViewModel();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:44317/Api/Persons/GetConcats/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<PersonContactResultViewModel> (apiResponse);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(AddContact model)
        {
            PersonContactResultViewModel viewModel = new PersonContactResultViewModel();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:44317/Api/Persons/GetConcats/" + model.PersonID))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        viewModel = JsonConvert.DeserializeObject<PersonContactResultViewModel>(apiResponse);
                    }
                }
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44317/Api/Persons/AddContact/", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return View(viewModel);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return View(viewModel);
                    }
                }
            }
            return View(viewModel);
        }
    }
}
