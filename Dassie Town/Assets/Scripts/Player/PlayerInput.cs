using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PipeBuilder _pipeBuilder;


    private void Start()
    {
        _pipeBuilder = GetComponent<PipeBuilder>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Ground")
                {
                    _pipeBuilder.PlacePipe(hit, mousePos);
                }

                else if (hit.collider.tag == "NPC")
                {
                    print("clicked npc");
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Pipe")
                {
                    _pipeBuilder.RemovePipe(hit, mousePos);
                }
            }
        }
    }
}
