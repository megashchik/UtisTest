using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.DTO;

namespace Repository
{
    public class WorkersRepository : ICrud<IPerson>
    {
        Func<WorkersContext> GetContext { get; init; }

        internal WorkersRepository(Func<WorkersContext> getContext)
        {
            GetContext = getContext;
        }
        public int AddOrUpdate(IPerson person)
        {
            try
            {
                using var context = GetContext();
                if (context.Workers.Any(n => n.Id == person.Id))
                {
                    context.Workers.Update(person.Convert());
                    context.SaveChanges();
                    return person.Id;
                }
                else
                {
                    var entry = context.Workers.Add(person.Convert());
                    context.SaveChanges();
                    return entry.Entity.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var context = GetContext();
                var entity = context.Workers.Single(n => n.Id == id);
                context.Workers.Remove(entity);
                context.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                throw new Model.Exceptions.EntityNotFoundException("Entity with this id was not found", e);
            }
        }

        public IPerson[] Get(int page, int pageSize)
        {
            if (page < 0 || pageSize <= 0)
                throw new Model.Exceptions.EntityNotFoundException("These parameters are invalid. The page must be non-negative and the size positive");
            using var context = GetContext();
            var skip = pageSize * page;
            return context.Workers.Skip(skip).Take(pageSize).ToArray();
        }

        public IPerson Get(int id)
        {
            try
            {
                using var context = GetContext();
                return context.Workers.Single(n => n.Id == id);
            }
            catch(InvalidOperationException e)
            {
                throw new Model.Exceptions.EntityNotFoundException($"Entity with this id was not found", e);
            }
        }
    }
}
