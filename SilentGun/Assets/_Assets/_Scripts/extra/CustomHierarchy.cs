﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;

///// <summary> Sets a background color for this game object in the Unity Hierarchy window </summary>
//[UnityEditor.InitializeOnLoad]
//#endif
//public class CustomHierarchy
//{

//	private static Vector2 offset = new Vector2(20, 1);

//	static CustomHierarchy()
//	{
//		EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
//	}

//	private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
//	{

//		var obj = EditorUtility.InstanceIDToObject(instanceID);
//		if (obj != null)
//		{
//			Color backgroundColor = Color.white;
//			Color textColor = Color.white;
//			Texture2D texture = null;

//			//ADD CONDITIONS HERE TO COLOR MORE OBJECTS
//			// YOU CAN COLOR FOLLOWING NAME, TAG...
//			if (obj.name == "Player")
//			{
//				backgroundColor = new Color(0.2f, 0.6f, 0.1f);
//				textColor = new Color(0.9f, 0.9f, 0.9f);
//			}
//			else if ((obj as GameObject).GetComponent<Canvas>())
//			{
//				backgroundColor = new Color(0.9f, 0.57f, 0.32f);
//				textColor = new Color(0.9f, 0.9f, 0.9f);
//			}
//			else if ((obj as GameObject).GetComponent<Camera>())
//			{
//				backgroundColor = new Color(0.3f, 0.4f, 1f);
//				textColor = new Color(0.9f, 0.9f, 0.9f);
//			}
//			else if (obj.name == "SceneManager")
//			{
//				backgroundColor = new Color(0.9f, 0.9f, 0.9f);
//				textColor = new Color(0f, 0f, 0f);
//			}
//			else if (obj.name == "Env")
//			{
//				backgroundColor = new Color(0.9f, 0.9f, 0.9f);
//				textColor = new Color(0f, 0f, 0f);
//			}
//			else if (obj.name == "Levels")
//			{
//				backgroundColor = new Color(0.9f, 0.9f, 0.9f);
//				textColor = new Color(0f, 0f, 0f);
//			}

//			if (backgroundColor != Color.white)
//			{
//				Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
//				Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

//				EditorGUI.DrawRect(bgRect, backgroundColor);
//				EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
//				{
//					normal = new GUIStyleState() { textColor = textColor },
//					fontStyle = FontStyle.Bold
//				}
//				);

//				if (texture != null)
//					EditorGUI.DrawPreviewTexture(new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height)), texture);
//			}
//		}
//	}
//}