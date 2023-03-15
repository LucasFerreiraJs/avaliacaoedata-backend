using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.ViewModel;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.ViewModel.Regions;

namespace api.Controllers


{
    [ApiController]
    [Route("[controller]")]
    public class RegionController:ControllerBase
    {

        [HttpGet]
        [Route("/v1/region")]
        public async Task<IActionResult> GetAsync(
               [FromServices] ApiDataContext context
            ) {

            try {

                var regions = await context.Regions
                    .AsNoTracking()
                    .Select(region => new ListRegionsViewModel { 
                        id = region.RegionID,
                        Name = region.Name
                    }).OrderBy(x => x.Name)
                    .ToListAsync();

                return Ok( new ResultViewModel<dynamic>(new { 
                    totalItems = regions.Count,
                    regions
                }));

            }
            catch (Exception err) {

                List<string> errList = new List<string>();
                errList.Add(err.Message);
                errList.Add("01RE - Falha interna no servidor");

                return StatusCode(500, new ResultViewModel<List<string>>(errList));
            } 
        }


        [HttpPost]
        [Route("/v1/region")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterRegionViewModel model,
            [FromServices] ApiDataContext context
            ) {

            var region = new Region {
                Name = model.Name,
            };



            try {
                await context.Regions.AddAsync(region);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    region
                }));
            
            } catch (Exception err)
            {
                List<string> errList = new List<string>();
                errList.Add(err.Message);
                errList.Add("02RE - Falha interna no servidor");

                return StatusCode(500, new ResultViewModel<List<string>>(errList));

            }
        }


    }
}
