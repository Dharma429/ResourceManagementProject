using Microsoft.AspNetCore.Mvc;
//using ResourceManagement.Common.HttpClient;
using ResourceManagementProject.HttpClient;
using ResourceManagementProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManagementProject.Controllers
{
    public class RoleController : Controller
    {
        //public HttpService RMHttpClient { get; set; }
        //public RoleController(HttpService log1)
        //{
        //    RMHttpClient = log1;

        //}

        HttpService RMHttpClient = new HttpService();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> SearchData()
        {
            List<RoleViewModel> list = await RMHttpClient.GetRequest<List<RoleViewModel>>("api/RoleApi/GetRoles");
            return Json(list);
        }

        public JsonResult SearchList([FromBody] string role)
        {
            RoleViewModel record =  RMHttpClient.GetRequestSync<RoleViewModel>("api/RoleApi/GetRoles");

            return Json(record != null ? true : false);
        }
        public JsonResult SearchList()
        {
            List<RoleViewModel> list =  RMHttpClient.GetRequestSync<List<RoleViewModel>>("api/Role/Get");
            return Json(list);
        }

        [HttpPost]
        public JsonResult SaveRoleDetails([FromBody] RoleViewModel role)
        {
            RMHttpClient.SendRequest<RoleViewModel,string>("api/RoleApi/SaveRole", role);
            return Json(new { res=1 });
        }

        [HttpPost]
        public JsonResult Delete([FromBody] string id)
        {
            RoleViewModel record = RMHttpClient.GetRequestSync<RoleViewModel>("api/RoleApi/GetRoles");

            return Json(new { });
        }
    }
}
