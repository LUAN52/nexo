using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nexo.Models;
using Nexo.data;

namespace nexo.repository
{
    public class ProductRepository : IRepository<Product>
    {
        
        public ProductRepository(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public int Delete(Product entity)
        {
             Context.Products.Remove(entity);
            var save =Context.SaveChanges();

           return save;
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(string id)
        {   
            var idI = int.Parse(id);
            
            return Context.Products.Where(i=>i.id==idI).FirstOrDefault();
        }

         public List<Product> GetByIdClient(string id)
        {   
            return Context.Products.Where(i=>i.Client_id==id).ToList();
        }

        public Product GetOneProductByIdClient(int id,string idClient)
        {
            var allProducts = GetByIdClient(idClient);
            var oneProduct = allProducts.Where(i=>i.id==id).FirstOrDefault();


            return oneProduct;
                   

        }
        public int insert(Product entity)
        {
             Context.Products.Add(entity);
           var result = Context.SaveChanges();

           return result;
        }

        public List<Product> Query(Expression<Func<Product, bool>> filter)
        {
            var result = Context.Products.Where(filter);
            return result.ToList();
        }

        public int Update(Product entity)
        {
             Context.Products.Update(entity);
           var result = Context.SaveChanges();

            return result;
        }
    }
}