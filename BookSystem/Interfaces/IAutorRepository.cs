using BookSystem.Models;

namespace BookSystem.Interfaces
{
    public interface IAutorRepository
    {
        Task<Autor> GetByIdAsync(int? id);
        
        Task<Autor> GetByNameAsync(string nome);

        Task<IEnumerable<Autor>> GetAllAsync();

        Task<IEnumerable<string>> GetAutorByNameList();

        void Add(Autor autor);

        Task AddAsync(Autor autor); 

        void Update(Autor autor);

        void Delete(int? id);

        bool AutorExists(int id);

    }
}
