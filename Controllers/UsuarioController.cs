using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Philotes.Data;
using Philotes.Models;

namespace Philotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        public UsuarioController(DataContext context)
        {
            _context = context; 
        }

        private void CriarPasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool>UsuarioExistente(string username)
        {
            if(await _context.Usuarios.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
    [HttpPost("Registrar")]
    public async Task<IActionResult> RegistrarUsuario(Usuario user)
    {
        try
        {
            if(await UsuarioExistente(user.Username))
                throw new System.Exception("Nome de usuario já existe");
            CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user.Id);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    private bool VerificarPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    [HttpPost("Autenticar")]
    public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
    {
        try
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));
            if (usuario == null)
            {
                throw new System.Exception("Usuário não encontrado");
            }
            else if (!VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
            {
                throw new System.Exception("Senha incorreta");
            }
            else 
            {
                usuario.DataAcesso = System.DateTime.Now;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario.Id);
            }
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenhaUsuario(Usuario credenciais)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));
                if (usuario == null) 
                    throw new System.Exception("Usuario não encontrado.");

                CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
                usuario.PasswordHash = hash; 
                usuario.PasswordSalt = salt;
                _context.Usuarios.Update(usuario);
                int linhaAfetada = await _context.SaveChangesAsync();
                return Ok(linhaAfetada);
            
            }
            catch (System.Exception ex)    
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List <Usuario> lista = await _context.Usuarios.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task <IActionResult> Delete(int Id)
        {
            try
            {
                Usuario usuarioAserExcluido = await _context.Usuarios.FirstOrDefaultAsync (us => us.Id == Id);
                _context.Usuarios.Remove(usuarioAserExcluido);
                int usuarioExcluido = await _context.SaveChangesAsync();
                return Ok(usuarioExcluido);
            }
            catch (Exception ex)
            {
                return BadRequest (ex.Message);
            }
        }

    }

}


