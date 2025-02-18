using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Scene loading functions based on Index or String (the scene name)
    public void LoadSceneString(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}
