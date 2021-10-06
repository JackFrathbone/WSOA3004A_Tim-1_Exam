using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour
{
    public int sourceFlowAmount;

    private TextMeshPro _flowAmountLabel;

    public List<Pipe> _connectedPipes = new List<Pipe>();

    private void Start()
    {
        _flowAmountLabel = GetComponentInChildren<TextMeshPro>();
        _flowAmountLabel.text = sourceFlowAmount.ToString();
    }

    public void ReceiveSignal(Machine mach)
    {
        mach.ReturnSignal(this);
    }
}
