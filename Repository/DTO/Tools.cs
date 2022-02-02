using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    internal static class Tools
    {
        public static Person Convert(this IPerson person)
        {
            return new Person()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday,
                HaveChildren = person.HaveChildren,
                Sex = person.Sex,
            };

        }
    }
}
