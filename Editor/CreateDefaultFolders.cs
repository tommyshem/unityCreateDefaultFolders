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
        public static void CreateFolders(string rootFolder, string[] folders)
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
    }
}
