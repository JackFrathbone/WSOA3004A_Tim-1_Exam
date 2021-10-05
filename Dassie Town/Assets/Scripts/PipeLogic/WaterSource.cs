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

    public void StartFlow()
    {
        foreach (Pipe p in _connectedPipes)
        {
            p.RefreshPipeConnection(this);
        }
    }
}
