using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PipeBuilder _pipeBuilder;

    public bool disableInput;

    [SerializeField] float clickDistanceLimit;

    private PlayerController playerController;

    private void Start()
    {
        _pipeBuilder = GetComponent<PipeBuilder>();
        playerController = GetComponentInChildren<PlayerController>();
    }

    private void Update()
    {
        if (!disableInput)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f;

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                float mouseDistance = Vector3.Distance(mousePos, playerController.transform.position);

                if (hit.collider != null && mouseDistance <= clickDistanceLimit)
                {
                    if (hit.collider.tag == "Ground")
                    {
                        _pipeBuilder.PlacePipe(hit, mousePos);
                    }

                    else if (hit.collider.tag == "NPC")
                    {
                        hit.collider.GetComponent<CharacteDialogueController>().ActivateDialogue();
                    }
                }
            }

            if (Input.GetMouseButton(1))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f;

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                float mouseDistance = Vector3.Distance(mousePos, playerController.transform.position);

                if (hit.collider != null && mouseDistance <= clickDistanceLimit)
                {
                    if (hit.collider.tag == "Pipe")
                    {
                        _pipeBuilder.RemovePipe(hit, mousePos);
                    }
                }
            }
        }
    }
}
