using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ChangeCommand
    {
        public int Id { get; init; }
        public ChangeState State { get; init; }

        public ChangeCommand(int id, ChangeState state)
        {
            Id = id;
            State = state;
        }
    }
}
