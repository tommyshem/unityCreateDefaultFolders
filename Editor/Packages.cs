// ReSharper disable once CheckNamespace

namespace TomsTools.Editor
{
    public static class Packages
    {
        // basic packages to start the project
        private const string BasicPackages =
            "{\n\"dependencies\": {\n\"com.unity.cinemachine\": \"3.1.4\",\n    \"com.unity.formats.fbx\": \"5.1.3\",\n    \"com.unity.ide.rider\": \"3.0.36\",\n    \"com.unity.ide.visualstudio\": \"2.0.23\",\n    \"com.unity.inputsystem\": \"1.14.1\",\n    \"com.unity.render-pipelines.universal\": \"17.1.0\",\n    \"com.unity.ugui\": \"2.0.0\",\n    \"com.unity.modules.animation\": \"1.0.0\",\n    \"com.unity.modules.assetbundle\": \"1.0.0\",\n    \"com.unity.modules.imageconversion\": \"1.0.0\",\n    \"com.unity.modules.imgui\": \"1.0.0\",\n    \"com.unity.modules.jsonserialize\": \"1.0.0\",\n    \"com.unity.modules.particlesystem\": \"1.0.0\",\n    \"com.unity.modules.physics\": \"1.0.0\",\n    \"com.unity.modules.ui\": \"1.0.0\",\n    \"com.unity.modules.uielements\": \"1.0.0\"\n  }\n}";

        private const string BasicP = @"{\""dependencies\"": {
\""com.unity.cinemachine\"": \""3.1.4\"",
    \""com.unity.formats.fbx\"": \""5.1.3\"",
    \""com.unity.ide.rider\"": \""3.0.36\"",
    \""com.unity.ide.visualstudio\"": \""2.0.23\"",
    \""com.unity.inputsystem\"": \""1.14.1\"",
    \""com.unity.render-pipelines.universal\"": \""17.1.0\"",
    \""com.unity.ugui\"": \""2.0.0\"",
    \""com.unity.modules.animation\"": \""1.0.0\"",
    \""com.unity.modules.assetbundle\"": \""1.0.0\"",
    \""com.unity.modules.imageconversion\"": \""1.0.0\"",
    \""com.unity.modules.imgui\"": \""1.0.0\"",
    \""com.unity.modules.jsonserialize\"": \""1.0.0\"",
    \""com.unity.modules.particlesystem\"": \""1.0.0\"",
    \""com.unity.modules.physics\"": \""1.0.0\"",
    \""com.unity.modules.ui\"": \""1.0.0\"",
    \""com.unity.modules.uielements\"": \""1.0.0\""\
 }
}""";

        public static string GetPackages()
        {
            return BasicPackages;
        }
    }
}