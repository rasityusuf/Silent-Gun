using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStartCollider : MonoBehaviour
{
    [SerializeField] private GameObject poolWater;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Bubble>(out Bubble bubble))
        {
            if(bubble.GetType() == typeof(WaterBubble))
            {
                poolWater.SetActive(true);
                Destroy(bubble.gameObject);
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
