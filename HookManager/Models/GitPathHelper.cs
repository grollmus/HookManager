using System;
using System.IO;

namespace HookManager.Models
{
    internal static class GitPathHelper
    {
        public static string FindHookPath(string repository)
        {
            var dir = new DirectoryInfo(repository);
            if (dir.Name == "hooks" && dir.Parent?.Name == ".git")
                return repository;

            return Path.Combine(dir.FullName, ".git", "hooks");
        }

        public static string HookFileName(HookType hookType)
        {
            switch (hookType)
            {
                case HookType.ApplyPatchMsg:
                    return "applypatch-msg";
                case HookType.PreApplyPatch:
                    return "pre-applypatch";
                case HookType.PostApplyPatch:
                    return "post-applypatch";
                case HookType.PreCommit:
                    return "pre-commit";
                case HookType.PrepareCommitMsg:
                    return "prepare-commit-msg";
                case HookType.CommitMsg:
                    return "commit-msg";
                case HookType.PostCommit:
                    return "post-commit";
                case HookType.PreRebase:
                    return "pre-rebase";
                case HookType.PostCheckout:
                    return "post-checkout";
                case HookType.PostMerge:
                    return "post-merge";
                case HookType.PreReceive:
                    return "pre-receive";
                case HookType.Update:
                    return "update";
                case HookType.PostReceive:
                    return "post-receive";
                case HookType.PostUpdate:
                    return "post-update";
                case HookType.PostRewrite:
                    return "post-rewrite";
                case HookType.PrePush:
                    return "pre-push";
                case HookType.PreMergeCommit:
                    return "pre-merge-commit";
                case HookType.PushToCheckout:
                    return "push-to-checkout";
                case HookType.PreAutoGc:
                    return "pre-auto-gc";
                case HookType.SendmailValidate:
                    return "sendemail-validate";
                case HookType.FsMonitorWatchman:
                    return "fsmonitor-watchman";
                case HookType.PostIndexChange:
                    return "post-index-change";
                case HookType.P4PreSubmit:
                    return "p4-pre-submit";
                default:
                    throw new ArgumentOutOfRangeException(nameof(hookType), hookType, null);
            }
        }
    }
}