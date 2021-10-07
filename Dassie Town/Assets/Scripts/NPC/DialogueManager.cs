using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] GameObject dialogueVisuals;
    [SerializeField] TextMeshProUGUI dialogueText;

    private CharacterDialogue currentDialogue;
    private int onDialogue;

    private void EnableVisuals()
    {
        dialogueVisuals.SetActive(true);
    }

    private void DisableVisuals()
    {
        dialogueVisuals.SetActive(false);
    }

    public void StartGreeting(string greeting)
    {
        EnableVisuals();
        dialogueText.text = greeting;

        GameManager.instance.PauseGame();
    }

    public void StartDialogue(CharacterDialogue dialogue)
    {
        EnableVisuals();
        onDialogue = 0;
        currentDialogue = dialogue;

        dialogueText.text = currentDialogue.sentences[onDialogue];

        GameManager.instance.PauseGame();
    }

    public void NextDialogue()
    {
        if(currentDialogue != null)
        {
            if (onDialogue >= currentDialogue.sentences.Length -1)
            {
                currentDialogue.dialogueEvent.Invoke();
                currentDialogue.hasRun = true;
                EndDialogue();
            }
            else
            {
                onDialogue++;
                dialogueText.text = currentDialogue.sentences[onDialogue];
            }
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        currentDialogue = null;
        DisableVisuals();

        GameManager.instance.UnPauseGame();
    }
}
