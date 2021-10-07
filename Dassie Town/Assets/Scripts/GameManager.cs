using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //List of all machines in order to refresh the pipe network
    private List<Machine> machines = new List<Machine>();

    public TextMeshProUGUI pipesLeftText;

    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInput playerInput;

    private void Start()
    {
        //Creates a list of machines in the level
        foreach (GameObject machineObject in GameObject.FindGameObjectsWithTag("Machine"))
        {
            if(machineObject.GetComponent<Machine>() != null)
            {
                machines.Add(machineObject.GetComponent<Machine>());
            }
        }
    }

    //Refreshes the connection of all machines/pipes in the level
    public void RefreshMachines()
    {
        foreach(Machine mach in machines)
        {
            mach.StartSignal();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PauseGame()
    {
        playerController.freezeMovement = true;
        playerInput.disableInput = true;
    }

    public void UnPauseGame()
    {
        playerController.freezeMovement = false;
        playerInput.disableInput = false;
    }
}
