using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDeathRoomManager : MonoBehaviour
{
    // Manages Death Room - Does not get destroyed on scene load
    public static PersistentDeathRoomManager instance;
 
    [SerializeField] private bool warComplete;
    [SerializeField] private bool famineComplete;
    [SerializeField] private bool pestComplete;

    [SerializeField] private int solveCount;

    private void Awake()
    {
        solveCount = 0;
        warComplete = false;
        famineComplete = false;
        pestComplete = false;
    }

    // Singleton
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Getters and Setters for room completion status
    public void SetWarComplete() 
    {
        warComplete = true;
    }

    public void SetFamineComplete()
    {
        famineComplete = true;
    }

    public void SetPestComplete()
    {
        pestComplete = true;
    }

    public bool GetWarComplete() 
    {
        return warComplete;
    }

    public bool GetFamineComplete()
    {
        return famineComplete;
    }

    public bool GetPestComplete()
    {
        return pestComplete;
    }

    public void UpdateSolveCount() 
    {
        solveCount += 1;
    }

    public int GetSolveCount() 
    {
        return solveCount;
    }

}
