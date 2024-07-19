using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CurrentComputerDAO
    {
        public static ObservableCollection<CurrentComputer> GetCurrentComputer()
        {
            var listCurrentComputer = new ObservableCollection<CurrentComputer>();
            try
            {
                using var db = new PlayTechContext();
                var CurrentComputer = db.CurrentComputers.ToList();
                listCurrentComputer = new ObservableCollection<CurrentComputer>(CurrentComputer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCurrentComputer;
        }

        public static void SaveCurrentComputer(CurrentComputer c)
        {
            try
            {
                using var context = new PlayTechContext();
                context.CurrentComputers.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static void UpdateCurrentComputer(CurrentComputer c)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static ObservableCollection<CurrentComputer> GetCurrentComputerByUserID(int userID)
        {
            try
            {
                using var db = new PlayTechContext();

                var currentComputers = db.CurrentComputers
                                        .Include(cc => cc.Computer)
                                        .Where(cc => cc.UserId == userID)
                                        .ToList();
                var listCurrentComputers = new ObservableCollection<CurrentComputer>(currentComputers);

                return listCurrentComputers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching CurrentComputers for UserID {userID}: {ex.Message}");
                return new ObservableCollection<CurrentComputer>(); 
            }
        }
    }
}
