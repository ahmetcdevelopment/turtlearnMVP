using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.Persistance.Services
{
    public class ZrfRoleManager : IZrfRoleService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public ZrfRoleManager(IUnitOfWork UnitOfWork, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _UnitOfWork = UnitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;

        }
        public IDataResult<IList<RoleDTO>> FetchAllDtos()
        {
            //var all = _roleManager.Roles.
            var allQueryableRecords = _UnitOfWork.Roles.GetAllQueryableRecords();
            //var allQueryableRecords = _roleManager.Roles.Select(x => x.Id > 0);
            var RoleList = allQueryableRecords.ToList();
            //RoleList.ForEach(role =>
            //{
            //    role.Id = role.Id > 0 && claim.ClaimTypeId != null ?
            //    TableExtensions.GetTitleForId(claim.ClaimTypeId) : "Bulunamadı";
            //});
            return RoleList == null || RoleList.Count <= 0 ?
                new DataResult<List<RoleDTO>>(ResultStatus.Error, new List<RoleDTO>()) :
                new DataResult<List<RoleDTO>>(ResultStatus.Success, RoleList);
        }

        public IDataResult<IList<RoleDTO>> FetchAllDtosWithCheckeds(User user)
        {
            //var allEntities = TableExtensions.GetAllEntities();
            //var allTables = TableExtensions.GetSpecificTableObjectsList(allEntities);
            var checkedRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            //allTables.Add(new TableObjectDto { Value = 127, Text = "Özel Yetkiler" });
            var allQueryableRecords = _UnitOfWork.Roles.GetAllQueryableRecords();
            var RoleList = allQueryableRecords.ToList();
            //List<RoleDTO> claimByModulesList = new List<RoleDTO>();

            RoleList.ForEach(role =>
            {
                role.Checked = checkedRoles.Contains(role.Name)? "checked" : "";
                ////var claimsByTable = ClaimList.Where(claim => claim.ClaimTypeId == table.Value);
                //var roleCheckeds = checkedRoles.Where(x => x.Type == table.Text); //o modül için seçili olanlar
                //var dto = new ClaimsByModulesDTO();
                //dto.ModuleId = table.Value;
                //dto.ModuleName = table.Text;
                //dto.Claims = claimsByTable != null && claimsByTable.Count() > 0 ? claimsByTable.ToList() : null;
                //if (moduleCheckeds != null && moduleCheckeds.Count() > 0) //seçili olanlar varsa
                //{
                //    foreach (var claim in dto.Claims)
                //    {
                //        claim.Checked = moduleCheckeds.Any(x => x.Value == claim.ClaimValue) ? "checked" : "";
                //    }
                //}

                //claimByModulesList.Add(dto);

                ////claim.ClaimTypeStr = claim.ClaimTypeId > 0 && claim.ClaimTypeId != null ?

            });
            return RoleList == null || RoleList.Count <= 0 ?
            new DataResult<List<RoleDTO>>(ResultStatus.Error, new List<RoleDTO>()) :
            new DataResult<List<RoleDTO>>(ResultStatus.Success, RoleList);
        }

        public IDataResult<IList<RoleDTO>> FetchAllDtosByIds(List<int> selectedIds)
        {
            var allQueryableRecords = _UnitOfWork.Roles.GetAllQueryableRecords().Where(x => selectedIds.Contains(x.Id)); ;
            var RoleList = allQueryableRecords.ToList();
            //RoleList.ForEach(role =>
            //{
            //    role.Name = role.Id > 0 && role.Id != null ?
            //    TableExtensions.GetTitleForId(claim.ClaimTypeId) : "Bulunamadı";
            //});
            return RoleList == null || RoleList.Count <= 0 ?
                new DataResult<List<RoleDTO>>(ResultStatus.Error, new List<RoleDTO>()) :
                new DataResult<List<RoleDTO>>(ResultStatus.Success, RoleList);
        }
    }
}
