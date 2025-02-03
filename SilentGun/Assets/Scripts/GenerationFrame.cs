using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationFrame : MonoBehaviour
{
    [SerializeField] private GameObject waterBubble;
    float timer = 3f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(waterBubble,transform.position, Quaternion.identity);
            timer = 3f;

        }
    }
   
}
