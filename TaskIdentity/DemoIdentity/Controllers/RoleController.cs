using Bl.Interfaces;
using DAL.Entity;
using DemoIdentity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DemoIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IAppRole appRoleRepo;
        public RoleController(IAppRole appRoleRepo)
        {
            this.appRoleRepo = appRoleRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await appRoleRepo.GetAll());
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var item = await appRoleRepo.Get(Id);
            if (item == null)
            {
                return BadRequest($"The {Id} Fot Found");
            }
            RoleDtoCRUD roleDtoCRUD = new RoleDtoCRUD()
            { 
                Id = item.Id,
                Name = item.Name
            };
            return Ok(roleDtoCRUD);
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleDtoCRUD roleDtoCRUD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DemoAppRole demoAppRole = new DemoAppRole() { 
                
                    Id = roleDtoCRUD.Id,
                    Name = roleDtoCRUD.Name
                
                };

                await appRoleRepo.Create(demoAppRole);
                await appRoleRepo.Save();
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit(Guid Id, RoleDtoCRUD roleDtoCRUD)
        {
            if (Id != roleDtoCRUD.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await appRoleRepo.Get(Id);
            if (item == null)
            {
                return BadRequest($"The {Id} Fot Found");
            }

            try
            {
                item.Name = roleDtoCRUD.Name;
                appRoleRepo.Update(item);
               await appRoleRepo.Save();
                return Ok($"item with Id {Id} Updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid Id)
        {


            var item = await appRoleRepo.Get(Id);
            if (item == null)
            {
                return BadRequest($"The {Id} Fot Found");
            }

            try
            {
                appRoleRepo.Delete(item);
                await appRoleRepo.Save();
                return Ok($"item with Id {Id} Deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
