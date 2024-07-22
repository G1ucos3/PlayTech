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
    public class CurrentComputerService : ICurrentComputerService
    {
        private readonly ICurrentComputerRepository iCurrentComputerRepository;

        public CurrentComputerService()
        {
            iCurrentComputerRepository = new CurrentComputerRepository();
        }

        public ObservableCollection<CurrentComputer> GetCurrentComputer() => iCurrentComputerRepository.GetCurrentComputer();

        public void SaveCurrentComputer(CurrentComputer c) => iCurrentComputerRepository.SaveCurrentComputer(c);

        public void UpdateCurrentComputer(CurrentComputer c) => iCurrentComputerRepository.UpdateCurrentComputer(c);

        public ObservableCollection<CurrentComputer> GetCurrentComputerByUserID(int userID) => iCurrentComputerRepository.GetCurrentComputerByUserID(userID);

        public ObservableCollection<CurrentComputer> GetCurrentComputerByComputerID(int computerId) => iCurrentComputerRepository.GetCurrentComputerByComputerID(computerId);
        public void DeleteCurrentComputerByUserID(int userID) => iCurrentComputerRepository.GetCurrentComputerByUserID(userID);
        public void DeleteCurrentComputerByComputerID(int computerID) => iCurrentComputerRepository.GetCurrentComputerByComputerID(computerID);
    }
}
