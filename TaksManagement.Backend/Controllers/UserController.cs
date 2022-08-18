using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities;
using TaksManagement.Backend.Models;
using TaksManagement.Backend.Services;

namespace TaksManagement.Backend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAppService service;

        public UserController(UserAppService _service)
        {
            service = _service;
        }

        [HttpGet("list")]
        public ActionResult<List<UserModel>> GetUsers()
        {
            return service.GetUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUser(string id)
        {
            return service.GetUser(id);
        }

        [HttpPost]
        public ActionResult<UserModel> Create(UserModel user)
        {
            service.Create(user);

            return Created(nameof(user), user);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserModel userUpdate)
        {
            service.Update(userUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            service.Delete(id);
            return NoContent();
        }

    }
}
