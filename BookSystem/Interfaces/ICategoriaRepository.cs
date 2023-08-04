using BookSystem.Models;

namespace BookSystem.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetByIdAsync(int? id);

        Task<Categoria> GetByNameAsync(string nome);

        Task<IEnumerable<Categoria>> GetAllAsync();

        Task<IEnumerable<string>> GetCatByNameList();

        void Add(Categoria categoria);

        Task AddAsync(Categoria categoria);

        void Update(Categoria categoria);

        void Delete(int? id);

        bool bookExists(int id);
    }
}
