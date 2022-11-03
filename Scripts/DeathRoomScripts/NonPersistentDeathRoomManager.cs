using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonPersistentDeathRoomManager : MonoBehaviour
{
    [SerializeField] private PersistentDeathRoomManager persistentDM;
    [SerializeField] private GameObject player;
    
    // Pillar Objects
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject scales;
    [SerializeField] private GameObject crown;

    // Pillar Lamps and colors
    [SerializeField] private GameObject swordLight;
    [SerializeField] private GameObject scalesLight;
    [SerializeField] private GameObject crownLight;
    [SerializeField] private Color swordLightColor;
    [SerializeField] private Color scalesLightColor;
    [SerializeField] private Color crownLightColor;

    // Doors to other rooms
    [SerializeField] private GameObject warDoor;
    [SerializeField] private GameObject pestDoor;
    [SerializeField] private GameObject famineDoor;
    
    private void Awake()
    {
        persistentDM = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PersistentDeathRoomManager>();
        CheckRoomCompletion();
    }

    

    // Checks room completion on each load, and sets appropriate objects in scene
    // This Currently:
    // - Activates appropriate item on pillar in death room when matching room is completed.
    // - Changes light above pillar to appropriate color when matching room is completed.
    // - Switch Door layer to non interactable layer (rooms cannot be revisited once completed)
    // - Checks win condition for game via number of rooms solved
    private void CheckRoomCompletion() 
    {
        if (persistentDM.GetWarComplete())
        {
            sword.SetActive(true);
            swordLight.GetComponent<PillarLightController>().ChangeColor(swordLightColor);
            warDoor.layer = 0;
        }

        if (persistentDM.GetFamineComplete())
        {
            scales.SetActive(true);
            scalesLight.GetComponent<PillarLightController>().ChangeColor(scalesLightColor);
            famineDoor.layer = 0;
        }

        if (persistentDM.GetPestComplete())
        {
            crown.SetActive(true);
            crownLight.GetComponent<PillarLightController>().ChangeColor(crownLightColor);
           pestDoor.layer = 0;
        }

        if (persistentDM.GetSolveCount() >= 3) 
        {
            Debug.Log("ALL ROOMS SOLVED, HUZZAH!");
        }
    }

}
