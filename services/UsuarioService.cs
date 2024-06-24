using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class UsuarioService
    {
        private readonly LojaDbContext _dbContext;

        public UsuarioService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> GetUsuarioByLoginAsync(string login)
		{
			return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
		}

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUsuarioAsync(int id, Usuario usuario)
		{
			var existingUsuario = await _dbContext.Usuarios.FindAsync(id);
			if (existingUsuario != null)
			{
				existingUsuario.Login = usuario.Login;
				existingUsuario.Email = usuario.Email;
				if (!string.IsNullOrEmpty(usuario.Senha))
				{
					existingUsuario.Senha = HashPassword(usuario.Senha);
				}

				await _dbContext.SaveChangesAsync();
			}
		}

        public async Task DeleteUsuarioAsync(int id)
		{
			var usuario = await _dbContext.Usuarios.FindAsync(id);
			if (usuario != null)
			{
				_dbContext.Usuarios.Remove(usuario);
				await _dbContext.SaveChangesAsync();
			}
		}

		public bool ValidarSenha(string senha, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(senha, hashedPassword);
		}

		// Método para hashear uma senha
		private string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}
    }
}
