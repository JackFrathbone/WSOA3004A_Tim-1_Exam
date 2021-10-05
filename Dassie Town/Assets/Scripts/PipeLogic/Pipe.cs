using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public List<Pipe> _connectedPipes = new List<Pipe>();

    public List<WaterSource> connectedSources = new List<WaterSource>();
    public int currentFlow;

    public List<Machine> connectedMachines = new List<Machine>();

    public bool sourceDirectlyConnected;
    public bool machineDirectlyConnected;

    public bool cleared;

    public void RefreshPipeConnection(WaterSource s)
    {
        connectedSources.Clear();
        currentFlow = 0;

        connectedSources.Add(s);
        cleared = false;
        GetTotalFlow();

        foreach (Pipe p in _connectedPipes)
        {
            if (p.cleared)
            {
                foreach (WaterSource source in connectedSources)
                {
                    p.connectedSources.Add(source);
                }
                p.cleared = false;
                p.GetTotalFlow();
                p.RefreshPipeConnection(s);
            }
        }
    }

    private void GetTotalFlow()
    {
        currentFlow = 0;
        foreach (WaterSource source in connectedSources)
        {
            currentFlow += source.sourceFlowAmount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pipe")
        {
            Pipe addedPipe = collision.GetComponent<Pipe>();
            _connectedPipes.Add(addedPipe);

            if (addedPipe.connectedSources.Count != 0)
            {
                foreach (WaterSource source in addedPipe.connectedSources)
                {
                    if (!connectedSources.Contains(source))
                    {
                        connectedSources.Add(source);
                    }
                }

                cleared = false;
                GetTotalFlow();
            }
        }
        else if (collision.tag == "Source")
        {
            WaterSource addedSource = collision.GetComponentInParent<WaterSource>();
            if (!connectedSources.Contains(addedSource))
            {
                connectedSources.Add(addedSource);
                addedSource._connectedPipes.Add(this);
                GetTotalFlow();
                sourceDirectlyConnected = true;
            }
        }

        else if (collision.tag == "Machine")
        {
            Machine addedMachine = collision.GetComponentInParent<Machine>();
            if (!connectedMachines.Contains(addedMachine))
            {
                connectedMachines.Add(addedMachine);
                addedMachine._connectedPipes.Add(this);
                machineDirectlyConnected = true;
                addedMachine.RefreshFlow();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Pipe")
        {
            _connectedPipes.Remove(collision.GetComponent<Pipe>());
        }
    }

    private void ClearNeighbourPipes()
    {
        foreach (Pipe p in _connectedPipes)
        {
            if (!p.cleared)
            {
                p.connectedSources.Clear();
                p.currentFlow = 0;
                p.cleared = true;
                p.ClearNeighbourPipes();
            }
        }
    }

    public void DestroyPipe()
    {
        foreach (Pipe p in _connectedPipes)
        {
            p._connectedPipes.Remove(this);
        }

        foreach (WaterSource source in connectedSources)
        {
            if (source._connectedPipes.Contains(this))
            {
                source._connectedPipes.Remove(this);
            }
        }

        foreach (Machine mach in connectedMachines)
        {
            if (mach._connectedPipes.Contains(this))
            {
                mach._connectedPipes.Remove(this);
            }
        }

        ClearNeighbourPipes();

        foreach (WaterSource source in connectedSources)
        {
            source.StartFlow();
        }

        Destroy(gameObject);
    }
}
