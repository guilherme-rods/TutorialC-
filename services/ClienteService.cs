using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
	public class ClienteService
	{
		private readonly LojaDbContext _dbContext;

		public ClienteService(LojaDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<Cliente>> GetAllClientsAsync()
		{
			return await _dbContext.Clientes.ToListAsync();
		}

		public async Task<Cliente> GetClientByIdAsync(int id)
		{
			return await _dbContext.Clientes.FindAsync(id);
		}
		public async Task AddClientAsync(Cliente Cliente)
		{
			_dbContext.Clientes.Add(Cliente);
			await _dbContext.SaveChangesAsync();
		}

		// Método para atualizar os dados de um Cliente
		public async Task UpdateClientAsync(int id, Cliente Cliente)
		{
			var existingCliente = await _dbContext.Clientes.FindAsync(id);
			if (existingCliente != null)
			{
				existingCliente.Nome = Cliente.Nome;
				existingCliente.Email = Cliente.Email;
				existingCliente.Cpf = Cliente.Cpf;

				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task DeleteClientAsync(int id)
		{
			var Cliente = await _dbContext.Clientes.FindAsync(id);
			if (Cliente != null)
			{
				_dbContext.Clientes.Remove(Cliente);
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}