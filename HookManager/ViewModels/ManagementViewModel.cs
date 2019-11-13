using System.Collections.ObjectModel;
using System.Linq;
using HookManager.Models;

namespace HookManager.ViewModels
{
    internal class ManagementViewModel : ViewModelBase
    {
        private readonly IHookInstaller _hookInstaller;
        private readonly IHookReader _hookReader;
        private string _repositoryPath;

        private HookViewModel _selectedHook;

        public ManagementViewModel(IHookReader hookReader, IHookInstaller hookInstaller)
        {
            _hookReader = hookReader;
            _hookInstaller = hookInstaller;
            Hooks = new ObservableCollection<HookViewModel>();
        }

        public ObservableCollection<HookViewModel> Hooks { get; }

        public HookViewModel SelectedHook
        {
            get => _selectedHook;
            set
            {
                if (_selectedHook == value)
                    return;

                _selectedHook = value;
                OnPropertyChanged(nameof(SelectedHook));
            }
        }

        private void HookViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!(sender is HookViewModel vm))
                return;

            if (e.PropertyName != nameof(HookViewModel.IsInstalled))
                return;

            if (vm.IsInstalled)
            {
                _hookInstaller.InstallHook(vm.Model, _repositoryPath);
            }
            else
            {
                _hookInstaller.UninstallHook(vm.Model, _repositoryPath);
            }
        }

        private void SubscribeItems()
        {
            foreach (var hookViewModel in Hooks)
            {
                hookViewModel.PropertyChanged += HookViewModel_PropertyChanged;
            }
        }

        private void UnsubscribeItems()
        {
            foreach (var hookViewModel in Hooks)
            {
                hookViewModel.PropertyChanged -= HookViewModel_PropertyChanged;
            }
        }

        public void UpdatePaths(string repositoryPath, string sharedFolderPath)
        {
            UnsubscribeItems();
            Hooks.Clear();

            _repositoryPath = repositoryPath;
            var installed = _hookReader.ListInstalledHooks(repositoryPath).ToList();
            var available = _hookReader.ListAvailableHooks(sharedFolderPath);

            foreach (var hook in available)
            {
                var vm = new HookViewModel(hook)
                {
                    IsInstalled = installed.Any(i => i.ScriptFileName == hook.ScriptFileName)
                };

                Hooks.Add(vm);
            }

            SubscribeItems();
        }
    }
}