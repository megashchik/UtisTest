using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PostgreSqlFactory : IUtisFactory
    {

        public WorkersRepository GetWorkersRepository()
        {
            return new WorkersRepository(() => new PostgreSqlContext());
        }
    }
}
