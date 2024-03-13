using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using Claim = turtlearnMVP.Domain.Entities.Claim;

namespace turtlearnMVP.Persistance.Services
{
    public class ClaimManager : IClaimService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public ClaimManager(IUnitOfWork UnitofWork, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _UnitOfWork = UnitofWork;
            _roleManager = roleManager;
            _userManager = userManager;

        }

        private static string _tableNameTR = TableExtensions.GetTableTitle<Claim>();

        public IQueryable<ClaimDTO> _QueryableClaims => _UnitOfWork.Claims.GetAllQueryableRecords();

        public IDataResult<IList<ClaimDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.Claims.GetAllQueryableRecords();
            var ClaimList = allQueryableRecords.ToList();
            ClaimList.ForEach(claim =>
            {
                claim.ClaimTypeStr = claim.ClaimTypeId > 0 && claim.ClaimTypeId != null ?
                TableExtensions.GetTitleForId(claim.ClaimTypeId) : "Bulunamadı";
            });
            return ClaimList == null || ClaimList.Count <= 0 ?
                new DataResult<List<ClaimDTO>>(ResultStatus.Error, new List<ClaimDTO>()) :
                new DataResult<List<ClaimDTO>>(ResultStatus.Success, ClaimList);
        }

        public IDataResult<IList<ClaimsByModulesDTO>> FetchAllDtosByModules()
        {
            var allEntities = TableExtensions.GetAllEntities();
            var allTables = TableExtensions.GetSpecificTableObjectsList(allEntities);
            allTables.Add(new TableObjectDto { Value = 127, Text = "Özel Yetkiler" });
            var allQueryableRecords = _UnitOfWork.Claims.GetAllQueryableRecords();
            var ClaimList = allQueryableRecords.ToList();
            List<ClaimsByModulesDTO> claimByModulesList = new List<ClaimsByModulesDTO>();
            //allTables.ForEach(module =>
            //{
            //    var model = new ClaimsByModulesDTO();
            //    model.ModuleId = module.Value;
            //    model.ModuleName = module.Text;
            //    var modelCLaims = ClaimList.Where(c => c.ClaimTypeId )
            //})
            allTables.ForEach(table =>
            {
                var claimsByTable = ClaimList.Where(claim => claim.ClaimTypeId == table.Value);
                var dto = new ClaimsByModulesDTO();
                dto.ModuleId = table.Value;
                dto.ModuleName = table.Text;
                dto.Claims = claimsByTable != null && claimsByTable.Count() > 0?  claimsByTable.ToList() : null;
                
                claimByModulesList.Add(dto);

                //claim.ClaimTypeStr = claim.ClaimTypeId > 0 && claim.ClaimTypeId != null ?

            });
            return ClaimList == null || ClaimList.Count <= 0 ?
            new DataResult<List<ClaimsByModulesDTO>>(ResultStatus.Error, new List<ClaimsByModulesDTO>()) :
            new DataResult<List<ClaimsByModulesDTO>>(ResultStatus.Success, claimByModulesList);
        }


        public IDataResult<IList<ClaimsByModulesDTO>> FetchAllDtosByModulesWithCheckeds(Role role)
        {
            var allEntities = TableExtensions.GetAllEntities();
            var allTables = TableExtensions.GetSpecificTableObjectsList(allEntities);
            var checkedClaims = _roleManager.GetClaimsAsync(role).GetAwaiter().GetResult();
            allTables.Add(new TableObjectDto { Value = 127, Text = "Özel Yetkiler" });
            var allQueryableRecords = _UnitOfWork.Claims.GetAllQueryableRecords();
            var ClaimList = allQueryableRecords.ToList();
            List<ClaimsByModulesDTO> claimByModulesList = new List<ClaimsByModulesDTO>();
            
            allTables.ForEach(table =>
            {
                var claimsByTable = ClaimList.Where(claim => claim.ClaimTypeId == table.Value);
                var moduleCheckeds = checkedClaims.Where(x => x.Type == table.Text); //o modül için seçili olanlar
                var dto = new ClaimsByModulesDTO();
                dto.ModuleId = table.Value;
                dto.ModuleName = table.Text;
                dto.Claims = claimsByTable != null && claimsByTable.Count() > 0 ? claimsByTable.ToList() : null;
               if(moduleCheckeds != null && moduleCheckeds.Count() > 0) //seçili olanlar varsa
                {
                    foreach(var claim in dto.Claims)
                    {
                        claim.Checked = moduleCheckeds.Any(x => x.Value == claim.ClaimValue) ? "checked" : "";
                    }
                }

                claimByModulesList.Add(dto);

                //claim.ClaimTypeStr = claim.ClaimTypeId > 0 && claim.ClaimTypeId != null ?

            });
            return ClaimList == null || ClaimList.Count <= 0 ?
            new DataResult<List<ClaimsByModulesDTO>>(ResultStatus.Error, new List<ClaimsByModulesDTO>()) :
            new DataResult<List<ClaimsByModulesDTO>>(ResultStatus.Success, claimByModulesList);
        }

        public IDataResult<IList<ClaimsByModulesDTO>> FetchAllDtosByModulesWithCheckeds(User user)
        {
            var allEntities = TableExtensions.GetAllEntities();
            var allTables = TableExtensions.GetSpecificTableObjectsList(allEntities);
            var checkedClaims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();
            allTables.Add(new TableObjectDto { Value = 127, Text = "Özel Yetkiler" });
            var allQueryableRecords = _UnitOfWork.Claims.GetAllQueryableRecords();
            var ClaimList = allQueryableRecords.ToList();
            List<ClaimsByModulesDTO> claimByModulesList = new List<ClaimsByModulesDTO>();

            allTables.ForEach(table =>
            {
                var claimsByTable = ClaimList.Where(claim => claim.ClaimTypeId == table.Value);
                var moduleCheckeds = checkedClaims.Where(x => x.Type == table.Text); //o modül için seçili olanlar
                var dto = new ClaimsByModulesDTO();
                dto.ModuleId = table.Value;
                dto.ModuleName = table.Text;
                dto.Claims = claimsByTable != null && claimsByTable.Count() > 0 ? claimsByTable.ToList() : null;
                if (moduleCheckeds != null && moduleCheckeds.Count() > 0) //seçili olanlar varsa
                {
                    foreach (var claim in dto.Claims)
                    {
                        claim.Checked = moduleCheckeds.Any(x => x.Value == claim.ClaimValue) ? "checked" : "";
                    }
                }

                claimByModulesList.Add(dto);

                //claim.ClaimTypeStr = claim.ClaimTypeId > 0 && claim.ClaimTypeId != null ?

            });
            return ClaimList == null || ClaimList.Count <= 0 ?
            new DataResult<List<ClaimsByModulesDTO>>(ResultStatus.Error, new List<ClaimsByModulesDTO>()) :
            new DataResult<List<ClaimsByModulesDTO>>(ResultStatus.Success, claimByModulesList);
        }

        public async Task<IDataResult<Claim>> GetById(int id)
        {
            var claim = await _UnitOfWork.Claims.GetByIdAsync(id);
            return claim == null || claim.Id <= 0 ?
                new DataResult<Claim>(ResultStatus.Error, Messages.ResultIsNotFound, new Claim()) :
                new DataResult<Claim>(ResultStatus.Success, claim);
        }

        public async Task<IResult> InsertAsync(Claim entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Claims.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(Claim entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Claims.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public IDataResult<IList<ClaimDTO>> FetchAllDtosByIds(List<int> selectedIds)
        {
            var allQueryableRecords = _UnitOfWork.Claims.GetAllQueryableRecords().Where(x => selectedIds.Contains(x.Id)); ;
            var ClaimList = allQueryableRecords.ToList();
            ClaimList.ForEach(claim =>
            {
                claim.ClaimTypeStr = claim.ClaimTypeId > 0 && claim.ClaimTypeId != null ?
                TableExtensions.GetTitleForId(claim.ClaimTypeId) : "Bulunamadı";
            });
            return ClaimList == null || ClaimList.Count <= 0 ?
                new DataResult<List<ClaimDTO>>(ResultStatus.Error, new List<ClaimDTO>()) :
                new DataResult<List<ClaimDTO>>(ResultStatus.Success, ClaimList);
        }

    }
}
