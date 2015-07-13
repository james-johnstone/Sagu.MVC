using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sagu.DTO;
using Sagu.MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Sagu.MVC.Controllers
{
    public class AnimalsController : Controller
    {
        HttpClient saguClient = SaguClient.GetClient();

        // GET: Animals
        public async Task<ActionResult> Index()
        {
            var response = await saguClient.GetAsync("api/animals");

            if (response.IsSuccessStatusCode)
            {
                var viewModel = new AnimalsViewModel();
                var json = await response.Content.ReadAsStringAsync();
                viewModel.Animals = JsonConvert.DeserializeObject<IEnumerable<Animal>>(json);

                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // GET: Animals/View/5
        public async Task<ActionResult> View(Guid id)
        {
            var response = await saguClient.GetAsync("api/animals/" + id);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var viewModel = JsonConvert.DeserializeObject<AnimalViewModel>(json);

                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // GET: Animals/Create
        public async Task<ActionResult> Create()
        {
            var response = await saguClient.GetAsync("api/areas/");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var areas = JsonConvert.DeserializeObject<IEnumerable<Area>>(json);

                var viewModel = new AnimalViewModel();
                viewModel.Areas = GetAreas(areas);
                return View("Edit", viewModel);
            }
            else
                return Content("An error occured.");
        }

        private IEnumerable<SelectListItem> GetAreas(IEnumerable<Area> areas)
        {
            return areas.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            });
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Animal animal)
        {
            try
            {
                animal.Id = Guid.NewGuid();
                var json = JsonConvert.SerializeObject(animal);

                var response = await saguClient.PostAsync("api/animals",
                                        new StringContent(json, Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return Content("An error occurred, status code " + response.StatusCode);
            }
            catch
            {
                return Content("An error occurred.");
            }
        }

        // GET: Animals/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await saguClient.GetAsync("api/animals/" + id);

            if (response.IsSuccessStatusCode)
            {
                var areasResponse = await saguClient.GetAsync("api/areas/");
                var areasJson = await areasResponse.Content.ReadAsStringAsync();
                var areas = JsonConvert.DeserializeObject<IEnumerable<Area>>(areasJson);

                var json = await response.Content.ReadAsStringAsync();
                var viewModel = JsonConvert.DeserializeObject<AnimalViewModel>(json);

                viewModel.Areas = GetAreas(areas);

                return View("Edit", viewModel);
            }
            else
                return Content("An error occured.");
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Animal animal)
        {
            try
            {
                var json = JsonConvert.SerializeObject(animal);

                var response = await saguClient.PutAsync("api/animals",
                                        new StringContent(json, Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return Content("An error occurred, status code " + response.StatusCode);
            }
            catch
            {
                return Content("An error occurred.");
            }
        }

        // POST: Animals/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var response = await saguClient.DeleteAsync("api/animals/" + id);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return Content("An error occurred, status code " + response.StatusCode);
            }
            catch
            {
                return Content("An error occurred.");
            }
        }
    }
}
