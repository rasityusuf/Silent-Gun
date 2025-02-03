using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentObject : MonoBehaviour
{

	[SerializeField] private GameObject boxGO;
	//This is for box
public void DestroyItself()
	{
		boxGO.transform.parent = GameObject.FindObjectOfType<LevelPrefabScript>().gameObject.transform;
		boxGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

		Destroy(transform.parent.gameObject, 0.1f);
	}
}
