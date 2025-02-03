using UnityEngine;

public class RopeObject : PuzzleObjects, IInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D ropeChildRigidbody;
    public void Interact(Bubble bubble)
    {
        if (bubble is FireBubble)
        {
            Activate();
        }
    }

    public override void Activate()
    {
        if (ropeChildRigidbody != null)
        {
            Debug.Log("rope");
            animator.SetTrigger("isTouch");
        }
    }

    private void BreakRope()
    {
        ropeChildRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
