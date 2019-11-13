using System.IO;
using System.Linq;
using System.Text;

namespace HookManager.Models
{
    internal class HookInstaller : IHookInstaller
    {
        private string AppendHook(Hook hook, string content)
        {
            var sb = new StringBuilder(content);
            sb.AppendLine(Constants.ScriptEntry + hook.ScriptFilePath);

            var path = PreparePath(hook.ScriptFilePath);

            if (IsPowershellFile(hook))
                sb.AppendLine($"powershell.exe -ExecutionPolicy RemoteSigned -Command {path} || exit 1");
            else
                sb.AppendLine($"cmd.exe /c \"{path}\" || exit 1");

            return sb.ToString();
        }

        private string CreateNewHookFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine("#!/bin/sh");

            return sb.ToString();
        }

        private int FindHook(string[] lines, Hook hook)
        {
            var prefixLength = Constants.ScriptEntry.Length;

            for (var i = 0; i < lines.Length; ++i)
            {
                if (lines[i].Length > prefixLength && lines[i].Substring(prefixLength) == hook.ScriptFilePath)
                    return i;
            }

            return -1;
        }

        private bool IsPowershellFile(Hook hook)
        {
            return Path.GetExtension(hook.ScriptFilePath)?.Trim('.') == "ps1";
        }

        private string PreparePath(string hookScriptFilePath)
        {
            return hookScriptFilePath.Replace('\\', '/');
        }

        public void InstallHook(Hook hook, string repositoryPath)
        {
            var hookPath = GitPathHelper.FindHookPath(repositoryPath);
            var fileName = GitPathHelper.HookFileName(hook.Type);
            var path = Path.Combine(hookPath, fileName);

            var content = File.Exists(path)
                ? File.ReadAllText(path)
                : CreateNewHookFile();

            content = AppendHook(hook, content);

            File.WriteAllText(path, content);
        }

        public void UninstallHook(Hook hook, string repositoryPath)
        {
            var hookPath = GitPathHelper.FindHookPath(repositoryPath);
            var fileName = GitPathHelper.HookFileName(hook.Type);
            var path = Path.Combine(hookPath, fileName);

            var lines = File.ReadAllLines(path);

            var hookIndex = FindHook(lines, hook);
            if (hookIndex != -1)
            {
                var list = lines.ToList();
                list.RemoveRange(hookIndex, 2);

                File.WriteAllLines(path, list);
            }
        }
    }
}