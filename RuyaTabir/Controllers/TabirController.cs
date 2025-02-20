using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using RuyaTabir.Models.db;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using RuyaTabir.Models.api;

namespace RuyaTabir.Controllers
{
    public class TabirController : Controller
    {
        // GET: Tabir
        RuyaTabirMvcEntities3 db = new RuyaTabirMvcEntities3();
        public ActionResult Index(string response)
        {
            ViewBag.result = response;
            return View();
        }
        public ActionResult Tabir(int id)
        {

            var ruya = db.RUYA.Find(id);
            return View("Tabir", ruya);
        }
        [HttpGet]
        public ActionResult YeniRuya()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> YeniRuya(ChatDto p1)
        {
            string girdi = p1.girdi;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://chatgpt-api7.p.rapidapi.com/ask"),
                Headers =
    {
        //your api key
    },
                Content = new StringContent("{\r\"query\": \"" +"rüyamda"+ girdi +"rüyamı tabir et."+ "\"\r}")
                {
                    Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
                }
            };
            ChatDto chatResult = null;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                ChatDto ligSiralama = JsonConvert.DeserializeObject<ChatDto>(body);
                chatResult = ligSiralama;
            }
            
            return Redirect("/Tabir/Index?response="+chatResult.response);

        }

    }
}