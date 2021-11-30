using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeBuilder : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] GameObject pipeParent;
    [SerializeField] GameObject pipePrefab;

    public int totalPipes;

    private AudioSource audioSource;
    [SerializeField] AudioClip placeSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = placeSound;

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

                    if (!audioSource.isPlaying)
                    {
                        audioSource.pitch = 1f;
                        audioSource.Play();
                    }
                
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
                if (!audioSource.isPlaying)
                {
                    audioSource.pitch = 0.5f;
                    audioSource.Play();
                }
            }
        }
    }
}
