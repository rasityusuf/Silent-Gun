using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    private void Awake()
    {
        instance = this;
    }
}
