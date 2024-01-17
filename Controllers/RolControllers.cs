using apiservicio.Models;
using apiservicio.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiservicio.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RolController
    {
        private readonly IRolService _IRolService;

        public RolController(IRolService iTemp)
        {
            this._IRolService = iTemp;
        }
        [HttpGet]
        public async Task<List<Rol>>GetList() 
        {
            return await _IRolService.GetList();
        }
        [HttpPost("AgregaActualiza")]
        public async Task<Rol> AgregaActualiza(
            int Id, string NombreRol, string t)
        {
            Rol l= new Rol();
            l.Id = Id;
            l.NombreRol = NombreRol;
            return await _IRolService.AgregaActualiza(l,t);
        }
    }
}
