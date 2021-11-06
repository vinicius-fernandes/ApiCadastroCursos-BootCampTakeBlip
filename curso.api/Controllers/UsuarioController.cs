using curso.api.Business.Entidades;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
using curso.api.Infraestrutura.Data;
using curso.api.Models;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authentication;

        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            IAuthenticationService authentication)
        {
            _usuarioRepository = usuarioRepository;
            _authentication = authentication;
        }

        /// <summary>
        /// Esse serviço permite autenticar um usuário cadastrado e ativdo
        /// </summary>
        /// <param name="loginViewModelInput">View model do login</param>
        /// <returns>Retorna status ok e dados do usuário em caso de sucesso</returns>
        [SwaggerResponse(statusCode:200,description:"Sucesso ao autenticar",Type =typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
           Usuario usuario =  _usuarioRepository.ObterUsuario(loginViewModelInput.Login);
            if(usuario == null)
            {
                return BadRequest("Erro ao obter usuário");
            }
            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo =1,
                Login = "Vinicius",
                Email = "vinicius@gmail.com",
            };

            var token = _authentication.GerarToken(usuarioViewModelOutput);
            return Ok(new { Token = token,
                Usuario = usuarioViewModelOutput,
                 });
        }
        /// <summary>
        /// Esse serviço permite o cadastro de usuários
        /// </summary>
        /// <param name="registroViewModelInput">Retorna status OK e as informações do usuári ocadastrado</param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao registrar", Type = typeof(RegistroViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput)
        {


            var usuario = new Usuario();
            usuario.Email = registroViewModelInput.Email;
            usuario.Senha = registroViewModelInput.Senha;
            usuario.Login = registroViewModelInput.Login;

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();
            return Created("", registroViewModelInput);
        }
    }
}
