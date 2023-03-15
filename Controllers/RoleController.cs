using api.Data;
using api.Models;
using api.ViewModel;
using api.ViewModel.Roles;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {

        [HttpGet]
        [Route("/v1/role")]
        public async Task<IActionResult> GetAsync(
            [FromServices] ApiDataContext context
            )
        {
            try
            {
                //throw new Exception("teste msg erro");

                var roles = await context.Roles
                    .AsNoTracking()
                    .Select(role => new ListRolesViewModel
                    {
                        id = role.RoleID,
                        Name = role.Name
                    }).OrderBy(x => x.id)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    totalItems = roles.Count,
                    roles
                }));


            }
            catch (Exception err)
            {

                List<string> errList = new List<string>();
                errList.Add(err.Message);
                errList.Add("01RO - Falha interna no servidor");

                return StatusCode(500, new ResultViewModel<List<string>>(errList));

            }

        }

        [HttpPost]
        [Route("/v1/role")]
        public async Task<IActionResult> Post(
               [FromBody] RegisterRoleViewModel model,
               [FromServices] ApiDataContext context
            )
        {

            var role = new Role
            {
                RoleID = Guid.NewGuid(),
                Name = model.Name
            };

            try
            {
                await context.Roles.AddAsync(role);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new { roleName = role.Name }));

            }
            catch (Exception err)
            {

                List<string> errList = new List<string>();

                errList.Add("02RO - Falha interna no servidor");
                return StatusCode(500, new ResultViewModel<List<string>>(errList));
            }
        }
    }
}
