using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sagu.DTO;
using Sagu.MVC.Models;

namespace Sagu.MVC.Controllers
{
    public class ExplorersController : Controller
    {
        HttpClient saguClient = SaguClient.GetClient();

        // GET: Explorers
        public async Task<ActionResult> Index()
        {
            var response = await saguClient.GetAsync("api/explorers");

            if (response.IsSuccessStatusCode)
            {
                var viewModel = new ExplorersViewModel();
                var json = await response.Content.ReadAsStringAsync();
                viewModel.Explorers = JsonConvert.DeserializeObject<IEnumerable<Explorer>>(json);

                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // GET: Explorers/View/5
        public async Task<ActionResult> View(Guid id)
        {
            var response = await saguClient.GetAsync("api/explorers/" + id);

            if (response.IsSuccessStatusCode)
            {                
                var json = await response.Content.ReadAsStringAsync();
                var viewModel = JsonConvert.DeserializeObject<ExplorerViewModel>(json);
   
                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // GET: Explorers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Explorers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Explorer explorer)
        {
            try
            {                
                explorer.Id = Guid.NewGuid();
                var json = JsonConvert.SerializeObject(explorer);

                var response = await saguClient.PostAsync("api/explorers",
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

        // GET: Explorers/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await saguClient.GetAsync("api/explorers/" + id);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var viewModel = JsonConvert.DeserializeObject<Explorer>(json);

                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // POST: Explorers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Explorer explorer)
        {
            try
            {
                var json = JsonConvert.SerializeObject(explorer);

                var response = await saguClient.PutAsync("api/explorers",
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

        // POST: Explorers/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var response = await saguClient.DeleteAsync("api/explorers/" + id);

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
