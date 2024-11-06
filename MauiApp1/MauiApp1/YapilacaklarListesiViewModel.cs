using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace MauiApp1
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _taskName;
        private bool _isCompleted;

        public string TaskName
        {
            get => _taskName;
            set
            {
                if (_taskName != value)
                {
                    _taskName = value;
                    OnPropertyChanged(nameof(TaskName));
                }
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class YapilacaklarListesiViewModel : BaseViewModel
    {
        private ObservableCollection<TaskItem> _tasks;
        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public ICommand ShowAddTaskCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveTasksCommand { get; }

        public YapilacaklarListesiViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            ShowAddTaskCommand = new Command(OnShowAddTask);
            EditCommand = new Command<TaskItem>(OnEditTask);
            DeleteCommand = new Command<TaskItem>(OnDeleteTask);
            SaveTasksCommand = new Command(OnSaveTasks);

            LoadTasks(); 
        }

        private async void OnShowAddTask()
        {
            string taskName = await Application.Current.MainPage.DisplayPromptAsync("Yeni Görev", "Görev adı girin:");

            if (!string.IsNullOrWhiteSpace(taskName))
            {
                Tasks.Add(new TaskItem { TaskName = taskName });
            }
        }

        private async void OnEditTask(TaskItem task)
        {
            string newTaskName = await Application.Current.MainPage.DisplayPromptAsync(
                "Görev Düzenle",
                "Görevi düzenleyin:",
                initialValue: task.TaskName
            );

            if (!string.IsNullOrWhiteSpace(newTaskName) && newTaskName != task.TaskName)
            {
                task.TaskName = newTaskName;
            }
        }

        private void OnDeleteTask(TaskItem task)
        {
            Tasks.Remove(task);
        }

        private void OnSaveTasks()
        {
            string tasksJson = JsonSerializer.Serialize(Tasks);
            Preferences.Set("Tasks", tasksJson);
        }

        private void LoadTasks()
        {
            string tasksJson = Preferences.Get("Tasks", null);

            if (!string.IsNullOrEmpty(tasksJson))
            {
                var tasks = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(tasksJson);
                Tasks = tasks ?? new ObservableCollection<TaskItem>();
            }
        }
    }
}
