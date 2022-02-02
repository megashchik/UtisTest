using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfClient.Model;

namespace WpfClient.ViewModel
{
    internal class WorkerViewModel
    {
        WorkerModel Model { get; } = new WorkerModel();
        public Worker SelectedWorker { get; set; } = null!;
        public int SelectedIndex { get; set; }

        public ICommand New { get; init; }
        public ICommand Edit { get; init; }

        public ObservableCollection<Worker> Workers { get; } = new ObservableCollection<Worker>();

        public WorkerViewModel()
        {
            Workers.CollectionChanged += Workers_CollectionChanged;

#pragma warning disable VSTHRD012 // Указывайте JoinableTaskFactory, где это возможно
            New = new DelegateCommand(AddWorker);
#pragma warning restore VSTHRD012 // Указывайте JoinableTaskFactory, где это возможно

#pragma warning disable VSTHRD012 // Указывайте JoinableTaskFactory, где это возможно
            Edit = new DelegateCommand<Worker>(EditWorker);
#pragma warning restore VSTHRD012 // Указывайте JoinableTaskFactory, где это возможно
            
            InitAsync().ConfigureAwait(false);
        }

        private void Workers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is object)
                foreach (var item in e.OldItems)
                {
                    Worker worker = item as Worker;
                    if(!worker.IsLocal)
                        RemoveWorker(worker);
                }
        }

        void AddWorker()
        {
            Workers.Add(new Worker());
        }

        void EditWorker(Worker worker)
        {
            Model.Update(worker);
            Workers.Remove(worker);
        }

        void RemoveWorker(Worker worker)
        {
            Model.Delete(worker);
        }

        async Task InitAsync()
        {
            await foreach (var info in Model.GetWorkersAsync())
            {
                switch(info.State)
                {
                    case Utis.WorkerIntegration.State.Update:
                        if (Workers.Contains(info.Worker))
                        {
                            Workers.Remove(info.Worker);
                            Workers.Add(info.Worker);
                        }
                        else
                        {
                            Workers.Add(info.Worker);
                        }
                        break;
                    case Utis.WorkerIntegration.State.Remove:
                        var index = Workers.IndexOf(info.Id);
                        Workers[index].ToLocal();
                        Workers.RemoveAt(index);
                        break;
                }
            }
        }
    }
}
