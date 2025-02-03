using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        if(collision.TryGetComponent<PoolObject>(out PoolObject poolObject))
        {
            Debug.Log("Triggered");
            poolObject.borderReached = true;
        }
    }
}
