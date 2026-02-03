using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BundleLoader : MonoBehaviour
{
    public Text textReference;
    
    private const string BundleName = "rolnicza21/apartments";

    private void Start()
    {
#if !UNITY_EDITOR
        var path = Path.Combine(Application.streamingAssetsPath, BundleName);

        if (!File.Exists(path))
        {
            Debug.LogError($"Bundle not found at path: {path}");
            return;
        }

        var bundle = AssetBundle.LoadFromFile(path);
        if (bundle == null)
        {
            Debug.LogError("Failed to load AssetBundle");
            return;
        }

        var scenes = bundle.GetAllScenePaths();
        
        textReference.text = $"Loaded apartment scenes {scenes.Length}";
#endif
    }
}