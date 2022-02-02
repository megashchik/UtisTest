using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ITraceableCrud<T> : ICrud<T>
    {
        IAsyncEnumerable<IInfo<IPerson>> GetUpdates();
    }
}
