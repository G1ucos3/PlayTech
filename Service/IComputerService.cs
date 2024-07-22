using BusinessObjects;
using System.Collections.ObjectModel;

namespace Service
{
    public interface IComputerService
    {
        void DeleteComputer(Computer c);
        ObservableCollection<Computer> GetComputer();
        Computer GetComputerById(int id);
        void SaveComputer(Computer c);
        void UpdateComputer(Computer c);
    }
}