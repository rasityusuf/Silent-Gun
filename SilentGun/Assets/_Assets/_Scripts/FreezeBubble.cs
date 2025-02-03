using UnityEngine;

public class FreezeBubble : Bubble
{
    public override void BubbleBehaviour(float angle, Vector2 gunDirection)
    {
        base.BubbleBehaviour(angle, gunDirection);
        rb.gravityScale = this.gravityScale;
    }
}
