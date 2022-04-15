using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using PersonApp.Entities;
using System.Text;

namespace PersonApp.WebUI.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.PersonId = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:21121/api/Contacts/{id}");
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Contact>>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:21121/api/Contacts/" + id);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Contact>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: Contacts/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.PersonId = id;
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            contact.Id = 0;
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(contact);
                StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PostAsync("http://localhost:21121/api/Contacts/", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { id = contact.PersonId });
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Eklenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:21121/api/Contacts/GetContact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Contact>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(contact);
                StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PutAsync($"http://localhost:21121/api/Contacts/{id}", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { id = contact.PersonId });
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Güncellenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:21121/api/Contacts/GetContact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Contact>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int PersonId)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"http://localhost:21121/api/Contacts/{id}");
            return RedirectToAction(nameof(Index), new { id = PersonId });
        }

    }
}
