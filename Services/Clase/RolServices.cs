using apiservicio.Business.Contracts;
using apiservicio.Models;
using apiservicio.Services.Contracts;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace apiservicio.Services.Clases
{
    public class RolService : IRolService

    {
        private readonly IRolRepository _IRolRepository;
        public RolService(IRolRepository tempI)
        {
            _IRolRepository = tempI;
        }
        public Task<List<Rol>> GetList()
        {
            return _IRolRepository.GetList();
        }
        public Task<Rol> AgregaActualiza(Rol l, string t)
        {
            return _IRolRepository.AgregaActualiza(l , t);
        }

       
    }
}