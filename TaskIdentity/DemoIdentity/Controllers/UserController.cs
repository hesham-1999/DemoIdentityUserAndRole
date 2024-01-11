using Bl.Interfaces;
using DAL.Entity;
using DemoIdentity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DemoIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUser appUserRepo;
        private readonly string directoryPath;
        public UserController(IAppUser appUserRepo , IConfiguration configuratio)
        {
            this.appUserRepo = appUserRepo;
            directoryPath = configuratio.GetSection("ImageDir").Value;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await appUserRepo.GetAll());
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var item = await appUserRepo.Get(Id);
            if (item == null)
            {
                return BadRequest($"The {Id} Fot Found");
            }
            //user roleDtoCRUD = new RoleDtoCRUD()
            //{
            //    Id = item.Id,
            //    Name = item.Name
            //};
            return Ok(item);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] UserDtoCrud userDtoCrud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userDtoCrud.Image == null || userDtoCrud.Image.Length == 0)
            {
                return BadRequest("Invalid image");
            }
            try
            {
                string uploadsFolder = Path.Combine(directoryPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + userDtoCrud.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
               
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await userDtoCrud.Image.CopyToAsync(fileStream);
                }

                DemoAppUser demoAppUser = new DemoAppUser()
                {

                    Id = userDtoCrud.Id,
                    Name = userDtoCrud.Name ,
                    UserImage = filePath

                };

                await appUserRepo.Create(demoAppUser);
                await appUserRepo.Save();
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit(Guid Id,[FromForm] UserDtoCrud userDtoCrud)
        {
            if (Id != userDtoCrud.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await appUserRepo.Get(Id);
            if (item == null)
            {
                return BadRequest($"The {Id} Fot Found");
            }

            try
            {

                System.IO.File.Delete(item.UserImage);

                string uploadsFolder = Path.Combine(directoryPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + userDtoCrud.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await userDtoCrud.Image.CopyToAsync(fileStream);
                }
                item.Id = userDtoCrud.Id;
                item.Name = userDtoCrud.Name;
                item.UserImage = filePath;
                appUserRepo.Update(item);
                await appUserRepo.Save();
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

           
            var item = await appUserRepo.Get(Id);
            if (item == null)
            {
                return BadRequest($"The {Id} Fot Found");
            }

            try
            {
                System.IO.File.Delete(item.UserImage);
                appUserRepo.Delete(item);
                await appUserRepo.Save();
                return Ok($"item with Id {Id} Deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
