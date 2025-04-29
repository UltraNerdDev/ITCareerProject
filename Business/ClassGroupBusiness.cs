using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data;
using Data;
using Data.Models;

namespace Business
{
    public class ClassGroupBusiness
    {
        private SchoolRegistryContext context;

        public List<ClassGroup> GetAll()
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Classes.ToList();
            }
        }

        public ClassGroup Get(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Classes.Find(id);
            }
        }

        public void Add(ClassGroup classGroup)
        {
            using (context = new SchoolRegistryContext())
            {
                context.Classes.Add(classGroup);
                context.SaveChanges();
            }
        }

        public void Update(ClassGroup classGroup)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Classes.Find(classGroup.Id);
                if (item != null)
                {
                    context.Entry(item).CurrentValues.SetValues(classGroup);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Classes.Find(id);
                if (item != null)
                {
                    context.Remove(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
