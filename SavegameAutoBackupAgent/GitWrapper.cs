using System;
using System.Collections.Generic;
using System.Environment;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using LibGit2Sharp;

namespace SavegameAutoBackupAgent
{
    public class GitWrapper
    {
        public static string CloneRepo(string localRepo, string remoteRepo)
        {
            var fullRepoName = GetProfilesFolder(localRepo);
            var val = Repository.Clone(remoteRepo, fullRepoName);
            return val;
        }

        public static void CloneProfilesRepo()
        {
            GitWrapper.CloneRepo(Properties.Settings.Default.ProfilesRepoName, Properties.Settings.Default.ProfilesRepo);
        }

        public static string GetProfilesFolder(string localRepoName)
        {
            var appDataPath = GetFolderPath(SpecialFolder.ApplicationData);

            var specificFolder = Path.Combine(appDataPath, System.AppDomain.CurrentDomain.FriendlyName);

            var localRepoFolder = Path.Combine(specificFolder, localRepoName);

            return localRepoFolder;
        }

        public static object CheckRepoStatus(string localRepo)
        {
            var retval = new List<object>();
            var fullRepoName = GetProfilesFolder(localRepo);

            using (var repo = new Repository(fullRepoName))
            {
                //repo.Fetch("origin");
                var remote = repo.Network.Remotes["origin"];
                repo.Network.Fetch(remote);
                repo.Fetch("origin");

                var status = repo.RetrieveStatus();
                retval.AddRange(status);
            }
            return retval;
        }
    }
}