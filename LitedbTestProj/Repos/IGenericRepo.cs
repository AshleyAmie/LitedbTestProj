using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace LitedbTestProj.Repos
{
    public interface IGenericRepo<T> where T : class
    {
        ILiteCollection<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        bool Update(T obj);
        void Delete(int id);
        bool DeleteAll();
        void Save();
    }
}
