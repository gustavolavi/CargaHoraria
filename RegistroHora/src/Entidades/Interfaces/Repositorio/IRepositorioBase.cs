using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces.Repositorio
{
    public interface IRepositorioBase<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        T GetById(int id);
        List<T> GetAll();
    }
}
