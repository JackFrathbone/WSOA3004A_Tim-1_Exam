using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputMain : MonoBehaviour
{
    public int flowInput;
    public int flowRequired;

    private TextMeshPro _flowAmountLabel;

    public List<Pipe> connectedPipes = new List<Pipe>();

    public List<OutputMain> connectedOutputs = new List<OutputMain>();

    public UnityEvent machineEventPass;
    public UnityEvent machineEventFail;

    public InputVisual inputVisual;
    public bool conditionMet;

    private Coroutine _currentCoroutine;

    private AudioSource _audioSource;
    public AudioClip waterPassSound;

    private void Start()
    {
        _audioSource = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();

        _flowAmountLabel = GetComponentInChildren<TextMeshPro>();
        GetCurrentFlow();
    }

    public void AddInputToPriorityList()
    {
        GameManager.instance.inputPriority = this;
    }

    private void CheckCondition()
    {
        _flowAmountLabel.text = flowInput.ToString() + "/" + flowRequired.ToString();

        //If flow is correct, then run the scripted event/or run an event if flow isnt correct ie open and close doors depending on machine state
        if (flowInput == flowRequired)
        {
            if (!conditionMet)
            {
                _audioSource.PlayOneShot(waterPassSound);
            }

            conditionMet = true;

            machineEventPass.Invoke();

            PumpCheck();

            if (inputVisual != null)
            {
                inputVisual.sprite.sprite = inputVisual.conditionalSprite;
            }
        }
        else
        {
            conditionMet = false;

            PumpClear();

            machineEventFail.Invoke();

            if (inputVisual != null)
            {
                inputVisual.sprite.sprite = inputVisual.originalSprite;
            }

        }
    }

    private void GetCurrentFlow()
    {
        flowInput = 0;

        foreach (OutputMain output in connectedOutputs)
        {
            flowInput += output.sourceFlowAmount;
        }
        CheckCondition();
    }

    //Checks if this machine is a pump, and then runs the pump scripts
    private void PumpCheck()
    {
        if (GetComponent<PumpInput>() != null)
        {
            GetComponent<PumpInput>().SetPumpFlow(flowInput);
        }
    }

    private void PumpClear()
    {
        if (GetComponent<PumpInput>() != null)
        {
            GetComponent<PumpInput>().SetPumpFlow(0);
        }
    }

    public void StartSignal()
    {
        connectedOutputs.Clear();
        flowInput = 0;

        foreach (Pipe pipe in connectedPipes)
        {
            pipe.PassSignal(this);
        }

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        _currentCoroutine = StartCoroutine(WaitBeforeChangeFlow());
    }

    public void ReturnSignal(OutputMain output)
    {
        if (!connectedOutputs.Contains(output))
        {
            connectedOutputs.Add(output);
        }
    }

    private IEnumerator WaitBeforeChangeFlow()
    {
        yield return new WaitForSeconds(1f);
        GetCurrentFlow();
    }
}
