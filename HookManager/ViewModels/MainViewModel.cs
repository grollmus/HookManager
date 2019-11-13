using System.ComponentModel;
using HookManager.Models;

namespace HookManager.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            var hookReader = new HookReader();
            var hookInstaller = new HookInstaller();
            var config = new Configuration();

            FolderSelection = new FolderSelectionViewModel(config);
            Management = new ManagementViewModel(hookReader, hookInstaller);
            UpdatePaths();

            FolderSelection.PropertyChanged += FolderSelection_PropertyChanged;
        }

        public FolderSelectionViewModel FolderSelection { get; }

        public ManagementViewModel Management { get; }

        private void FolderSelection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FolderSelection.RepositoryPath) || e.PropertyName == nameof(FolderSelection.SharedFolderPath))
                UpdatePaths();
        }

        private void UpdatePaths()
        {
            Management.UpdatePaths(FolderSelection.RepositoryPath, FolderSelection.SharedFolderPath);
        }
    }
}