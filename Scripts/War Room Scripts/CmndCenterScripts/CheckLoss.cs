using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLoss : MonoBehaviour
{

    // Ground detects if it has collided with a bomb and can return if it has been hit.
    // This will trigger the game over state in the game manager.

    [SerializeField] private bool isHit;

    private void Awake()
    {
        isHit = false;
    }

    public bool CheckIfHit() 
    {
        return isHit;
    }

    // For resetting the game after game over state.
    public void ResetIfHit() 
    {
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile") 
        {
            isHit = true;
        }
    }
}
