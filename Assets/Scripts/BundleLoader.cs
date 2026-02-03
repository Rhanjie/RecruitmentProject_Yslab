using System;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BundleLoader : MonoBehaviour
{
    public Text textReference;
    public Button loadSceneButton;
    
    private static AssetBundle _loadedBundle;
    private string[] _apartmentScenes;
    
    private const string BundleName = "rolnicza21/apartments";

    private void Awake()
    {
        loadSceneButton.onClick.AddListener(LoadRandomApartment);
        loadSceneButton.gameObject.SetActive(false);
    }

    private void Start()
    {
#if !UNITY_EDITOR
        var path = Path.Combine(Application.streamingAssetsPath, BundleName);

        if (!File.Exists(path))
        {
            Debug.LogError($"Bundle not found at path: {path}");
            return;
        }

        if (_loadedBundle == null)
            _loadedBundle = AssetBundle.LoadFromFile(path);
        
        if (_loadedBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle");
            return;
        }

        _apartmentScenes  = _loadedBundle.GetAllScenePaths();
        
        textReference.text = $"Loaded apartment scenes {_apartmentScenes.Length}";
        
        loadSceneButton.gameObject.SetActive(true);
#endif
    }

    public void LoadRandomApartment()
    {
        if (_apartmentScenes == null || _apartmentScenes.Length == 0)
        {
            Debug.LogError("No apartment scenes available");
            return;
        }

        StartCoroutine(LoadApartmentRoutine());
    }

    private System.Collections.IEnumerator LoadApartmentRoutine()
    {
        var mainSceneName = SceneManager.GetActiveScene().name;
        
        var randomIndex = Random.Range(0, _apartmentScenes.Length);
        var scenePath = _apartmentScenes[randomIndex];
        var apartmentSceneName = Path.GetFileNameWithoutExtension(scenePath);

        Debug.Log($"Loading apartment scene: {apartmentSceneName}");
        
        yield return SceneManager.LoadSceneAsync(apartmentSceneName, LoadSceneMode.Additive);

        var apartmentScene = SceneManager.GetSceneByName(apartmentSceneName);
        SceneManager.SetActiveScene(apartmentScene);
        
        var mainScene = SceneManager.GetSceneByName(mainSceneName);
        if (mainScene.IsValid())
        {
            yield return SceneManager.UnloadSceneAsync(mainScene);
        }
    }
}