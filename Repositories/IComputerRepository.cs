using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IComputerRepository
    {
        void DeleteComputer(Computer c);
        ObservableCollection<Computer> GetComputer();
        Computer GetComputerById(int id);
        void SaveComputer(Computer c);
        void UpdateComputer(Computer c);
    }
}