using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace LitedbTestProj.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T: class
    {
        private LiteDBContext context;
        private ILiteCollection<T> collection;

        public GenericRepo()
        {
            this.context = new LiteDBContext();
            collection = context.litedb.GetCollection<T>();
        }

        public GenericRepo(LiteDBContext c)
        {
            context = c;
            collection = context.litedb.GetCollection<T>();
        }

        public ILiteCollection<T> GetAll()
        {
            return collection;
        }

        public T GetById(int id)
        {
            return (T)collection.FindById(id);
        }

        public void Insert(T obj)
        {
            collection.Insert(obj);
        }

        public bool Update(T obj)
        {
            bool updated = collection.Update(obj);
            context.litedb.GetCollection<T>().Update(obj);
            return updated;
        }

        public void Delete(int id)
        {
            var existing = (T)collection.FindById(id);
            if (existing != null)
                collection.Delete(id);
        }

        public bool DeleteAll()
        {
            bool deleted;
            int totalCount, deletedCount;
            totalCount = collection.Count();
            deletedCount = collection.DeleteAll();
            if (totalCount == deletedCount)
                deleted = true;
            else
                deleted = false;
            return deleted;
        }

        public void Save()
        {

        }
    }
}
