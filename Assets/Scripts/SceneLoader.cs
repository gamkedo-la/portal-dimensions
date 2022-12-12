using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private AsyncOperation loadLevelOperation = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null && loadLevelOperation == null)
        {
            loadLevelOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        
    }
}
