using UnityEngine;

public class PumpInput : MonoBehaviour
{
    //Sets how the maximum amount that is outputted by the pump output
    public int pumpLimit;

    [SerializeField] PumpOutput pumpOutput;

    //Sets the flow of the output the same as the connect input
    public void SetPumpFlow(int currentFlow)
    {
        if(currentFlow >= pumpLimit)
        {
            pumpOutput.SetPumpOutput(pumpLimit);
        }
        else
        {
            pumpOutput.SetPumpOutput(currentFlow);
        }
    }
}
