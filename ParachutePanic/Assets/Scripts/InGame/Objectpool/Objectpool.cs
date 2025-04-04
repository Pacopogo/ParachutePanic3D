using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//simple ObjectPool for the trash shoot
//tutorial from Bendux Youtube "Introduction To Object Pooling In Unity"

//Note: I added so the pool expands when it is out of inactive objects
//Note: I also added varied objects to the pool
public class Objectpool : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int _poolSize = 6;
    [SerializeField] private GameObject[] _objPrefab;

    [HideInInspector] public List<GameObject> PoolList = new List<GameObject>();

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
        AddPool(_poolSize);
    }

    public void AddPool(int amount)
    {
        for (int j = 0; j < _objPrefab.Length; j++)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(_objPrefab[j]);
                obj.SetActive(false);
                PoolList.Add(obj);
            }
        }
    }

    public GameObject PooledObject()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {
            if (!PoolList[i].activeInHierarchy)
            {
                return PoolList[i];
            }
        }
        return null;
    }

    public GameObject GetSpesificPool(int index)
    {
        GameObject obj = null;
        switch (index)
        {
            case 0:
                Debug.Log("Any");
                obj = GetAnyTrash();

                break;

            case 1:
                Debug.Log("EMP");
                obj = GetEMP();

                break;

            case 2:
                Debug.Log("Bulk");
                obj = GetBulk();

                break;
        }

        return obj;

    }
    private GameObject GetAnyTrash()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {
            if (!PoolList[i].activeInHierarchy)
            {
                return PoolList[i];
            }
        }

        return null;
    }
    private GameObject GetEMP()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {

            if (!PoolList[i].GetComponent<EMPTrash>())
                continue;

            if (!PoolList[i].activeInHierarchy)
            {
                return PoolList[i];
            }
        }

        return null;
    }
    private GameObject GetBulk()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {

            if (!PoolList[i].GetComponent<BulkyTrash>())
                continue;

            if (!PoolList[i].activeInHierarchy)
            {
                return PoolList[i];
            }
        }

        return null;
    }


    public void ClearObjects()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {
            PoolList[i].SetActive(false);
        }
        return;
    }

}
