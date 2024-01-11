using Bl.Interfaces;
using DAL.Entity;
using DemoIdentity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        private readonly IAppUserAppRole appUserAppRoleRepo;
        public ManagerController(IAppUserAppRole appUserAppRoleRepo)
        {
            this.appUserAppRoleRepo = appUserAppRoleRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await appUserAppRoleRepo.GetAll());
        }

      
        [HttpPost]
        public async Task<ActionResult> AssignUserToRole(AppUserAppRoleDto appUserAppRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

             
                DemoAppUserAppRole demoAppUserAppRole = new DemoAppUserAppRole()
                {

                    AppUserId = appUserAppRoleDto.UserId,
                    AppRoleId = appUserAppRoleDto.RoleId

                };

                await appUserAppRoleRepo.Create(demoAppUserAppRole);
                await appUserAppRoleRepo.Save();
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveUserFromRole(AppUserAppRoleDto appUserAppRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

               
                var item = await appUserAppRoleRepo.GetBy(appUserAppRoleDto.UserId, appUserAppRoleDto.RoleId);
                appUserAppRoleRepo.Delete(item);
                await appUserAppRoleRepo.Save();
                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Edit(EditUserRole editUserRole)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
        

            try
            {
                var item = await appUserAppRoleRepo.GetBy(editUserRole.UserId, editUserRole.OldRoleId);
                item.AppRoleId = editUserRole.NewRoleId;


                appUserAppRoleRepo.Update(item);
                await appUserAppRoleRepo.Save();
                return Ok($"item with Id {item.AppUserId} Updated successfully with  {item.AppRoleId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
