using AccesoDatos.Scraping.Contexto;
using AutoMapper;
using Dto.Scraping.Request.Usuario;
using Dto.Scraping.Response;
using Dto.Scraping.Response.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositorios.Scraping.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Implementaciones
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {

        private readonly ScraperdbContext _contexto;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UsuarioRepositorio(ScraperdbContext contexto, IMapper mapper, IConfiguration config) : base(contexto)
        {
            _contexto = contexto;
            _mapper = mapper;
            _config = config;
        }
        public async Task<ResponseBase> Registrar(UsuarioRequest request)
        {
            var respuesta = new ResponseBase();

            try
            {

                var usuarioExistente = await _contexto.Usuarios
                    .FirstOrDefaultAsync(p => p.NombreUsuario.ToUpper() == request.NombreUsuario.ToUpper());

                if (usuarioExistente != null) throw new InvalidDataException("El nombre de usuario ya existe, ingrese otro.");

                var usuario = _mapper.Map<Usuario>(request);
                usuario.UsuarioRegistro = Environment.UserName;
                await AddAsync(usuario);
                respuesta.Success = true;
                respuesta.Message = "Usuario registrado exitosamente";
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = false;
            }
            return respuesta;
        }

        public async Task<ResponseBase<LoginResponse>> Login(LoginRequest request)
        {
            var respuesta = new ResponseBase<LoginResponse>();

            try
            {
                var jwtSettings = _config.GetSection("JwtSettings");

                var usuario = await _contexto.Usuarios
                    .Where(p => p.NombreUsuario == request.Usuario && p.Clave == request.Clave)
                    .FirstOrDefaultAsync();


                if (usuario != null)
                {

                    var fechaExpiracion = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"]));

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim(ClaimTypes.Email, usuario.Correo),
                        new Claim(ClaimTypes.Expiration, new DateTimeOffset(fechaExpiracion).ToUnixTimeSeconds().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                    };
                    var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
                    var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

                    var header = new JwtHeader(credenciales);

                    var payload = new JwtPayload(
                        jwtSettings["Issuer"],
                        jwtSettings["Audience"],
                        claims,
                        DateTime.Now,
                    fechaExpiracion);

                    var jwtToken = new JwtSecurityToken(header, payload);

                    respuesta.Data = new()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        Nombre = usuario.NombreUsuario
                    };
                    respuesta.Success = true;
                    respuesta.Message = "Inicio de sesión exitoso.";
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.Message = "Credenciales incorrectas.";
                }

            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = false;
            }
            return respuesta;
        }
    }
}
