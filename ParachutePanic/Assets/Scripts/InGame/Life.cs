using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private GameObject[] lifeLights;
    [SerializeField] private Material[] materials;
    private MeshRenderer currentMesh;
    private AudioSource audioSoruce;
    private SceneLoader sceneLoader;

    public int currentLife = 3;

    private void Start()
    {
        audioSoruce = GetComponent<AudioSource>();
        sceneLoader = GetComponent<SceneLoader>();
    }

    public void LoseLife()
    {
        currentLife--;
        currentLife = Mathf.Clamp(currentLife, 0, lifeLights.Length);

        for (int i = 0; i < lifeLights.Length; i++)
        {
            currentMesh = lifeLights[i].GetComponent<MeshRenderer>();
            if (currentLife <= i)
            {
                currentMesh.material = materials[1];
            }
            else
            {
                currentMesh.material = materials[0];

            }
        }

        if (currentLife <= 0)
            StartCoroutine(YouDied());

    }


    private IEnumerator YouDied()
    {
        yield return new WaitForSeconds(1);
        audioSoruce.Play();
        yield return new WaitForSeconds(audioSoruce.clip.length);
        sceneLoader.LoadSceneIndex(0);
    }
}
