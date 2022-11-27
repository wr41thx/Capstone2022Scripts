using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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

    // Doors to other rooms, lights, and respective skeletons
    [SerializeField] private GameObject warDoor;
    [SerializeField] private GameObject pestDoor;
    [SerializeField] private GameObject famineDoor;
    [SerializeField] private GameObject warSkel;
    [SerializeField] private GameObject pestSkel;
    [SerializeField] private GameObject famineSkel;
    [SerializeField] private GameObject warLight;
    [SerializeField] private GameObject pestLight;
    [SerializeField] private GameObject famineLight;

    // Events for Death Room Upon Room Completions
    [SerializeField] private UnityEvent oneRoomComplete;
    [SerializeField] private UnityEvent twoRoomsComplete;
    [SerializeField] private UnityEvent allRoomsCompleted;
    
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
            warDoor.SetActive(false);
            warLight.SetActive(false);
            warSkel.SetActive(true);
        }

        if (persistentDM.GetFamineComplete())
        {
            scales.SetActive(true);
            scalesLight.GetComponent<PillarLightController>().ChangeColor(scalesLightColor);
            famineDoor.layer = 0;
            famineDoor.SetActive(false);
            famineLight.SetActive(false);
            famineSkel.SetActive(true);
        }

        if (persistentDM.GetPestComplete())
        {
            crown.SetActive(true);
            crownLight.GetComponent<PillarLightController>().ChangeColor(crownLightColor);
            pestDoor.layer = 0;
            pestDoor.SetActive(false);
            pestLight.SetActive(false);
            pestSkel.SetActive(true);
        }

        switch (persistentDM.GetSolveCount()) 
        {
            case 1:
                oneRoomComplete.Invoke();
                break;

            case 2:
                oneRoomComplete.Invoke();
                twoRoomsComplete.Invoke();
                break;

            case 3:
                oneRoomComplete.Invoke();
                twoRoomsComplete.Invoke();
                allRoomsCompleted.Invoke();
                break;
        }
        
    }

}
