using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using nexo.Models;
using Nexo.data;

namespace nexo.repository
{
    public class ClientRepository : IRepository<Client>
    {

        public ClientRepository(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public int Delete(Client entity)
        {
             Context.Users.Remove(entity);
            var save =Context.SaveChanges();

            return save;
        }

        public List<Client> GetAll()
        {
            return Context.Users.ToList();
        }

        public Client GetById (string id)
        {
            return Context.Users.Where(i=>i.Id==id).FirstOrDefault();
        }

        public int insert(Client entity)
        {
            Context.Add(entity);
            var result = Context.SaveChanges();

            return result;

        }

        public List<Client> Query(Expression<System.Func<Client, bool>> filter)
        {
            var result = Context.Users.Where(filter);
            return result.ToList();
        }

        public int Update(Client entity)
        {
            Context.Users.Update(entity);
           var result = Context.SaveChanges();

            return result;

        }

       

        
    }
}