using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //List of all machines in order to refresh the pipe network
    private List<InputMain> inputs = new List<InputMain>();
    public InputMain inputPriority;

    public TextMeshProUGUI pipesLeftText;

    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInput playerInput;

    [SerializeField] GameObject pauseMenu;

    private bool gamePaused;

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

        PauseGame();
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
            if (!gamePaused)
            {
                pauseMenu.SetActive(true);
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        playerController.freezeMovement = true;
        playerInput.disableInput = true;
    }

    public void UnPauseGame()
    {
        gamePaused = false;
        playerController.freezeMovement = false;
        playerInput.disableInput = false;
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    private IEnumerator InputWait()
    {
        if (inputs.Count > 0)
        {
            foreach (InputMain input in inputs)
            {
                if(inputPriority != null)
                {
                    inputPriority.StartSignal();
                    yield return new WaitForSeconds(0.5f);
                    inputPriority = null;
                }

                input.StartSignal();
                yield return new WaitForSeconds(0.5f);
            }
        }

        RefreshInputs();

        yield return new WaitForSeconds(0.5f);
    }

}
