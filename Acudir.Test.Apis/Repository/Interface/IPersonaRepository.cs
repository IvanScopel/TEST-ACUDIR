using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Repository.Interface
{
    public interface IPersonaRepository
    {
        List<Persona> GetAll(string? nombre = null, int? edad = null);
        void Add(Persona persona);
        void Update(Persona persona);
    }

}
