using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    private string sceneToLoad;

    void Start()
    {
        // Recupera il nome della scena target
        sceneToLoad = PlayerPrefs.GetString("TargetScene", "SimpleScene");

        // Avvia il caricamento
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
