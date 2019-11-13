# HookManager

Manage git hooks for lokal repositories (currently only supported for Windows Clients)

## Usage

* Select folder of repository to edit
* Select folder where shared hook scripts are stored
* Install a hook from the list by simply checking it.
* Uninstall a hook by unchecking it.
* Multiple hooks per type are supported

## Shared Folder

The tool will search inside a folder for .json files and expects them to contain the following information:

```json
{
    "title": "Name of the Hook that will shown in the UI",
    "description": "A longer description text that will be shown in the UI",
    "scriptFileName": "The name of the of the script file in the same folder (.ps1 currently supported)",
    "type": "The hook type this scripts can be installed to (see next section)"
}
```

## Hook Types

* ApplyPatchMsg
* PreApplyPatch
* PostApplyPatch
* PreCommit
* PrepareCommitMsg
* CommitMsg
* PostCommit
* PreRebase
* PostCheckout
* PostMerge
* PreReceive
* Update
* PostReceive
* PostUpdate
* PreAutoGc
* PostRewrite
* PrePush
* PreMergeCommit
* PushToCheckout
* SendmailValidate
* FsMonitorWatchman
* P4PreSubmit
* PostIndexChange
