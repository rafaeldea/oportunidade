using Oportunidade.Models.DTO;
using System.Collections.Generic;

namespace Oportunidade.Interface.Negocio
{
    public interface ITopico
    {
        public List<TopicoDTO> UltimosTopicos();
    }
}
