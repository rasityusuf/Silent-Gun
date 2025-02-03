using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEffectsScript : MonoBehaviour
{
public void DestroyBubbleEffect()
	{
		Destroy(this.gameObject, 0.1f);
	}
}
