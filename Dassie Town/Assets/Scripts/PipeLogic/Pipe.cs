using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public List<Pipe> _connectedPipes = new List<Pipe>();

    public List<OutputMain> connectedOutputs = new List<OutputMain>();

    public List<InputMain> connectedInputs = new List<InputMain>();

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

        else if (collision.tag == "Output")
        {
            OutputMain addedOutput = collision.GetComponentInParent<OutputOutlet>().masterOutput;

            if (!connectedOutputs.Contains(addedOutput))
            {
                connectedOutputs.Add(addedOutput);
                addedOutput._connectedPipes.Add(this);
                sourceDirectlyConnected = true;
            }
        }

        else if (collision.tag == "Input")
        {
            //Finds the connected input through the inlet
            InputMain addedInput = collision.GetComponentInParent<InputInlet>().masterInput;
            addedInput.AddInputToPriorityList();

            if (!connectedInputs.Contains(addedInput))
            {
                connectedInputs.Add(addedInput);
                addedInput.connectedPipes.Add(this);
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

    public void PassSignal(InputMain input)
    {
        signalPassed = true;

        StartCoroutine(SignalWait(input));
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

        foreach (OutputMain output in connectedOutputs)
        {
            if (output._connectedPipes.Contains(this))
            {
                output._connectedPipes.Remove(this);
            }
        }

        foreach (InputMain input in connectedInputs)
        {
            if (input.connectedPipes.Contains(this))
            {
                input.connectedPipes.Remove(this);
            }
        }

        if (hasPipesConnected == true)
        {
            //GameManager.instance.RefreshMachines();
        }

        Destroy(gameObject);
    }

    private IEnumerator SignalWait(InputMain input)
    {
        waterFlowVisual.SetActive(true);
        yield return new WaitForSeconds(0.001f);

        foreach (Pipe pipe in _connectedPipes)
        {
            if (!pipe.signalPassed)
            {
                pipe.PassSignal(input);
            }

        }

        foreach (OutputMain output in connectedOutputs)
        {
            output.ReceiveSignal(input);
        }

        yield return new WaitForSeconds(0.01f);

        signalPassed = false;

        waterFlowVisual.SetActive(false);
    }
}
