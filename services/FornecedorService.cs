using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace Loja.Services
{
	public class FornecedorService
	{
		private readonly LojaDbContext _dbContext;

		public FornecedorService(LojaDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<Fornecedor>> GetAllFornecedoresAsync()
		{
			return await _dbContext.Fornecedores.ToListAsync();
		}

		public async Task<Fornecedor> GetFornecedorByIdAsync(int id)
		{
			return await _dbContext.Fornecedores.FindAsync(id);
		}

		public async Task AddFornecedorAsync(Fornecedor Fornecedor)
		{
			_dbContext.Fornecedores.Add(Fornecedor);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateFornecedorAsync(int id, Fornecedor Fornecedor)
		{
			var existingFornecedor = await _dbContext.Fornecedores.FindAsync(id);
			if (existingFornecedor != null)
			{
				existingFornecedor.CNPJ = Fornecedor.CNPJ;
				existingFornecedor.Nome = Fornecedor.Nome;
				existingFornecedor.Endereco = Fornecedor.Endereco;
				existingFornecedor.Email = Fornecedor.Email;
				existingFornecedor.Telefone = Fornecedor.Telefone;

				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task DeleteFornecedorAsync(int id)
		{
			var Fornecedor = await _dbContext.Fornecedores.FindAsync(id);
			if (Fornecedor != null)
			{
				_dbContext.Fornecedores.Remove(Fornecedor);
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}