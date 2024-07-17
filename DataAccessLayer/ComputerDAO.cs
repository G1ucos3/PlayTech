using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ComputerDAO
    {
        public static List<Computer> GetComputer()
        {
            var listComputer = new List<Computer>();
            try
            {
                using var db = new PlayTechContext();
                listComputer = db.Computers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listComputer;
        }

        public static void SaveComputer(Computer c)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Computers.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static void UpdateComputer(Computer c)
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

        public static void DeleteComputer(Computer c)
        {
            try
            {
                using var context = new PlayTechContext();
                var p1 = context.Computers.SingleOrDefault(o => o.ComputerId == c.ComputerId);
                context.Computers.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Computer GetComputerById(int id)
        {
            using var db = new PlayTechContext();
            return db.Computers.FirstOrDefault(c => c.ComputerId.Equals(id));
        }
    }
}
