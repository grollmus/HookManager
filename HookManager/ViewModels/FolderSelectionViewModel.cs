using System.Windows.Input;
using HookManager.Models;
using HookManager.Mvvm;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HookManager.ViewModels
{
    internal class FolderSelectionViewModel : ViewModelBase
    {
        private readonly IConfiguration _configuration;
        private string _repositoryPath;

        private string _sharedFolderPath;

        public FolderSelectionViewModel(IConfiguration configuration)
        {
            _configuration = configuration;
            BrowseRepositoryCommand = new DelegateCommand(BrowseRepository);
            BrowseSharedFolderCommand = new DelegateCommand(BrowseSharedFolder);

            RepositoryPath = _configuration.Get("repositoryPath");
            SharedFolderPath = _configuration.Get("sharedFolderPath");
        }

        public ICommand BrowseRepositoryCommand { get; }
        public ICommand BrowseSharedFolderCommand { get; }

        public string RepositoryPath
        {
            get => _repositoryPath;
            set
            {
                if (_repositoryPath == value)
                    return;

                _repositoryPath = value;
                OnPropertyChanged(nameof(RepositoryPath));
                _configuration.Set("repositoryPath", value);
            }
        }

        public string SharedFolderPath
        {
            get => _sharedFolderPath;
            set
            {
                if (_sharedFolderPath == value)
                    return;

                _sharedFolderPath = value;
                OnPropertyChanged(nameof(SharedFolderPath));
                _configuration.Set("sharedFolderPath", value);
            }
        }

        private string BrowseFolder()
        {
            var dlg = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            var result = dlg.ShowDialog();

            if (result != CommonFileDialogResult.Ok)
                return null;

            return dlg.FileName;
        }

        private void BrowseRepository()
        {
            RepositoryPath = BrowseFolder() ?? RepositoryPath;
        }

        private void BrowseSharedFolder(object obj)
        {
            SharedFolderPath = BrowseFolder() ?? SharedFolderPath;
        }
    }
}