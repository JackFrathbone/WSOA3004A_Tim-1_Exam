using UnityEngine;

public class PumpOutput : MonoBehaviour
{
    [SerializeField] WaterSource outputSourceParent;

    public void SetPumpOutput(int currentflow)
    {
        outputSourceParent.SetFlow(currentflow);
    }
}
