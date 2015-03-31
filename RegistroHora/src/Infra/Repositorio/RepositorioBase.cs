using Entidades.Interfaces.Repositorio;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioBase<T> :IDisposable, IRepositorioBase<T> where T : class
    {
        public Conexao db = new Conexao();

        public void Dispose()
        {
            db.Dispose();
        }

        public virtual void Add(T obj)
        {
            db.Set<T>().Add(obj);
            db.SaveChanges();
        }

        public virtual void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public virtual void Delete(T obj)
        {
            db.Set<T>().Remove(obj);
            db.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public virtual List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        
    }
}
