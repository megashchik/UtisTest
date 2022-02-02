using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ICrud<T>
    {
        int AddOrUpdate(T entity);
        T[] Get(int page, int pageSize);
        T Get(int id);
        void Delete(int id);
    }
}
