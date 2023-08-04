using BookSystem.Data;
using BookSystem.Interfaces;
using BookSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSystem.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BookSystemContext _context;
        public LivroRepository(BookSystemContext bookSystemContext)
        {
            _context = bookSystemContext;
        }
        public void Add(Livro livro)
        {
            _context.Livro.Add(livro);
        }

        public async Task AddAsync(Livro livro)
        {
            await _context.Livro.AddAsync(livro);
        }

        public bool bookExists(int id)
        {
            return (_context.Livro?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task DeleteAsync(int? id)
        {
            var LivroToDelet = await GetByIdAsync(id);

            _context.Livro.Remove(LivroToDelet);
        }

        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _context.Livro
                .Include(a => a.Autor)
                .Include(c => c.Categoria)
                .ToListAsync();
        }

        public async Task<Livro> GetByIdAsync(int? id)
        {
            return await _context.Livro
                .Include(a => a.Autor)
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(a => a.ID == id);
        }

        public void Update(Livro livro)
        {
            _context.Update(livro);
        }
    }
}
