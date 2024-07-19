using BusinessObjects;
using DataAccessLayer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository iComputerRepository;

        public ComputerService()
        {
            iComputerRepository = new ComputerRepository();
        }

        public ObservableCollection<Computer> GetComputer() => iComputerRepository.GetComputer();

        public void SaveComputer(Computer c) => iComputerRepository.SaveComputer(c);

        public void UpdateComputer(Computer c) => iComputerRepository.UpdateComputer(c);

        public void DeleteComputer(Computer c) => iComputerRepository.DeleteComputer(c);

        public Computer GetComputerById(int id) => iComputerRepository.GetComputerById(id);
    }
}
