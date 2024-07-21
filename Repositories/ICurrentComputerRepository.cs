using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface ICurrentComputerRepository
    {
        ObservableCollection<CurrentComputer> GetCurrentComputer();
        void SaveCurrentComputer(CurrentComputer c);
        void UpdateCurrentComputer(CurrentComputer c);
        ObservableCollection<CurrentComputer> GetCurrentComputerByUserID(int userID);

        public ObservableCollection<CurrentComputer> GetCurrentComputerByComputerID(int computerId);
    }
}