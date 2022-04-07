using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonApp.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace PersonApp.WebUI.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PeopleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:21121/api/People");
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Person>>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:21121/api/People/" + id);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Person>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(person);
                StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PostAsync("http://localhost:21121/api/People/", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Eklenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:21121/api/People/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Person>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(person);
                StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PutAsync($"http://localhost:21121/api/People/{id}", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Güncellenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:21121/api/People/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Person>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"http://localhost:21121/api/People/{id}");
            return RedirectToAction(nameof(Index));
        }

    }
}
