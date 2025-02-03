using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfBoxBottomScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("ground") && other.gameObject.TryGetComponent<BoxHolder>(out BoxHolder boxHolder))
		{
			Destroy(other.gameObject, 0.1f);
		}
		if (other.gameObject.CompareTag("Player"))
		{
			//PLayer reset
		}
	}
}
