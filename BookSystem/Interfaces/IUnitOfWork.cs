namespace BookSystem.Interfaces
{
    public interface IUnitOfWork
    {
        public Task CommitAsync();

        public void Commit();

        public void Roolback();
    }
}
