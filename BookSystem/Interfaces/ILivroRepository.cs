using BookSystem.Models;

namespace BookSystem.Interfaces
{
    public interface ILivroRepository
    {
        Task<Livro> GetByIdAsync(int? id);

        Task<IEnumerable<Livro>> GetAllAsync();

        void Add(Livro livro);

        Task AddAsync(Livro livro);

        void Update(Livro livro);

        Task DeleteAsync(int? id);

        bool bookExists(int id);


    }
}
