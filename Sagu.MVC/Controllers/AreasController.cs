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
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Sagu.MVC.Controllers
{
    public class AreasController : Controller
    {
        HttpClient saguClient = SaguClient.GetClient();

        // GET: Areas
        public async Task<ActionResult> Index()
        {
            var response = await saguClient.GetAsync("api/areas");

            if (response.IsSuccessStatusCode)
            {
                var viewModel = new AreasViewModel();
                var json = await response.Content.ReadAsStringAsync();
                viewModel.Areas = JsonConvert.DeserializeObject<IEnumerable<Area>>(json);

                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // GET: Areas/View/5
        public async Task<ActionResult> View(Guid id)
        {
            var response = await saguClient.GetAsync("api/areas/" + id);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var viewModel = JsonConvert.DeserializeObject<AreaViewModel>(json);

                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Area area)
        {
            try
            {
                area.Id = Guid.NewGuid();
                var json = JsonConvert.SerializeObject(area);

                var response = await saguClient.PostAsync("api/areas",
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

        // GET: Areas/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await saguClient.GetAsync("api/areas/" + id);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var viewModel = JsonConvert.DeserializeObject<Area>(json);

                return View(viewModel);
            }
            else
                return Content("An error occured.");
        }

        // POST: Areas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Area area)
        {
            try
            {
                var json = JsonConvert.SerializeObject(area);

                var response = await saguClient.PutAsync("api/areas",
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

        // POST: Areas/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var response = await saguClient.DeleteAsync("api/areas/" + id);

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
