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
                if (!dialogue.checkMachines)
                {
                    DialogueManager.instance.StartDialogue(dialogue, characterName);
                    return;
                }
                else if (dialogue.checkMachines)
                {
                    bool metConditions = true;

                    foreach (Machine mach in dialogue.machinesToCheck)
                    {
                        if (!mach.conditionMet)
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
