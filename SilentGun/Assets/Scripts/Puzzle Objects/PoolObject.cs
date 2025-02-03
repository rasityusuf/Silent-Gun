using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : PuzzleObjects,IInteractable
{
    [SerializeField] private float increaseAmount;
    public bool borderReached;
    public override void Activate()
    {
    }

	private void Start()
	{
        canRiseWater = true;

    }

	private void Update()
	{
		if ( (c1p.hasTouchedFreeze || c2p.hasTouchedFreeze) && canRiseWater)
		{
            NoCheckWaterRise();
            c1p.hasTouchedFreeze = false;
            c2p.hasTouchedFreeze = false;
        }
    }

    bool canRiseWater;
    private IEnumerator giveDelay()
	{
        canRiseWater = false;
        yield return new WaitForSeconds(0.1f);
        canRiseWater = true;
    }
 
    private void NoCheckWaterRise()
	{
        float prevLocalScaleY = transform.localScale.y;

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + increaseAmount, transform.localScale.z);

        float scaleDifference = transform.localScale.y - prevLocalScaleY;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float boundsHeight = spriteRenderer.bounds.size.y / prevLocalScaleY;
            float positionOffset = (scaleDifference * boundsHeight) / 20.0f;
            transform.position += new Vector3(0, positionOffset, 0);
        }
        else
        {
            Debug.LogWarning("SpriteRenderer not found! Ensure the object has a SpriteRenderer.");
        }
    }

	public void SpecificInteraction(Bubble bubble)
    {
        if (!canRiseWater) return;

        CheckIfObjectIsActive();
        if(bubble is WaterBubble)
        {
            float prevLocalScaleY = transform.localScale.y;

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + increaseAmount, transform.localScale.z);

            float scaleDifference = transform.localScale.y - prevLocalScaleY;

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                float boundsHeight = spriteRenderer.bounds.size.y / prevLocalScaleY;
                float positionOffset = (scaleDifference * boundsHeight) / 20.0f;
                transform.position += new Vector3(0, positionOffset, 0);
            }
            else
            {
                Debug.LogWarning("SpriteRenderer not found! Ensure the object has a SpriteRenderer.");
            }
        }
    }

    public void CheckIfObjectIsActive()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            
        }
    }

    [SerializeField] private Color freezeColor;

    public poolColliderAssist c1p;
    public poolColliderAssist c2p;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bubble>(out Bubble bubble) && !borderReached)
        {
            Debug.Log(borderReached);
            SpecificInteraction(bubble);
            
        }
        if(bubble is FreezeBubble && borderReached)
        {
            GetComponent<Collider2D>().isTrigger = false;
            //GetComponent<SpriteRenderer>().sprite = ;
            GetComponent<SpriteRenderer>().color = freezeColor;
        }

        if (bubble == null) return;

        Destroy(bubble.gameObject);

    }

    public void Interact(Bubble bubble)
    {
        return;
    }
}
