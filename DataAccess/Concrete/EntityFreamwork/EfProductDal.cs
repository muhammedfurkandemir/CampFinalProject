using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFreamwork
{
    //NuGet
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposiable pattern implementation of c#
            //using içerisindeki new lenen yapı ile iş bittiği anda garbage collectore hemen boşta kalan new in silinmesini söyler.
            //çünkü dbcontext yapıı maliyetli bir yapıdır.hızlıca temizlemeyi sağlar.
            using (NorthwindContext context=new NorthwindContext())
            {
                var addedEntity=context.Entry(entity);//veritabanı ile gelen entity i eşleştir.
                addedEntity.State = EntityState.Added;//veritabanındaki durumuna ekle
                context.SaveChanges();//değişiklikleri kaydet
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);//veritabanı ile gelen entity i eşleştir.
                deletedEntity.State = EntityState.Deleted;//veritabanından sil 
                context.SaveChanges();//değişiklikleri kaydet
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null
                ? context.Set<Product>().ToList() 
                : context.Set<Product>().Where(filter).ToList();
            }
            
        }


        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);//veritabanı ile gelen entity i eşleştir.
                updatedEntity.State = EntityState.Modified;//veritabanında güncelle
                context.SaveChanges();//değişiklikleri kaydet
            }
        }
    }
}
