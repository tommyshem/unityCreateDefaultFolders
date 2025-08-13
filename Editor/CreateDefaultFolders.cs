using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;
using static UnityEngine.Debug;
using static UnityEngine.Windows.Directory;


// ReSharper disable once CheckNamespace
namespace TomsTools.Editor
{
    public static class ToolsMakeFolders
    {
        // change to the correct gist you want to use
        private const string GistURL =
            "https://gist.githubusercontent.com/tommyshem/ad0720e6c37484a2e1b375ffa11788f8/raw/04117d04a880668ba074823c29bd6be6a3a618b8/gistfile1.txt";

        [MenuItem("Tools/Setup/Add 3D Packages")]
        public static void Add3DUnityPackage()
        {
            AddUnityPackage("animation.rigging");
            AddUnityPackage("formats.fbx");
            AddUnityPackage("timeline");
            AddUnityPackage("ai.navigation");
            AddUnityPackage("behavior");
            AddUnityPackage("cinemachine");
        }

        /// <summary>
        ///     Load file from GitHub gist link and
        ///     copy over the packages file with the
        ///     packages you want for a clean project to work on 
        /// </summary>
        /// <returns>None</returns>
        [MenuItem("Tools/Setup/Clean to Basic Packages")]
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
        [MenuItem("Tools/Setup/CleanPackages load from Gist file")]
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

        [MenuItem("Tools/Setup/Make Default Folders")]
        public static void CreateDefaultFolders()
        {
            // debugging information
            Log("Creating default folders...");
            Log(dataPath);
            // Create the root default folder
            CreateDirectory(Combine(dataPath, "_Project"));
            // 
            CreateFolders("_Project", new[]
            {
                "Assets",
                "Assets/Animations",
                "Assets/Audio",
                "Assets/Fonts",
                "Assets/Icons",
                "Assets/Materials",
                "Assets/Models",
                "Assets/Prefabs",
                "Assets/Scenes",
                "Assets/Scripts",
                "Assets/Sprites",
                "Assets/Textures",
                "Assets/UI"
            });
            Refresh();
        }

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

        //      private static string GetGistUrl(){
        //        return $"https://gist.github.com/tommyshem/ad0720e6c37484a2e1b375ffa11788f8/raw/c8bcf0195f316091d76b5343e0cd675f47aef37a/gistfile1.txt";

        //}

        private static async Task<string> GetGistContent(string url)
        {
            using var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        private static void ReplacePackageFile(string contents)
        {
            var existing = Combine(dataPath, "../Packages/manifest.json");
            System.IO.File.WriteAllText(existing, contents);
            Client.Resolve();
        }

        private static void AddUnityPackage(string packageName)
        {
            var mAddRequest = Client.Add($"com.unity.{packageName}");

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
    }
}
