using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabScript : MonoBehaviour
{
	[HideInInspector] public Transform startPos;
	[HideInInspector] public Transform endPos;

	private void Awake()
	{
        startPos = FindChildWithTag(this.gameObject.transform, "startPoint").transform;
        endPos = FindChildWithTag(this.gameObject.transform, "endPoint").transform;
    }

    GameObject FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
