﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetAllToGrid()
        {
            var data = await _roleManager.Roles.ToListAsync();
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddRole(int? id)
        {
            var model = new RoleEditViewModel();
            if (id.HasValue)
            {
                var role = await _roleManager.FindByIdAsync(id.Value.ToString());
                //eğer var olan bir role çağırılıyorsa modele map ediyorum.
                model = _mapper.Map<RoleEditViewModel>(role);
            }
            return PartialView(model);
        }
        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public async Task<IActionResult> AddRole(RoleEditViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var ExistingRole = await _roleManager.FindByNameAsync(model.Name);
        //        ModelState.AddModelError(string.Empty, "Bu rol zaten mevcut.");
        //        return View("Error");
        //    }
        //    var newRole = 
        //}
    }
}
