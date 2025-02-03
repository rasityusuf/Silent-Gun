using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Start()
    {
        GameManager.instance.blackBoard.SetValue("PlayerController", this);
    }
}
