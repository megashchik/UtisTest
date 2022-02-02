using Model;

namespace Utis.WorkerIntegration.Server
{
    public partial class WorkerMessage : IPerson
    {

        Model.Sex IPerson.Sex => (Model.Sex)Sex;


        DateTime IPerson.Birthday
        {
            get => new DateTime(Birthday);
        }

    }
}
    