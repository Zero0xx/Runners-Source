using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033B RID: 827
public class GameObjectUtil
{
	// Token: 0x0600188C RID: 6284 RVA: 0x0008E130 File Offset: 0x0008C330
	public static List<T> FindChildGameObjectsComponents<T>(GameObject parent, string name) where T : Component
	{
		List<T> list = new List<T>();
		if (parent != null)
		{
			IEnumerable<GameObject> enumerable = GameObjectUtil.FindChildGameObjectsEnumerable(parent, name);
			foreach (GameObject gameObject in enumerable)
			{
				list.Add(gameObject.GetComponent<T>());
			}
		}
		return list;
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x0008E1B0 File Offset: 0x0008C3B0
	public static List<GameObject> FindChildGameObjects(GameObject parent, string name)
	{
		List<GameObject> list = new List<GameObject>();
		if (parent != null)
		{
			IEnumerable<GameObject> enumerable = GameObjectUtil.FindChildGameObjectsEnumerable(parent, name);
			foreach (GameObject item in enumerable)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x0008E22C File Offset: 0x0008C42C
	private static IEnumerable<GameObject> FindChildGameObjectsEnumerable(GameObject parent, string name)
	{
		for (int i = 0; i < parent.transform.childCount; i++)
		{
			GameObject child = parent.transform.GetChild(i).gameObject;
			if (child.name == name)
			{
				yield return child;
			}
			IEnumerable<GameObject> gos = GameObjectUtil.FindChildGameObjectsEnumerable(child, name);
			foreach (GameObject go in gos)
			{
				yield return go;
			}
		}
		yield break;
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x0008E264 File Offset: 0x0008C464
	public static GameObject FindChildGameObject(GameObject parent, string name)
	{
		Transform transform = parent.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject gameObject = transform.GetChild(i).gameObject;
			if (gameObject.name == name)
			{
				return gameObject;
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, name);
			if (gameObject2 != null)
			{
				return gameObject2;
			}
		}
		return null;
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x0008E2C8 File Offset: 0x0008C4C8
	public static T FindChildGameObjectComponent<T>(GameObject parent, string name) where T : Component
	{
		if (parent == null)
		{
			return (T)((object)null);
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, name);
		if (gameObject == null)
		{
			return (T)((object)null);
		}
		return gameObject.GetComponent<T>();
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x0008E30C File Offset: 0x0008C50C
	public static T FindGameObjectComponent<T>(string name) where T : Component
	{
		GameObject gameObject = GameObject.Find(name);
		if (gameObject == null)
		{
			return (T)((object)null);
		}
		return gameObject.GetComponent<T>();
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x0008E33C File Offset: 0x0008C53C
	public static T FindGameObjectComponentWithTag<T>(string tagName, string name) where T : Component
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name == name)
			{
				return gameObject.GetComponent<T>();
			}
		}
		return (T)((object)null);
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x0008E388 File Offset: 0x0008C588
	public static GameObject FindGameObjectWithTag(string tagName, string name)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name == name)
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x0008E3CC File Offset: 0x0008C5CC
	public static GameObject FindParentGameObject(GameObject gameObject, string name)
	{
		while (gameObject != null)
		{
			GameObject gameObject2 = null;
			Transform parent = gameObject.transform.parent;
			if (parent != null)
			{
				gameObject2 = parent.gameObject;
				if (gameObject2 != null && gameObject2.name == name)
				{
					return gameObject2;
				}
			}
			gameObject = gameObject2;
		}
		return null;
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x0008E430 File Offset: 0x0008C630
	public static GameObject SendMessageFindGameObject(string objectName, string methodName, object value, SendMessageOptions options)
	{
		GameObject gameObject = GameObject.Find(objectName);
		if (gameObject != null)
		{
			gameObject.SendMessage(methodName, value, options);
		}
		return gameObject;
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x0008E45C File Offset: 0x0008C65C
	public static GameObject SendMessageFindGameObjectWithTag(string tagName, string objectName, string methodName, object value, SendMessageOptions options)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name == objectName)
			{
				gameObject.SendMessage(methodName, value, options);
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x0008E4A8 File Offset: 0x0008C6A8
	public static void SendMessageToTagObjects(string tagName, string methodName, object value, SendMessageOptions options)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject gameObject in array)
		{
			gameObject.SendMessage(methodName, value, options);
		}
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x0008E4E0 File Offset: 0x0008C6E0
	public static void SendDelayedMessageFindGameObject(string objectName, string methodName, object value)
	{
		if (DelayedMessageManager.Instance != null)
		{
			DelayedMessageManager.Instance.AddDelayedMessage(objectName, methodName, value);
		}
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x0008E500 File Offset: 0x0008C700
	public static void SendDelayedMessageToGameObject(GameObject gameObject, string methodName, object value)
	{
		if (DelayedMessageManager.Instance != null)
		{
			DelayedMessageManager.Instance.AddDelayedMessage(gameObject, methodName, value);
		}
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x0008E520 File Offset: 0x0008C720
	public static void SendDelayedMessageToTagObjects(string tagName, string methodName, object value)
	{
		if (DelayedMessageManager.Instance != null)
		{
			DelayedMessageManager.Instance.AddDelayedMessageToTag(tagName, methodName, value);
		}
	}
}
