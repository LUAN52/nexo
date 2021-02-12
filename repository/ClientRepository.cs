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

        public void Delete(Client entity)
        {
             Context.Users.Remove(entity);
            var save =Context.SaveChanges();

            if(save>0)
            {
                System.Console.WriteLine("salvou");
            }
            else
            {
                System.Console.WriteLine("nao salvou");
            }
        }

        public List<Client> GetAll()
        {
            return Context.Users.ToList();
        }

        public Client GetById (string id)
        {
            return Context.Users.Where(i=>i.Id==id).FirstOrDefault();
        }

        public void insert(Client entity)
        {
            Context.Add(entity);
            var result = Context.SaveChanges();

            if(result>0)
            {
                System.Console.WriteLine("inseriu com sucesso");
            }
            else
            {
                System.Console.WriteLine("nao inseriu");
            }

        }

        public List<Client> Query(Expression<System.Func<Client, bool>> filter)
        {
            var result = Context.Users.Where(filter);
            return result.ToList();
        }

        public void Update(Client entity)
        {
            Context.Users.Update(entity);
           var result = Context.SaveChanges();

            if(result>0)
            {
                System.Console.WriteLine("atualizou com exito");
            }
            else
            {
                System.Console.WriteLine("nao atualizou");
            }

        }

        
    }
}