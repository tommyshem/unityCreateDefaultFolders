using System.Threading.Tasks;
using UnityEditor;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;
using static UnityEngine.Debug;
using static UnityEngine.Windows.Directory;


namespace TomsTools
{
    public static class ToolsMakeFolders
    {
        [MenuItem("Tools/Setup/Add 3D Packages")]
        public static void Update3DUnityPackage()
        {
            AddUnityPackage("animation.rigging");
            AddUnityPackage("formats.fbx");
            AddUnityPackage("timeline");
            AddUnityPackage("ai.navigation");
            AddUnityPackage("behavior");
            AddUnityPackage("cinemachine"); 
        }

        [MenuItem("Tools/Setup/CleanPackages")]
        public static async void LoadNewManifest(){
            var content = await GetGistContent(GetGistUrl());
            ReplacePackageFile(content);
        }

        [MenuItem("Tools/Setup/Make Default Folders")]

        public static void CreateDefaultFolders()
        {
            // debugging information
            Log("Creating default folders...");
            Log(dataPath);
            // Create the root default folder
            CreateDirectory(Combine(dataPath, "_Project"));

            CreateFolders("_Project", new string[]
            {
                "Assets",
                "Assets/Animations",
                "Assets/Audio",
                "Assets/Fonts",
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
        
        private static string GetGistUrl(){
            return $"https://gist.github.com/tommyshem/ad0720e6c37484a2e1b375ffa11788f8/raw/06c7d3124a8bbe248aeeeb0502df6661a2afca5b/gistfile1.txt";
            
    }

    private static async Task<string> GetGistContent(string url){
        using var client = new System.Net.Http.HttpClient();
        var response = await client.GetAsync(url);
        string content = await response.Content.ReadAsStringAsync();
        return content;

    }
      
    private static void ReplacePackageFile(string contents){
        string existing = Combine(dataPath,"../Packages/manifest.json");
        System.IO.File.WriteAllText(existing, contents);
        UnityEditor.PackageManager.Client.Resolve();
    }

        private static void AddUnityPackage(string packageName) {
            UnityEditor.PackageManager.Client.Add($"com.unity.{packageName}");
        }


    }
}
