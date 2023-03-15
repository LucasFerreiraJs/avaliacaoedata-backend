using api.Data;
using api.Models;
using api.Shared.Utils;
using api.ViewModel;
using api.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase

    {

        [HttpGet]
        [Route("/v1/user")]
        public async Task<IActionResult> GetAsync(
                [FromServices] ApiDataContext context,
                [FromQuery] int page = 0,
                [FromQuery] int pageSize = 20,
                [FromQuery] string? filterName= "",
                   [FromQuery] int? regionId = null,
                    [FromQuery] string? roleId = "",
                    [FromQuery] int? status = null
            )
        {
            try
            {
                var users = await context.Users
                    .AsNoTracking()
                    .Include(user => user.Role)
                    .Include(user => user.Region)
                    .Select(user => new ListUserViewModel
                    {
                        ID = user.ID,
                        Name = user.Name,
                        CPF = user.CPF,
                        Email = user.Email,
                        UserLogin = user.UserLogin,
                        Role = user.Role,
                        Region = user.Region,
                        Status = user.Status,
                        DateAction = user.DateAction,
                        LastAccess = user.LastAccess
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(user => user.LastAccess)
                    .ToListAsync();


                if (!filterName.IsNullOrEmpty())
                {
                    users = users.FindAll(user => user.Name.ToUpper().Contains(filterName.ToUpper()));
                }


                if (regionId != null)
                {
                    users = users.FindAll(user => user.Region.RegionID == regionId);
                }

                if (!roleId.IsNullOrEmpty())
                {
                    Guid filterroleId = new Guid(roleId);
                    users = users.FindAll(user => user.Role.RoleID == filterroleId);
                }


                if (status != null)
                {
                    users = users.FindAll(user => user.Status == status);
                }

                return Ok(new ResultViewModel<dynamic>(new
                {
                    totalItems = users.Count,
                    page,
                    pageSize,
                    users
                }));

            }
            catch (Exception err)
            {

                List<string> errList = new List<string>();
                errList.Add(err.Message);
                errList.Add("01US - Falha ao listar usuários");

                return StatusCode(500, new ResultViewModel<List<string>>(errList));
            }
        }


        [HttpPost]
        [Route("/v1/user")]
        public async Task<IActionResult> Post(
               [FromBody] RegisterUserViewModel model,
               [FromServices] ApiDataContext context

            )
        {
            try
            {

                //Role role = await context.Roles.FirstOrDefaultAsync(role => role.Name == model.RoleName);
                model.RoleId = new Guid(model.Role);


                Role role = await context.Roles.FirstOrDefaultAsync(role => role.RoleID == model.RoleId);
                if (role == null)
                {
                    throw new KeyNotFoundException("Grupo de acesso não encontrado");
                }

                Region region = await context.Regions.FirstOrDefaultAsync(region => region.RegionID == model.Region);
               
                if (region == null)
                {
                    throw new KeyNotFoundException("Região não encontrada");
                }

                var encrypt = new PasswordCrypt();
                model.Password = encrypt.toSHA512(model.Password);

                var user = new User
                {
                    ID = Guid.NewGuid(),
                    Name = model.Name,
                    Email = model.Email,
                    CPF = model.CPF.Replace(".", "").Replace("/","").Replace(" ","").Replace("-",""),
                    UserLogin = model.UserLogin,
                    Password = model.Password,
                    RhCode = model.RhCode,
                    Status = model.Status,
                    UserAction = Guid.NewGuid(),
                    DateAction = DateTime.Now,
                    LastAccess = DateTime.Now,
                    RoleID = role.RoleID,
                    RegionID = region.RegionID,
                
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var successUser = new ListUserViewModel
                {
                    ID = user.ID,
                    Name = user.Name,
                    Email = user.Email,
                    CPF = user.CPF,
                    UserLogin = user.UserLogin,
                    Role = role,
                    Region = region,
                    Status = user.Status,
                    DateAction= user.DateAction,
                    LastAccess = user.LastAccess,
                };

                return Ok(new ResultViewModel<dynamic>(new
                {
                    successUser
                }));
            }
            catch (Exception err)
            {

                List<string> errList = new List<string>();
                errList.Add(err.Message);
                errList.Add("02US- Falha ao registrar usuário");

                return StatusCode(500, new ResultViewModel<List<string>>(errList));
            }
        }

        

        [HttpDelete]
        [Route("/v1/user/{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            [FromServices] ApiDataContext context
            ) {

            try
            {

                var getId = new Guid(id);
                var getUser = await context.Users.FirstOrDefaultAsync(user => user.ID == getId);

                if (getUser == null)
                {
                    throw new Exception("Usuário não encontrado");
                }

                context.Users.Remove(getUser);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new { getUser }));

            }
            catch (Exception err) {

                List<string> errList = new List<string>();
                errList.Add(err.Message);
                errList.Add("03US- Falha ao registrar usuário");

                return StatusCode(500, new ResultViewModel<List<string>>(errList));

            }
        }

        [HttpPut]
        [Route("/v1/user/{id}")]
        public async Task<IActionResult> Put(
            [FromRoute] string id,
            [FromServices] ApiDataContext context,
            [FromBody] RegisterUserViewModel model
            ) {


            try {

                var getId = new Guid(id);
                var getUser = await context.Users.FirstOrDefaultAsync(user => user.ID == getId);
                model.RoleId = new Guid(model.Role);

                if (getUser == null) {
                    throw new Exception("Usuário não encontrado");
                }

                var role = await context.Roles.FirstOrDefaultAsync(item => item.RoleID == model.RoleId);

                if (role == null)
                {
                    throw new Exception("Grupo de acesso não encontrado");
                }

                var region = await context.Regions.FirstOrDefaultAsync(item => item.RegionID ==  model.Region);

                if (region == null)
                {
                    throw new Exception("Região não encontrada");
                }

                getUser.Name = model.Name;
                getUser.Email = model.Email;
                getUser.UserLogin = model.UserLogin;
                getUser.UserAction = Guid.NewGuid();
                getUser.LastAccess = DateTime.Now;
                getUser.RoleID = role.RoleID;
                getUser.RegionID = region.RegionID;
                getUser.Status = model.Status;
                getUser.LastAccess = DateTime.Now;

                context.Users.Update(getUser);
                await context.SaveChangesAsync();


                return Ok(getUser);


            } catch (Exception err) {

                List<string> errList = new List<string>();
                errList.Add(err.Message);
                errList.Add("04US- Falha ao registrar usuário");

                return StatusCode(500, new ResultViewModel<List<string>>(errList));

            }

        }
    }
}
