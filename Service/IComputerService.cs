using BusinessObjects;

namespace Service
{
    public interface IComputerService
    {
        void DeleteComputer(Computer c);
        List<Computer> GetComputer();
        Computer GetComputerById(int id);
        void SaveComputer(Computer c);
        void UpdateComputer(Computer c);
    }
}