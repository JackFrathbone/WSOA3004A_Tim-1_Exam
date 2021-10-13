using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Machine : MonoBehaviour
{
    public int flowInput;
    public int flowRequired;

    private TextMeshPro _flowAmountLabel;

    public List<Pipe> _connectedPipes = new List<Pipe>();

    public List<WaterSource> connectedSources = new List<WaterSource>();

    public UnityEvent machineEventPass;
    public UnityEvent machineEventFail;

    public bool conditionMet;
    public SpriteRenderer sprite;
    public Sprite originalVisual;
    public Sprite conditionMetVisual;


    private Coroutine currentCoroutine;

    private void Start()
    {
        _flowAmountLabel = GetComponentInChildren<TextMeshPro>();
        GetCurrentFlow();
    }

    private void CheckCondition()
    {
        _flowAmountLabel.text = flowInput.ToString() + "/" + flowRequired.ToString();

        //If flow is correct, then run the scripted event/or run an event if flow isnt correct ie open and close doors depending on machine state
        if (flowInput >= flowRequired)
        {
            conditionMet = true;
            machineEventPass.Invoke();

            if(conditionMetVisual != null)
            {
                sprite.sprite = conditionMetVisual;
            }
        }
        else
        {
            conditionMet = false;
            machineEventFail.Invoke();
            sprite.sprite = originalVisual;
        }
    }

    private void GetCurrentFlow()
    {
        flowInput = 0;

        foreach (WaterSource source in connectedSources)
        {
            flowInput += source.sourceFlowAmount;
        }

        CheckCondition();
    }

    public void StartSignal()
    {
        connectedSources.Clear();
        flowInput = 0;

        foreach (Pipe pipe in _connectedPipes)
        {
            pipe.PassSignal(this);
        }

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(WaitBeforeChangeFlow());
    }

    public void ReturnSignal(WaterSource source)
    {
        if (!connectedSources.Contains(source))
        {
            connectedSources.Add(source);
        }

        GetCurrentFlow();
    }

    private IEnumerator WaitBeforeChangeFlow()
    {
        yield return new WaitForSeconds(2f);
        GetCurrentFlow();
    }
}
