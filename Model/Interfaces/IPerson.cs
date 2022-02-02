using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IPerson
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string MiddleName { get; }
        Sex Sex { get; }
        DateTime Birthday { get; }
        bool HaveChildren { get; }
    }
}
