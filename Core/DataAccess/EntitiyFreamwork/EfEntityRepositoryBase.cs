using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntitiyFreamwork
{
    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>
        where TEntity : class,IEntity, new()
        where TContext : DbContext,new()
    {
        public void Add(TEntity entity)
        {
            //IDisposiable pattern implementation of c#
            //using içerisindeki new lenen yapı ile iş bittiği anda garbage collectore hemen boşta kalan new in silinmesini söyler.
            //çünkü dbcontext yapıı maliyetli bir yapıdır.hızlıca temizlemeyi sağlar.
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);//veritabanı ile gelen entity i eşleştir.
                addedEntity.State = EntityState.Added;//veritabanındaki durumuna ekle
                context.SaveChanges();//değişiklikleri kaydet
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);//veritabanı ile gelen entity i eşleştir.
                deletedEntity.State = EntityState.Deleted;//veritabanından sil 
                context.SaveChanges();//değişiklikleri kaydet
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
            }

        }


        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);//veritabanı ile gelen entity i eşleştir.
                updatedEntity.State = EntityState.Modified;//veritabanında güncelle
                context.SaveChanges();//değişiklikleri kaydet
            }
        }
    }
}
