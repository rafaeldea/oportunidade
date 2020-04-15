using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Oportunidade.Interface.Negocio;
using Oportunidade.Models.DTO;

namespace Oportunidade.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly ITopico _topico;

        public FeedController(ITopico topico)
        {
            _topico = topico;
        }

        [HttpGet]
        public object UltimosTopicos()
        {
            try
            {
                return Ok(new UltimosTopicosRS()
                {
                    Topicos = _topico.UltimosTopicos()
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}