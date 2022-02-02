using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IInfo<T>
    {
        int Id { get; }
        T Item { get; }
        ChangeState State { get; }
    }
}
