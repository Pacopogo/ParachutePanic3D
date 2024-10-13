using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneString(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}
