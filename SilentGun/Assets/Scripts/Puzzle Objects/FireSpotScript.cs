using UnityEngine;
public class  FireSpotScript : PuzzleObjects, IInteractable
{
    private Animator anim;
    private Collider2D fireCol;
    private SpriteRenderer fireRend;

   [SerializeField] private bool isFlamableAgain;

	private void Start()
	{
        anim = this.gameObject.GetComponent<Animator>();
        fireCol = this.gameObject.GetComponent<Collider2D>();
        fireRend = this.gameObject.GetComponent<SpriteRenderer>();
    }

	public void Interact(Bubble bubble)
    {
        if (bubble is FireBubble)
        {
            if (!isFlamableAgain) return;

            fireCol.enabled = true;
            fireRend.enabled = true;

            anim.SetBool("isLit", true);
        }

        if (bubble is WaterBubble)
        {
            anim.SetBool("isLit", false);
        }
    }

    public void DeactivateFire()
	{
        fireCol.enabled = false;
        fireRend.enabled = false;
    }
}