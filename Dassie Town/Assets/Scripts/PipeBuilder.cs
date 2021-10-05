using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeBuilder : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject pipeParent;
    [SerializeField] GameObject pipePrefab;

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
                    Instantiate(pipePrefab, tilemap.GetCellCenterLocal(tilemap.WorldToCell(mousePos)), Quaternion.identity, pipeParent.transform);
                }
            }
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Pipe")
                {
                    hit.collider.GetComponent<Pipe>().DestroyPipe();
                }
            }
        }
    }
}
