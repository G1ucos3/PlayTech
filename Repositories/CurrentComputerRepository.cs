﻿using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CurrentComputerRepository : ICurrentComputerRepository
    {
        public ObservableCollection<CurrentComputer> GetCurrentComputer() => CurrentComputerDAO.GetCurrentComputer();

        public void SaveCurrentComputer(CurrentComputer c) => CurrentComputerDAO.SaveCurrentComputer(c);

        public void UpdateCurrentComputer(CurrentComputer c) => CurrentComputerDAO.UpdateCurrentComputer(c);

        public ObservableCollection<CurrentComputer> GetCurrentComputerByUserID(int userID) => CurrentComputerDAO.GetCurrentComputerByUserID(userID);
    }
}