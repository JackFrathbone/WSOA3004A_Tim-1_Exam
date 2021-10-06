using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeBuilder : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject pipeParent;
    [SerializeField] GameObject pipePrefab;

    public int totalPipes;

    private void Start()
    {
        GameManager.instance.pipesLeftText.text = totalPipes.ToString();
    }

    public void AddPipes(int i)
    {
        totalPipes += i;
        GameManager.instance.pipesLeftText.text = totalPipes.ToString();
    }

    public void PlacePipe(RaycastHit2D hit, Vector3 mousePos)
    {
        if(totalPipes != 0)
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Ground")
                {
                    totalPipes--;
                    GameManager.instance.pipesLeftText.text = totalPipes.ToString();
                    Instantiate(pipePrefab, tilemap.GetCellCenterLocal(tilemap.WorldToCell(mousePos)), Quaternion.identity, pipeParent.transform);
                }
            }
        }
    }

    public void RemovePipe(RaycastHit2D hit, Vector3 mousePos)
    {
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Pipe")
            {
                totalPipes++;
                GameManager.instance.pipesLeftText.text = totalPipes.ToString();
                hit.collider.GetComponent<Pipe>().DestroyPipe();
            }
        }
    }
}
