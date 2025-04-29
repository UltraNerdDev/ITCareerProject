using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class ParentBusiness
    {
        private SchoolRegistryContext context;

        public List<Parent> GetAll()
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Parents.ToList();
            }
        }

        public Parent Get(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Parents.Find(id);
            }
        }

        public void Add(Parent parent)
        {
            using (context = new SchoolRegistryContext())
            {
                context.Parents.Add(parent);
                context.SaveChanges();
            }
        }

        public void Update(Parent parent)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Parents.Find(parent.Id);
                if (item != null)
                {
                    context.Entry(item).CurrentValues.SetValues(parent);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Parents.Find(id);
                if (item != null)
                {
                    context.Remove(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
