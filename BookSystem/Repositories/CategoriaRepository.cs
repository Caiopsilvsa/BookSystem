using BookSystem.Data;
using BookSystem.Interfaces;
using BookSystem.Models;
using BookSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookSystem.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly BookSystemContext _context;
        public CategoriaRepository(BookSystemContext context)
        {
            _context = context;
        }

        public void Add(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
        }

        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);
        }

        public bool bookExists(int id)
        {
            return (_context.Categoria?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async void Delete(int? id)
        {
            var CategoriaToDelet = await GetByIdAsync(id);

            _context.Categoria.Remove(CategoriaToDelet);
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categoria> GetByIdAsync(int? id)
        {
            return await _context.Categoria
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<Categoria> GetByNameAsync(string nome)
        {
            var categoria = await _context.Categoria.
                    Where(c => c.Nome == nome).FirstOrDefaultAsync();

            return categoria;
        }

        public async Task<IEnumerable<string>> GetCatByNameList()
        {
            IQueryable<string> categorias = from g in _context.Categoria
                                            select g.Nome;

            var catNames = await categorias.Distinct().ToListAsync();

            return catNames;
        }

        public void Update(Categoria categoria)
        {
            _context.Update(categoria);
        }
    }
}
