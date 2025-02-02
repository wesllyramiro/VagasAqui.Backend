﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VA.Application.UseCase.Commands.CriarEmpresa;
using VA.Infrastructure.CrossCutting.Shared;

namespace VA.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpresaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CriarEmpresa(string empresa) 
        {
            var command = new CriarEmpresaCommand(empresa);
            var outPut = await _mediator.Send(command);

            if (!outPut.IsValid) 
            {
                return BadRequest(new Response(outPut.ErrorMessages));
            }

            return Ok(new Response(outPut.GetResult()));
        }
    }
}
