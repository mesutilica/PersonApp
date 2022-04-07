using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonApp.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace PersonApp.WebUI.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AppUsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:21121/api/AppUsers");
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<AppUser>>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:21121/api/AppUsers/" + id);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AppUser>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(appUser);
                StringContent stringContent = new(jsonData, encoding:Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PostAsync("http://localhost:21121/api/AppUsers/", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Eklenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:21121/api/AppUsers/{id}");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AppUser>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: AppUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(appUser);
                StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PutAsync($"http://localhost:21121/api/AppUsers/{id}", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Güncellenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:21121/api/AppUsers/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AppUser>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"http://localhost:21121/api/AppUsers/{id}");
            return RedirectToAction(nameof(Index));
        }

    }
}
