using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResourceManagement.BAL.Interfaces;
using ResourceManagement.BAL.Models;
using ResourceManagement.BAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ResourceManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleApiController : ControllerBase
    {
        private readonly IRoleManager _roleService;
        public RoleApiController(IRoleManager RoleService)
        {
            _roleService = RoleService;
        }

        
        //[System.Web.Http.HttpGet]
        //public Object GetRoles()
        //{
        //    var data = _roleService.GetRoles();
        //    var json = JsonConvert.SerializeObject(data, Formatting.Indented,
        //        new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //        }
        //    );
        //    return json;
        //}
        [HttpGet("GetRoles")]
        public async Task<ActionResult> GetRoles()
        {
            return Ok(_roleService.GetRoles());
        }

        [HttpPost("SaveRole")]
        public async Task<ActionResult> SaveRole([FromBody] Role role)
        {
            return Ok(_roleService.SaveRole(role));
        }

        [HttpGet("CheckRole")]
        public async Task<ActionResult> CheckRole(string role)
        {
            return Ok(_roleService.CheckRole(role));
        }

        [HttpGet("DeleteRole")]
        public async Task<ActionResult> DeleteRole( int Id)
        {
            return Ok(_roleService.DeleteRole(Id));
        }
    }
}
