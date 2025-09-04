using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using static System.IO.Path;
using static System.Net.WebRequestMethods;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;
using static UnityEngine.Debug;
using static UnityEngine.Windows.Directory;


// ReSharper disable once CheckNamespace
namespace TomsTools.Editor
{
    public static class ToolsMakeFolders
    {
        // Gist URL to load the default packages file from
        private const string GistURL =
              "https://gist.githubusercontent.com/tommyshem/ad0720e6c37484a2e1b375ffa11788f8/raw";

        // Default folders to create in a new project
        // You can change these to whatever you want
        private static string[] defaultFolders =
        {
            "Animations",
            "Audio",
            "Fonts",
            "Gizmos",
            "Icons",
            "Materials",
            "Models",
            "Prefabs",
            "Respurces",
            "Scenes",
            "Scripts",
            "Settings",
            "Sprites",
            "Textures",
            "UI"
        };

        // List of packages to remove from a new project
        // You can change these to whatever you want
        private static string[] packagesToRemove =
        {
            "com.unity.visualscripting",
            "com.unity.collab-proxy",
            "com.unity.multiplayer.center",
            "com.unity.test-framework",
            "com.unity.textmeshpro",
            "com.unity.timeline",
            "com.unity.ugui",
            "com.unity.ai.navigation",
            "com.unity.modules.xr",
            "com.unity.modules.vr",
            "com.unity.modules.unityanalytics",
            "com.unity.modules.unitywebrequest",
            "com.unity.modules.unitywebrequestassetbundle",
            "com.unity.modules.unitywebrequestaudio",
            "com.unity.modules.unitywebrequesttexture",
            "com.unity.modules.unitywebrequestwww",
            "com.unity.modules.accessibility"
        };

        // List of packages to add to a new project
        // You can change these to whatever you want
        private static string[] packages_3D_ToAdd =
        {
            "com.unity.cinemachine",
            "com.unity.animation.rigging",
            "com.unity.formats.fbx",
        };

        private static string[] packages_git_ToAdd = {
            "https://github.com/tommyshem/HierarchyPlus.git",
            "https://github.com/tommyshem/SimpleFolderIcon.git?path=Packages/com.seaeees.simple-folder-icon"

        };


        // --------------------------------------------------------------------------------
        // Public methods
        // --------------------------------------------------------------------------------


        /// <summary>
        ///     Add 3D packages to the project
        /// </summary>
        [MenuItem("Tools/TomsTools/Add git Packages", false, 4)]
        public static void AddGitUnityPackage()
        {
            AddPackages(packages_git_ToAdd);

        }

        /// <summary>
        ///     Add 3D packages to the project
        /// </summary>
        [MenuItem("Tools/TomsTools/Add 3D Packages", false, 3)]
        public static void Add3DUnityPackage()
        {
            AddPackages(packages_3D_ToAdd);

        }

        /// <summary>
        ///     Add 3D packages to the project
        /// </summary>
        [MenuItem("Tools/TomsTools/Remove unused Packages", false, 4)]
        public static void RemoveUnityPackage()
        {
            RemovePackages(packagesToRemove);

        }

        /// <summary>
        ///     Load file from GitHub gist link and
        ///     copy over the packages file with the
        ///     packages you want for a clean project to work on 
        /// </summary>
        [MenuItem("Tools/TomsTools/First Clean Setup (offline)", false, 1)]
        public static void LoadDefaultNewManifest()
        {
            try
            {
                ReplacePackageFile(Packages.GetPackages());
            }
            catch (Exception e)
            {
                Log("Loading default packages error " + e);
            }
        }

        /// <summary>
        ///     Load file from github gist link and
        ///     copy over the packages file with the
        ///     packages you want for a clean project to work on 
        /// </summary>
        /// <returns>None</returns>
        [MenuItem("Tools/TomsTools/First Clean Setup - load from Gist file", false, 2)]
        public static async void LoadNewManifest()
        {
            try
            {
                var content = await GetGistContent(GistURL);
                ReplacePackageFile(content);
            }
            catch (Exception e)
            {
                Log("Loading Gist error " + e);
            }
        }

        /// <summary>
        ///     Create a set of default folders for a new project
        /// </summary>
        [MenuItem("Tools/TomsTools/Make Default Folders", false, 0)]
        public static void CreateDefaultFolders()
        {
            // debugging information
            Log("Creating default folders...");
            Log(dataPath);
            // Create the root default folder
            CreateDirectory(Combine(dataPath, "_Project"));
            // 
            CreateFolders("_Project", defaultFolders);
            Refresh();
        }

        // --------------------------------------------------------------------------------
        // Private methods
        // --------------------------------------------------------------------------------

        /// <summary>
        ///     Create all the folders from the passed in from the string list    
        /// </summary>
        /// <param name="rootFolder"></param>
        /// <param name="folders"></param>
        /// <returns>None</returns>
        private static void CreateFolders(string rootFolder, string[] folders)
        {
            var rootPath = Combine(dataPath, rootFolder);
            Log($"Creating folders in: {rootPath}");
            foreach (var folder in folders)
            {
                var folderPath = Combine(rootPath, folder);
                if (!Exists(folderPath))
                {
                    CreateDirectory(folderPath);
                    Log($"Created folder: {folderPath}");
                }
                else
                {
                    Log($"Folder already exists: {folderPath}");
                }
            }
        }

        /// <summary>
        ///     Get the content of a Gist file
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<string> GetGistContent(string url)
        {
            using var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        /// <summary>
        ///     Replace the contents of the Packages/manifest.json file
        /// </summary>
        /// <param name="contents"></param>
        private static void ReplacePackageFile(string contents)
        {
            var existing = Combine(dataPath, "../Packages/manifest.json");
            System.IO.File.WriteAllText(existing, contents);
            Client.Resolve();
        }

        /// <summary>
        /// Add a list of Unity packages to the project
        /// </summary>
        private static void AddPackages(string[] packages)
        {
            foreach (var package in packages)
            {
                AddUnityPackage(package);
            }
        }

        /// <summary>
        ///     Add a Unity package to the project
        /// </summary>
        /// <param name="packageName"></param>
        private static void AddUnityPackage(string packageName)
        {
            var mAddRequest = Client.Add($"{packageName}");

            if (mAddRequest != null)
            {
                switch (mAddRequest.Status)
                {
                    case StatusCode.InProgress:
                        Log("Operation in progress...");
                        break;

                    case StatusCode.Success:
                        Log($"Successfully installed {packageName}");
                        break;

                    case StatusCode.Failure:
                        LogError($"Operation failed on Package: {packageName} Error: {mAddRequest.Error.message}");
                        break;
                }
            }
        }

        /// <summary>
        /// Remove a list of Unity packages from the project
        /// </summary>
        private static void RemovePackages(string[] packages)
        {
            foreach (var package in packages)
            {
                RemoveUnityPackage(package);
            }
        }

        /// <summary>
        ///     Remove a Unity package to the project
        /// </summary>
        /// <param name="packageName"></param>
        private static void RemoveUnityPackage(string packageName)
        {
            var mAddRequest = Client.Remove($"{packageName}");

            if (mAddRequest != null)
            {
                switch (mAddRequest.Status)
                {
                    case StatusCode.InProgress:
                        Log("Operation in progress...");
                        break;

                    case StatusCode.Success:
                        Log($"Successfully removed {packageName}");
                        break;

                    case StatusCode.Failure:
                        LogError($"Operation failed on Package: {packageName} Error: {mAddRequest.Error.message}");
                        break;
                }
            }
        }





    }
}
