using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace HookManager.Models
{
    internal class HookReader : IHookReader
    {
        private IEnumerable<Hook> ExtractHooksFromGit(string pathToHookFile)
        {
            if (string.IsNullOrEmpty(pathToHookFile))
                yield break;

            var lines = File.ReadAllLines(pathToHookFile);
            if (!lines.Any())
                yield break;

            foreach (var line in lines)
            {
                if (!line.StartsWith(Constants.ScriptEntry))
                    continue;

                var scriptFilePath = line.Substring(Constants.ScriptEntry.Length);
                var metaFilePath = Path.ChangeExtension(scriptFilePath, ".json");
                if (File.Exists(metaFilePath))
                {
                    var json = File.ReadAllText(metaFilePath);
                    var hook = JsonConvert.DeserializeObject<Hook>(json);
                    yield return hook;
                }
            }
        }

        private Hook SetHookPath(Hook hook, string folder)
        {
            hook.ScriptFilePath = Path.Combine(folder, hook.ScriptFileName);
            return hook;
        }

        public IEnumerable<Hook> ListAvailableHooks(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return Enumerable.Empty<Hook>();

            var files = Directory.EnumerateFiles(folder, "*.json");
            return files.Select(File.ReadAllText).Select(JsonConvert.DeserializeObject<Hook>).Select(h => SetHookPath(h, folder));
        }

        public IEnumerable<Hook> ListInstalledHooks(string repository)
        {
            if (string.IsNullOrEmpty(repository))
                yield break;

            repository = GitPathHelper.FindHookPath(repository);

            foreach (var hookType in Enum.GetValues(typeof(HookType)).OfType<HookType>())
            {
                var fileName = GitPathHelper.HookFileName(hookType);
                var pathToHookFile = Path.Combine(repository, fileName);

                if (File.Exists(pathToHookFile))
                {
                    var installedHooks = ExtractHooksFromGit(pathToHookFile);
                    foreach (var installedHook in installedHooks)
                    {
                        yield return installedHook;
                    }
                }
            }
        }
    }
}