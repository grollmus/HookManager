using HookManager.Models;

namespace HookManager.ViewModels
{
    internal class HookViewModel : ViewModelBase
    {
        private bool _isInstalled;

        public HookViewModel(Hook hook)
        {
            Title = hook.Title;
            Description = hook.Description;
            Type = hook.Type;
            Model = hook;
        }

        public string Description { get; }

        public bool IsInstalled
        {
            get => _isInstalled;
            set
            {
                if (_isInstalled == value)
                    return;

                _isInstalled = value;
                OnPropertyChanged(nameof(IsInstalled));
            }
        }

        public Hook Model { get; }

        public string Title { get; }

        public HookType Type { get; }
    }
}