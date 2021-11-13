using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //List of all machines in order to refresh the pipe network
    private List<InputMain> inputs = new List<InputMain>();

    public TextMeshProUGUI pipesLeftText;

    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInput playerInput;

    private void Start()
    {
        //Creates a list of inputs in the level
        foreach (GameObject inputObject in GameObject.FindGameObjectsWithTag("Input"))
        {
            if (inputObject.GetComponent<InputInlet>() != null)
            {
                if(!inputs.Contains(inputObject.GetComponent<InputInlet>().masterInput))
                {
                    inputs.Add(inputObject.GetComponent<InputInlet>().masterInput);
                }
            }
        }

        RefreshInputs();
    }

    //Refreshes the connection of all machines/pipes in the level
    public void RefreshInputs()
    {
        StartCoroutine(InputWait());
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

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    private IEnumerator InputWait()
    {
        if(inputs.Count > 0)
        {
            foreach (InputMain input in inputs)
            {
                input.StartSignal();
                yield return new WaitForSeconds(1f);
            }


            RefreshInputs();
        }
    }

}
