using UnityEngine;

public class DoorObject : PuzzleObjects, IInteractable
{
    private Animator animator;
    private Collider2D doorCol;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

	private void Start()
	{
        doorCol = GetComponent<Collider2D>();
    }

	public void Interact(Bubble bubble)
    {
        if (bubble is FreezeBubble)
        {
            Activate();
        }

    }

    public override void Activate()
    {
        if (animator != null)
        {
            animator.SetTrigger("isTouch");
            doorCol.enabled = false;
            Destroy(this.gameObject, 1f);
        }
    }
}
