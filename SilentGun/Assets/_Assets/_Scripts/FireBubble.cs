using UnityEngine;

public class FireBubble : Bubble
{
    int counter = 0;
    public float rotationMultiplier = 5f;
    public override void BubbleBehaviour(float angle, Vector2 gunDirection)
    {
        base.BubbleBehaviour(angle, gunDirection);
        rb.gravityScale = this.gravityScale;
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("ground"))
        {
            counter++;
            if (counter >= 3)
            {
                Destroy(gameObject);
            }
            
            //Buraya particul koyulacak
        }

    }
    


}
