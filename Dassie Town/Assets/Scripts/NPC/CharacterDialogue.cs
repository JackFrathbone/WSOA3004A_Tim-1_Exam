using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterDialogue
{
    public string topic;
    public bool runOnce;
    public bool hasRun;
    [TextArea(3, 10)]
    public string[] sentences;

    [Header("Conditionals")]
    public bool checkMachines;
    public List<Machine> machinesToCheck;

    [Header("Events to Run on Finish")]
    public UnityEvent dialogueEvent;
}
