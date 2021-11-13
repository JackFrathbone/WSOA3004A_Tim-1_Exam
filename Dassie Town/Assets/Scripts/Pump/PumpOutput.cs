using UnityEngine;

public class PumpOutput : MonoBehaviour
{
    [SerializeField] OutputMain outputSourceParent;

    public void SetPumpOutput(int currentflow)
    {
        outputSourceParent.SetFlow(currentflow);
    }
}
