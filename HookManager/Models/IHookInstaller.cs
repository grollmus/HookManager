namespace HookManager.Models
{
    internal interface IHookInstaller
    {
        void InstallHook(Hook hook, string repositoryPath);
        void UninstallHook(Hook hook, string repositoryPath);
    }
}