using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utis.WorkerIntegration;

namespace WpfClient.Model
{
    internal record Worker
    {
        private int Id { get; set; }
        string firstName = string.Empty;
        string lastName = string.Empty;
        string middleName = string.Empty;
        Sex sex;
        string birthDay = string.Empty;
        bool haveChildren = false;
        public string FirstName
        {
            get => firstName;
            set {
                IsLocal = true;
                firstName = value;
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                IsLocal = true;
                lastName = value;
            }
        }
        public string MiddleName
        {
            get => middleName;
            set
            {
                IsLocal = true;
                middleName = value;
            }
        }
        public Sex Sex
        {
            get => sex;
            set
            {
                IsLocal = true;
                sex = value;
            }
        }
        public string Birthday
        {
            get => birthDay;
            set
            {
                IsLocal = true;
                birthDay = value;
            }
        }
        public bool HaveChildren
        {
            get => haveChildren;
            set
            {
                IsLocal = true;
                haveChildren = value;
            }
        }

        public bool IsLocal { get; private set; } = true;

        public void ToLocal() => IsLocal = true;

        public virtual bool Equals(Worker? worker)
        {
            if (Id != 0 && Id == worker?.Id)
                return true;
            else
                return base.Equals(worker);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static implicit operator Worker (WorkerMessage message)
        {
            return new Worker()
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                MiddleName = message.MiddleName,
                Sex = (Sex)message.Sex,
                Birthday = DateOnly.FromDateTime(new DateTime(message.Birthday)).ToShortDateString(),
                HaveChildren = message.HaveChildren,
                IsLocal = false
            };
        }

        public static implicit operator WorkerMessage(Worker worker)
        {
            return new WorkerMessage()
            {
                Id = worker.Id,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                MiddleName = worker.MiddleName,
                Sex = (Utis.WorkerIntegration.Sex)worker.Sex,
                Birthday = DateTime.ParseExact(worker.Birthday, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture).Ticks,
                HaveChildren = worker.HaveChildren
            };
        }

        public static implicit operator Worker(int id) =>
            new Worker() { Id = id };

    }
}
