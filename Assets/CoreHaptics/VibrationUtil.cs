using System.Runtime.InteropServices;
using System.IO;

#if UNITY_EDITOR && UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

public class VibrationUtil
{
#if !UNITY_EDITOR && UNITY_IOS
    [DllImport("__Internal")]
    private static extern void setupHapticEngine();

    [DllImport("__Internal")]
    private static extern void playHapticEngine(float intensity, float sharpness, float duration, float sustained);

    public static void SetupHapticEngine()
    {
        setupHapticEngine();
    }

    public static void PlayHapticEngine(float intensity, float sharpness, float duration)
    {
        float sustained = duration > 0 ? 1 : 0;
        playHapticEngine(intensity, sharpness, duration, sustained);
    }
#else
    public static void SetupHapticEngine()
    {
    }

    public static void PlayHapticEngine(float intensity, float sharpness, float duration)
    {
    }
#endif

#if UNITY_EDITOR
    [UnityEditor.Callbacks.PostProcessBuild(0)]
    public static void OnPostprocessBuild(UnityEditor.BuildTarget target, string path)
    {
        string projectPath = PBXProject.GetPBXProjectPath(path);
        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromString(File.ReadAllText(projectPath));
        string targetGuid = pbxProject.TargetGuidByName("Unity-iPhone");

        pbxProject.SetBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");
        pbxProject.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "UnityFramework-Swift.h");
        pbxProject.AddFrameworkToProject(targetGuid, "CoreHaptics.framework", false);

        File.WriteAllText(projectPath, pbxProject.WriteToString());
    }
#endif
}
