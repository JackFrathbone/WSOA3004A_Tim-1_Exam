using System.Collections.Generic;
using UnityEngine;

public class CharacteDialogueController : MonoBehaviour
{
    [Header("Character Info")]
    public string characterName;

    [Header("Dialogue")]
    public List<CharacterDialogue> dialogues;

    //For when there is no dialogue to run
    [Header("Generic Greeting")]
    [TextArea(3, 10)]
    public string greeting;

    public void ActivateDialogue()
    {
        foreach (CharacterDialogue dialogue in dialogues)
        {
            if (dialogue.runOnce && !dialogue.hasRun || !dialogue.runOnce)
            {
                if (!dialogue.checkInputs)
                {
                    DialogueManager.instance.StartDialogue(dialogue, characterName);
                    return;
                }
                else if (dialogue.checkInputs)
                {
                    bool metConditions = true;

                    foreach (InputMain input in dialogue.inputsToCheckPass)
                    {
                        if (!input.conditionMet)
                        {
                            metConditions = false;
                        }
                    }

                    foreach (InputMain input in dialogue.inputsToCheckFail)
                    {
                        if (input.conditionMet)
                        {
                            metConditions = false;
                        }
                    }

                    if (metConditions == true)
                    {
                        DialogueManager.instance.StartDialogue(dialogue, characterName);
                        return;
                    }
                }
            }
        }

        DialogueManager.instance.StartGreeting(greeting, characterName);
    }
}
