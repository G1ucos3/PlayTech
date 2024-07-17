using BusinessObjects;

namespace Repositories
{
    public interface IComputerRepository
    {
        void DeleteComputer(Computer c);
        List<Computer> GetComputer();
        Computer GetComputerById(int id);
        void SaveComputer(Computer c);
        void UpdateComputer(Computer c);
    }
}