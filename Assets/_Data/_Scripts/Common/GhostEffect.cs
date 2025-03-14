using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [SerializeField] private GameObject ghostObject;
    public float ghostDelay;
    private float ghostDelaySeconds;

    public bool makeGhost = false;

    private void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }
    private void Update()
    {
        MakeGhost();
    }

    private void MakeGhost()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds < 0)
            {
                GameObject ghost = Instantiate(ghostObject, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                ghost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghost.transform.localScale = transform.localScale;
                Destroy(ghost, 0.5f);

                ghostDelaySeconds = ghostDelay;
                return;

            }
            ghostDelaySeconds -= Time.deltaTime;
        }
    }
}
