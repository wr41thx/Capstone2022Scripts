using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInitializing : MonoBehaviour
{
    [SerializeField] private GameObject[] gamePieces;

    private void Awake()
    {
        for (int i = 0; i < gamePieces.Length; i++)
        {
            gamePieces[i].SetActive(false);
        }
    }

}
