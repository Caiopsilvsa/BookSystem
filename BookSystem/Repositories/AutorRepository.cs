using BookSystem.Data;
using BookSystem.Interfaces;
using BookSystem.Models;
using BookSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookSystem.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BookSystemContext _context;
        public AutorRepository(BookSystemContext context)
        {
            _context = context;
        }

        public void Add(Autor autor)
        {
            _context.Autor.Add(autor);
        }

        public async Task AddAsync(Autor autor)
        {
            await _context.Autor.AddAsync(autor);
        }

        public bool AutorExists(int id)
        {
            return (_context.Autor?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async void Delete(int? id)
        {
            var AutorToDelet = await GetByIdAsync(id);

            _context.Autor.Remove(AutorToDelet);
        }

        public async Task<IEnumerable<Autor>> GetAllAsync()
        {
            return await _context.Autor.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAutorByNameList()
        {
            IQueryable<string> autores = from a in _context.Autor
                                         select a.Nome;

            var autorNames = await autores.Distinct().ToListAsync();
            return autorNames;
        }

        public async Task<Autor> GetByIdAsync(int? id)
        {       
            return await _context.Autor.FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<Autor> GetByNameAsync(string nome)
        {
            var autor = await _context.Autor.
                    Where(a => a.Nome == nome).FirstOrDefaultAsync();

            return autor;
        }

        public void Update(Autor autor)
        {
            _context.Update(autor);
        }
    }
}
