using BusinessObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAO
    {
        public static ObservableCollection<User> GetUser()
        {
            var listUser = new ObservableCollection<User>();
            try
            {
                using var db = new PlayTechContext();
                var User = db.Users.ToList();
                listUser = new ObservableCollection<User>(User);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listUser;
        }

        public static void SaveUser(User u)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Users.Add(u);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static void UpdateUser(User u)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Entry(u).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteUser(User u)
        {
            try
            {
                using var context = new PlayTechContext();
                var p1 = context.Users.SingleOrDefault(o => o.UserId == u.UserId);
                context.Users.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static User GetUserById(int id)
        {
            using var db = new PlayTechContext();
            return db.Users.FirstOrDefault(c => c.UserId.Equals(id));
        }
    }
}
