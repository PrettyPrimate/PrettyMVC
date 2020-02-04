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
        //private const string url = "http://prettyapi/api/clubs/GetAllClubs";
        //private string urlParameters = "?api_key=123";

        public async Task<ActionResult> Index(int? selectedClubId, int? selectedClubMemberId)
        {
            var model = new ClubsViewModel();

            var clubs = await GetClubs();

            if (selectedClubId.HasValue)
            {
                var club = await GetClubDetails(selectedClubId.Value);

                model.SelectedClub = club;
            }

            model.Clubs = clubs;

            return View(model);
        }

        [HttpGet]
        public async Task<List<Club>> GetClubs()
        {
            var clubs = new List<Club>();

            using (var client = GetHttpClient())
            {
                var fullReqUrl = "http://prettyapi/api/clubs/GetAllClubs";

                var response = await client.GetAsync(fullReqUrl);

                var stringContent = await response.Content.ReadAsStringAsync();

                clubs = DeserialiseContent<List<Club>>(stringContent);
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

        public async Task<ActionResult> AddClub(string clubName, string addressLine1, string town, string postcode)
        {
            //var club = new Club();

            //using (var client = GetHttpClient())
            //{
            //    var fullReqUrl = "http://prettyapi/api/clubs/GetAllClubs";

            //    var response = await client.GetAsync(fullReqUrl);

            //    var stringContent = await response.Content.ReadAsStringAsync();

            //    club = DeserialiseContent<Club>(stringContent);
            //}




            return RedirectToAction("Index", new { selectedClubId = 1 });
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

        [HttpGet]
        public async Task<Club> GetClubDetails(int clubId)
        {
            var club = new Club();

            using (var client = GetHttpClient())
            {
                var fullReqUrl = $"http://prettyapi/api/clubs/GetClub?clubid={clubId}";

                var response = await client.GetAsync(fullReqUrl);

                var stringContent = await response.Content.ReadAsStringAsync();

                club = DeserialiseContent<Club>(stringContent);
            }

            //return Json(club, JsonRequestBehavior.AllowGet);
            return club;
        }

        public async Task<PartialViewResult> GetClubView(int clubId)
        {
            var club = await GetClubDetails(clubId);

            var model = new ClubsViewModel() { SelectedClub = club};

            return PartialView("_UpdateClub", model);
        }

        //[HttpPost]
        //public async Task<JsonResult> UpdateTarget(UpdateStaffSalesTarget command)
        //{
        //    return this.Json(await this.ExecuteJSONCommand(command), JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public async Task<JsonResult> DetachOrders(List<int> id)
        //{
        //    foreach (var item in id)
        //    {
        //        var result = await this.ExecuteCommandWithResult(new RemoveOrderNumberFromStockItem { StockReference = item });
        //        if (!result.Success)
        //        {
        //            return Json(result);
        //        }
        //    }

        //    return Json(new Result { Success = true, SuccessReference = string.Format("{0}  Stock References Corrected", id.Count) });
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