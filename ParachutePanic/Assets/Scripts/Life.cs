using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private GameObject[] lifeLights;
    [SerializeField] private Material[] materials;
    private MeshRenderer currentMesh;
    private AudioSource audioSoruce;

    [SerializeField]private int currentLife = 3;

    
    public void LoseLife()
    {
        currentLife--;
        //audioSoruce.Play();
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

    }
}
