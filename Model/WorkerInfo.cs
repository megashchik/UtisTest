using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public record WorkerInfo : IInfo<IPerson>
    {
        public int Id { get; set; }
        public IPerson Item { get; set; } = null!;

        public ChangeState State {get; set; }
    }
}
