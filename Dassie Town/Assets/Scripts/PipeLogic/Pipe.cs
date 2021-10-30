using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public List<Pipe> _connectedPipes = new List<Pipe>();

    public List<WaterSource> connectedSources = new List<WaterSource>();

    public List<Machine> connectedMachines = new List<Machine>();

    public bool sourceDirectlyConnected;
    public bool machineDirectlyConnected;

    public bool signalPassed;

    [SerializeField] GameObject waterFlowVisual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pipe")
        {
            Pipe addedPipe = collision.GetComponent<Pipe>();
            _connectedPipes.Add(addedPipe);
        }
        else if (collision.tag == "Source")
        {
            WaterSource addedSource = collision.GetComponentInParent<WaterSource>();
            if (!connectedSources.Contains(addedSource))
            {
                connectedSources.Add(addedSource);
                addedSource._connectedPipes.Add(this);
                sourceDirectlyConnected = true;
            }
        }

        else if (collision.tag == "Machine")
        {
            Machine addedMachine = collision.GetComponentInParent<Machine>();
            if (!connectedMachines.Contains(addedMachine))
            {
                connectedMachines.Add(addedMachine);
                addedMachine.connectedPipes.Add(this);
                machineDirectlyConnected = true;
                //addedMachine.StartSignal();
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

    public void PassSignal(Machine mach)
    {
        signalPassed = true;

        StartCoroutine(SignalWait(mach));
    }

    public void DestroyPipe()
    {
        bool hasPipesConnected = false;

        foreach (Pipe p in _connectedPipes)
        {
            if (p._connectedPipes.Contains(this))
            {
                p._connectedPipes.Remove(this);
                hasPipesConnected = true;
            }
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
            if (mach.connectedPipes.Contains(this))
            {
                mach.connectedPipes.Remove(this);
            }
        }

        if (hasPipesConnected == true)
        {
            //GameManager.instance.RefreshMachines();
        }

        Destroy(gameObject);
    }

    private IEnumerator SignalWait(Machine mach)
    {
        waterFlowVisual.SetActive(true);
        yield return new WaitForSeconds(0.001f);

        foreach (Pipe pipe in _connectedPipes)
        {
            if (!pipe.signalPassed)
            {
                pipe.PassSignal(mach);
            }

        }

        foreach (WaterSource source in connectedSources)
        {
            source.ReceiveSignal(mach);
        }

        yield return new WaitForSeconds(0.01f);

        signalPassed = false;

        waterFlowVisual.SetActive(false);
    }
}
