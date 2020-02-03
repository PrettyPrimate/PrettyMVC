using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PrettyMVC.Models;

namespace PrettyMVC.Controllers
{
    public class HomeController : Controller
    {
        private const string url = "http://prettyapi/api/clubs/GetAllClubs";
        private string urlParameters = "?api_key=123";

        public async Task<ActionResult> Index()
        {
            var clubs = await GetClubs();


            var model = new ClubsViewModel(){ Clubs =  clubs };

            return View(model);
        }

        public async Task<List<Club>> GetClubs()
        {
            var clubs = new List<Club>();

            using (var client = GetHttpClient())
            {
                var fullReqUrl = "http://prettyapi/api/clubs/GetAllClubs";

                try
                {
                    var response = await client.GetAsync(fullReqUrl);

                    var stringContent = await response.Content.ReadAsStringAsync();

                    clubs = DeserialiseContent<List<Club>>(stringContent);
                }
                catch (Exception ex)
                {
                    throw new Exception("Response from the request did not indicate success");
                }
            }

            return clubs;
        }

        public Task<List<ClubMember>> GetClubMembers(int clubId)
        {
            return null;
        }

        public Task<List<ClubMember>> GetClubMember(int clubMemberId)
        {
            return null;
        }

        public Task<JsonResult> AddClub()
        {
            return null;
        }

        public Task<JsonResult> UpdateClub(Club club)
        {
            return null;
        }

        public Task<JsonResult> AddClubMember()
        {
            return null;
        }

        public Task<JsonResult> UpdateClubMember()
        {
            return null;
        }

        public async Task<List<Club>> GetClubDetails(int clubId)
        {
            var clubs = new List<Club>();

            using (var client = GetHttpClient())
            {
                var fullReqUrl = "http://prettyapi/api/clubs/GetAllClubs";

                try
                {
                    var response = await client.GetAsync(fullReqUrl);

                    var stringContent = await response.Content.ReadAsStringAsync();

                    clubs = DeserialiseContent<List<Club>>(stringContent);
                }
                catch (Exception ex)
                {
                    throw new Exception("Response from the request did not indicate success");
                }
            }

            return clubs;
        }

        //[HttpPost]
        //public async Task<JsonResult> UpdateTarget(UpdateStaffSalesTarget command)
        //{
        //    return this.Json(await this.ExecuteJSONCommand(command), JsonRequestBehavior.AllowGet);
        //}


        private HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true;

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)"); // default user agent just in case
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("123456");

            return client;
        }

        public T DeserialiseContent<T>(string serialisedContent)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(serialisedContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot deserialise object {nameof(T)}: {ex.Message}");
            }
        }

    }
}