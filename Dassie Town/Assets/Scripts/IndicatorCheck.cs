using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorCheck : MonoBehaviour
{
    public TextMeshPro text;

    public Color failColor;
    public Color passColor;

    [SerializeField] List<InputMain> inputsToCheckPass = new List<InputMain>();

    private float time = 0.0f;
    private float interpolationPeriod = 3f;

    private void Start()
    {
        CheckConditions();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;

            CheckConditions();
        }
    }

    private void SetFail()
    {
        text.text = "!";
        text.color = failColor;
    }

    private void SetPass()
    {
        text.text = ":)";
        text.color = passColor;
    }

    private void CheckConditions()
    {
        bool metConditions = true;

        foreach (InputMain input in inputsToCheckPass)
        {
            if (!input.conditionMet)
            {
                metConditions = false;
            }
        }

        if (metConditions == true)
        {
            SetPass();
        }
        else
        {
            SetFail();
        }
    }
}
