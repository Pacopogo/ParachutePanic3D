using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//simple ObjectPool for the trash shoot
//tutorial from Bendux Youtube "Introduction To Object Pooling In Unity"

//Note: I added so the pool expands when it is out of inactive objects
public class Objectpool : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int poolSize = 6;
    [SerializeField] private GameObject objPrefab;

    [HideInInspector] public List<GameObject> poolList = new List<GameObject>();

    #region Singleton
    public static Objectpool Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private void Start()
    {
        //Inizilalize the pool
        this.AddPool(this.poolSize);
    }

    public void AddPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public GameObject PooledObject()
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                return poolList[i];
            }
        }
        return null;
    }

    public void ClearObjects()
    {
        for (int i = 0; i < this.poolList.Count; i++)
        {
            poolList[i].SetActive(false);
        }
        return;
    }

}
