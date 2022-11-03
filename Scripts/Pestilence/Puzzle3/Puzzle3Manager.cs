using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3Manager : MonoBehaviour
{
    [SerializeField] private GameObject puzzle3Piece;
    [SerializeField] private GameObject finalStairs;
    [SerializeField] private GameObject openLid;
    [SerializeField] private GameObject closedLid;

    private void OnTriggerEnter(Collider other)
    {
        ActivatePuzzle3();
    }

    private void ActivatePuzzle3()
    {
        finalStairs.SetActive(true);
        openLid.SetActive(true);
        closedLid.SetActive(false);
    }
}
