using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainScene : MonoBehaviour
{
    public string mainSceneName = "Undefined";

    public void ReturnToInvestmentScene()
    {
        StartCoroutine(ReturnToInvestmentSceneRoutine());
    }

    private IEnumerator ReturnToInvestmentSceneRoutine()
    {
        var mainScene = SceneManager.GetSceneByName(mainSceneName);
        if (!mainScene.isLoaded)
        {
            yield return SceneManager.LoadSceneAsync(mainSceneName, LoadSceneMode.Additive);
            mainScene = SceneManager.GetSceneByName(mainSceneName);
        }

        SceneManager.SetActiveScene(mainScene);
        
        for (var i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name != mainSceneName)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}