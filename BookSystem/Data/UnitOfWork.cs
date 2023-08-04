using BookSystem.Interfaces;

namespace BookSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookSystemContext _context;
        public UnitOfWork(BookSystemContext context)
        {
            _context = context;
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Roolback()
        {
            //
        }
    }
}
