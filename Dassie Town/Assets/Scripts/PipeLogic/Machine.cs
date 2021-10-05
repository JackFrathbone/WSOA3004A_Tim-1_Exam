using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Machine : MonoBehaviour
{
    public int flowInput;
    public int flowRequired;
    public bool connected;

    private TextMeshPro _flowAmountLabel;

    public List<Pipe> _connectedPipes = new List<Pipe>();

    public UnityEvent machineEvent;

    private void Start()
    {
        _flowAmountLabel = GetComponentInChildren<TextMeshPro>();
        RefreshFlow();
    }

    private void CheckCondition()
    {
        if(flowInput >= flowRequired)
        {
            machineEvent.Invoke();
        }
    }

    public void RefreshFlow()
    {
        flowInput = 0;

        foreach (Pipe p in _connectedPipes)
        {
            foreach (WaterSource source in p.connectedSources)
            {
                flowInput += source.sourceFlowAmount;
            }
        }

        if(flowInput == 0)
        {
            StartCoroutine(ReChecker());
        }

        _flowAmountLabel.text = flowInput.ToString();
        CheckCondition();
    }

    //The collision with the machine happens before the pipes collide with each other, so they arent yet connected when the machine checks; This simply waits a bit and tries again
    IEnumerator ReChecker()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (Pipe p in _connectedPipes)
        {
            foreach (WaterSource source in p.connectedSources)
            {
                flowInput += source.sourceFlowAmount;
            }
        }

        _flowAmountLabel.text = flowInput.ToString();
        CheckCondition();
    }
}
