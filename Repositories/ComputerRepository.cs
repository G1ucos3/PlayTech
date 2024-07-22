using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        public ObservableCollection<Computer> GetComputer() => ComputerDAO.GetComputer();

        public void SaveComputer(Computer c) => ComputerDAO.SaveComputer(c);

        public void UpdateComputer(Computer c) => ComputerDAO.UpdateComputer(c);

        public void DeleteComputer(Computer c) => ComputerDAO.DeleteComputer(c);

        public Computer GetComputerById(int id) => ComputerDAO.GetComputerById(id);
    }
}
