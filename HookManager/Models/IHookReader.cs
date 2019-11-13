using System.Collections.Generic;

namespace HookManager.Models
{
    internal interface IHookReader
    {
        IEnumerable<Hook> ListAvailableHooks(string folder);
        IEnumerable<Hook> ListInstalledHooks(string repository);
    }
}