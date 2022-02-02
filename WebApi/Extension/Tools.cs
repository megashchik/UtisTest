using Utis.WorkerIntegration.Server;
using Model;

namespace WebApi.Extension
{
    public static class Tools
    {
        public static WorkerMessage FromIPerson(this IPerson person)
        {
            return new WorkerMessage()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday.Ticks,
                Sex = (Utis.WorkerIntegration.Server.Sex)person.Sex,
                HaveChildren = person.HaveChildren
            };
        }
    }
}
