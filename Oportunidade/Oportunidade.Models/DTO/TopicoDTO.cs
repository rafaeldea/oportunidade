using System;
using System.Collections.Generic;
using System.Text;

namespace Oportunidade.Models.DTO
{
    public class TopicoDTO
    {
        public List<PalavraDTO> PalavrasDescricao { get; set; }
        public int TotalPalavrasDescricao { get; set; }

        public List<PalavraDTO> PalavrasConteudo { get; set; }
        public int TotalPalavrasConteudo { get; set; }
    }
}
