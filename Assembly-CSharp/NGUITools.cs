using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x020000AE RID: 174
public static class NGUITools
{
	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00017F24 File Offset: 0x00016124
	// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00017F50 File Offset: 0x00016150
	public static float soundVolume
	{
		get
		{
			if (!NGUITools.mLoaded)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return NGUITools.mGlobalVolume;
		}
		set
		{
			if (NGUITools.mGlobalVolume != value)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00017F80 File Offset: 0x00016180
	public static bool fileAccess
	{
		get
		{
			return Application.platform != RuntimePlatform.WindowsWebPlayer && Application.platform != RuntimePlatform.OSXWebPlayer;
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00017F9C File Offset: 0x0001619C
	public static AudioSource PlaySound(AudioClip clip)
	{
		return NGUITools.PlaySound(clip, 1f, 1f);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00017FB0 File Offset: 0x000161B0
	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return NGUITools.PlaySound(clip, volume, 1f);
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00017FC0 File Offset: 0x000161C0
	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		volume *= NGUITools.soundVolume;
		if (clip != null && volume > 0.01f)
		{
			if (NGUITools.mListener == null)
			{
				NGUITools.mListener = (UnityEngine.Object.FindObjectOfType(typeof(AudioListener)) as AudioListener);
				if (NGUITools.mListener == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = (UnityEngine.Object.FindObjectOfType(typeof(Camera)) as Camera);
					}
					if (camera != null)
					{
						NGUITools.mListener = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (NGUITools.mListener != null && NGUITools.mListener.enabled && NGUITools.GetActive(NGUITools.mListener.gameObject))
			{
				AudioSource audioSource = NGUITools.mListener.audio;
				if (audioSource == null)
				{
					audioSource = NGUITools.mListener.gameObject.AddComponent<AudioSource>();
				}
				audioSource.pitch = pitch;
				audioSource.PlayOneShot(clip, volume);
				return audioSource;
			}
		}
		return null;
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x000180D8 File Offset: 0x000162D8
	public static WWW OpenURL(string url)
	{
		WWW result = null;
		try
		{
			result = new WWW(url);
		}
		catch (Exception ex)
		{
			global::Debug.LogError(ex.Message);
		}
		return result;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00018124 File Offset: 0x00016324
	public static WWW OpenURL(string url, WWWForm form)
	{
		if (form == null)
		{
			return NGUITools.OpenURL(url);
		}
		WWW result = null;
		try
		{
			result = new WWW(url, form);
		}
		catch (Exception ex)
		{
			global::Debug.LogError((ex == null) ? "<null>" : ex.Message);
		}
		return result;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0001818C File Offset: 0x0001638C
	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return UnityEngine.Random.Range(min, max + 1);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x000181A0 File Offset: 0x000163A0
	public static string GetHierarchy(GameObject obj)
	{
		string text = obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			text = obj.name + "\\" + text;
		}
		return text;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x000181F4 File Offset: 0x000163F4
	public static Color ParseColor(string text, int offset)
	{
		int num = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00018278 File Offset: 0x00016478
	public static string EncodeColor(Color c)
	{
		int num = 16777215 & NGUIMath.ColorToInt(c) >> 8;
		return NGUIMath.DecimalToHex(num);
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0001829C File Offset: 0x0001649C
	public static int ParseSymbol(string text, int index, List<Color> colors, bool premultiply)
	{
		int length = text.Length;
		if (index + 2 < length)
		{
			if (text[index + 1] == '-')
			{
				if (text[index + 2] == ']')
				{
					if (colors != null && colors.Count > 1)
					{
						colors.RemoveAt(colors.Count - 1);
					}
					return 3;
				}
			}
			else if (index + 7 < length && text[index + 7] == ']')
			{
				if (colors != null)
				{
					Color color = NGUITools.ParseColor(text, index + 1);
					if (NGUITools.EncodeColor(color) != text.Substring(index + 1, 6).ToUpper())
					{
						return 0;
					}
					color.a = colors[colors.Count - 1].a;
					if (premultiply && color.a != 1f)
					{
						color = Color.Lerp(NGUITools.mInvisible, color, color.a);
					}
					colors.Add(color);
				}
				return 8;
			}
		}
		return 0;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0001839C File Offset: 0x0001659C
	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				char c = text[i];
				if (c == '[')
				{
					int num = NGUITools.ParseSymbol(text, i, null, false);
					if (num > 0)
					{
						text = text.Remove(i, num);
						length = text.Length;
						continue;
					}
				}
				i++;
			}
		}
		return text;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00018400 File Offset: 0x00016600
	public static List<T> FindAll<T>() where T : Component
	{
		T[] array = Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
		List<T> list = new List<T>();
		foreach (T item in array)
		{
			if (item.gameObject.hideFlags == HideFlags.None)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0001846C File Offset: 0x0001666C
	public static T[] FindActive<T>() where T : Component
	{
		return UnityEngine.Object.FindObjectsOfType(typeof(T)) as T[];
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00018484 File Offset: 0x00016684
	public static Camera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		Camera[] array = NGUITools.FindActive<Camera>();
		int i = 0;
		int num2 = array.Length;
		while (i < num2)
		{
			Camera camera = array[i];
			if ((camera.cullingMask & num) != 0)
			{
				return camera;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x000184CC File Offset: 0x000166CC
	public static BoxCollider AddWidgetCollider(GameObject go)
	{
		return NGUITools.AddWidgetCollider(go, false);
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x000184D8 File Offset: 0x000166D8
	public static BoxCollider AddWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			Collider component = go.GetComponent<Collider>();
			BoxCollider boxCollider = component as BoxCollider;
			if (boxCollider == null)
			{
				if (component != null)
				{
					if (Application.isPlaying)
					{
						UnityEngine.Object.Destroy(component);
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(component);
					}
				}
				boxCollider = go.AddComponent<BoxCollider>();
				boxCollider.isTrigger = true;
			}
			NGUITools.UpdateWidgetCollider(boxCollider, considerInactive);
			return boxCollider;
		}
		return null;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0001854C File Offset: 0x0001674C
	public static void UpdateWidgetCollider(GameObject go)
	{
		NGUITools.UpdateWidgetCollider(go, false);
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00018558 File Offset: 0x00016758
	public static void UpdateWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			NGUITools.UpdateWidgetCollider(go.GetComponent<BoxCollider>(), considerInactive);
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00018574 File Offset: 0x00016774
	public static void UpdateWidgetCollider(BoxCollider bc)
	{
		NGUITools.UpdateWidgetCollider(bc, false);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00018580 File Offset: 0x00016780
	public static void UpdateWidgetCollider(BoxCollider box, bool considerInactive)
	{
		if (box != null)
		{
			GameObject gameObject = box.gameObject;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
			box.center = bounds.center;
			box.size = new Vector3(bounds.size.x, bounds.size.y, 0f);
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x000185E8 File Offset: 0x000167E8
	public static string GetName<T>() where T : Component
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0001863C File Offset: 0x0001683C
	public static GameObject AddChild(GameObject parent)
	{
		GameObject gameObject = new GameObject();
		if (parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0001869C File Offset: 0x0001689C
	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
		if (gameObject != null && parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00018710 File Offset: 0x00016910
	public static int CalculateRaycastDepth(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		if (component != null)
		{
			return component.raycastDepth;
		}
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		if (componentsInChildren.Length == 0)
		{
			return 0;
		}
		int num = componentsInChildren[0].raycastDepth;
		int i = 1;
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			num = Mathf.Min(num, componentsInChildren[i].raycastDepth);
			i++;
		}
		return num;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0001877C File Offset: 0x0001697C
	public static int CalculateNextDepth(GameObject go)
	{
		int num = -1;
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		int i = 0;
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			num = Mathf.Max(num, componentsInChildren[i].depth);
			i++;
		}
		return num + 1;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x000187BC File Offset: 0x000169BC
	public static int CalculateNextDepth(GameObject go, bool ignoreChildrenWithColliders)
	{
		if (ignoreChildrenWithColliders)
		{
			int num = -1;
			UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
			int i = 0;
			int num2 = componentsInChildren.Length;
			while (i < num2)
			{
				UIWidget uiwidget = componentsInChildren[i];
				if (!(uiwidget.cachedGameObject != go) || !(uiwidget.collider != null))
				{
					num = Mathf.Max(num, uiwidget.depth);
				}
				i++;
			}
			return num + 1;
		}
		return NGUITools.CalculateNextDepth(go);
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00018834 File Offset: 0x00016A34
	public static int AdjustDepth(GameObject go, int adjustment)
	{
		if (!(go != null))
		{
			return 0;
		}
		UIPanel component = go.GetComponent<UIPanel>();
		if (component != null)
		{
			component.depth += adjustment;
			return 1;
		}
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>(true);
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			UIWidget uiwidget = componentsInChildren[i];
			uiwidget.depth += adjustment;
			i++;
		}
		return 2;
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x000188A8 File Offset: 0x00016AA8
	public static void BringForward(GameObject go)
	{
		int num = NGUITools.AdjustDepth(go, 1000);
		if (num == 1)
		{
			NGUITools.NormalizePanelDepths();
		}
		else if (num == 2)
		{
			NGUITools.NormalizeWidgetDepths();
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x000188E0 File Offset: 0x00016AE0
	public static void PushBack(GameObject go)
	{
		int num = NGUITools.AdjustDepth(go, -1000);
		if (num == 1)
		{
			NGUITools.NormalizePanelDepths();
		}
		else if (num == 2)
		{
			NGUITools.NormalizeWidgetDepths();
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00018918 File Offset: 0x00016B18
	public static void NormalizeDepths()
	{
		NGUITools.NormalizeWidgetDepths();
		NGUITools.NormalizePanelDepths();
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00018924 File Offset: 0x00016B24
	public static void NormalizeWidgetDepths()
	{
		List<UIWidget> list = NGUITools.FindAll<UIWidget>();
		if (list.Count > 0)
		{
			list.Sort(new Comparison<UIWidget>(UIWidget.CompareFunc));
			int num = 0;
			int depth = list[0].depth;
			for (int i = 0; i < list.Count; i++)
			{
				UIWidget uiwidget = list[i];
				if (uiwidget.depth == depth)
				{
					uiwidget.depth = num;
				}
				else
				{
					depth = uiwidget.depth;
					num = (uiwidget.depth = num + 1);
				}
			}
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x000189B4 File Offset: 0x00016BB4
	public static void NormalizePanelDepths()
	{
		List<UIPanel> list = NGUITools.FindAll<UIPanel>();
		if (list.Count > 0)
		{
			list.Sort(new Comparison<UIPanel>(UIPanel.CompareFunc));
			int num = 0;
			int depth = list[0].depth;
			for (int i = 0; i < list.Count; i++)
			{
				UIPanel uipanel = list[i];
				if (uipanel.depth == depth)
				{
					uipanel.depth = num;
				}
				else
				{
					depth = uipanel.depth;
					num = (uipanel.depth = num + 1);
				}
			}
		}
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00018A44 File Offset: 0x00016C44
	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject gameObject = NGUITools.AddChild(parent);
		gameObject.name = NGUITools.GetName<T>();
		return gameObject.AddComponent<T>();
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00018A6C File Offset: 0x00016C6C
	public static T AddWidget<T>(GameObject go) where T : UIWidget
	{
		int depth = NGUITools.CalculateNextDepth(go);
		T result = NGUITools.AddChild<T>(go);
		result.width = 100;
		result.height = 100;
		result.depth = depth;
		result.gameObject.layer = go.layer;
		return result;
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00018ACC File Offset: 0x00016CCC
	public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
	{
		UISpriteData uispriteData = (!(atlas != null)) ? null : atlas.GetSprite(spriteName);
		UISprite uisprite = NGUITools.AddWidget<UISprite>(go);
		uisprite.type = ((uispriteData != null && uispriteData.hasBorder) ? UISprite.Type.Sliced : UISprite.Type.Simple);
		uisprite.atlas = atlas;
		uisprite.spriteName = spriteName;
		return uisprite;
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00018B28 File Offset: 0x00016D28
	public static GameObject GetRoot(GameObject go)
	{
		Transform transform = go.transform;
		for (;;)
		{
			Transform parent = transform.parent;
			if (parent == null)
			{
				break;
			}
			transform = parent;
		}
		return transform.gameObject;
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00018B68 File Offset: 0x00016D68
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return (T)((object)null);
		}
		object obj = go.GetComponent<T>();
		if (obj == null)
		{
			Transform parent = go.transform.parent;
			while (parent != null && obj == null)
			{
				obj = parent.gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return (T)((object)obj);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00018BDC File Offset: 0x00016DDC
	public static void Destroy(UnityEngine.Object obj)
	{
		if (obj != null)
		{
			if (Application.isPlaying)
			{
				if (obj is GameObject)
				{
					GameObject gameObject = obj as GameObject;
					gameObject.transform.parent = null;
				}
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00018C30 File Offset: 0x00016E30
	public static void DestroyImmediate(UnityEngine.Object obj)
	{
		if (obj != null)
		{
			if (Application.isEditor)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			else
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00018C5C File Offset: 0x00016E5C
	public static void Broadcast(string funcName)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00018CA0 File Offset: 0x00016EA0
	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00018CE4 File Offset: 0x00016EE4
	public static bool IsChild(Transform parent, Transform child)
	{
		if (parent == null || child == null)
		{
			return false;
		}
		while (child != null)
		{
			if (child == parent)
			{
				return true;
			}
			child = child.parent;
		}
		return false;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00018D34 File Offset: 0x00016F34
	private static void Activate(Transform t)
	{
		NGUITools.SetActiveSelf(t.gameObject, true);
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			Transform child = t.GetChild(i);
			if (child.gameObject.activeSelf)
			{
				return;
			}
			i++;
		}
		int j = 0;
		int childCount2 = t.childCount;
		while (j < childCount2)
		{
			Transform child2 = t.GetChild(j);
			NGUITools.Activate(child2);
			j++;
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00018DAC File Offset: 0x00016FAC
	private static void Deactivate(Transform t)
	{
		NGUITools.SetActiveSelf(t.gameObject, false);
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00018DBC File Offset: 0x00016FBC
	public static void SetActive(GameObject go, bool state)
	{
		if (state)
		{
			NGUITools.Activate(go.transform);
		}
		else
		{
			NGUITools.Deactivate(go.transform);
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00018DE0 File Offset: 0x00016FE0
	public static void SetActiveChildren(GameObject go, bool state)
	{
		Transform transform = go.transform;
		if (state)
		{
			int i = 0;
			int childCount = transform.childCount;
			while (i < childCount)
			{
				Transform child = transform.GetChild(i);
				NGUITools.Activate(child);
				i++;
			}
		}
		else
		{
			int j = 0;
			int childCount2 = transform.childCount;
			while (j < childCount2)
			{
				Transform child2 = transform.GetChild(j);
				NGUITools.Deactivate(child2);
				j++;
			}
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00018E58 File Offset: 0x00017058
	public static bool GetActive(GameObject go)
	{
		return go && go.activeInHierarchy;
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00018E70 File Offset: 0x00017070
	public static void SetActiveSelf(GameObject go, bool state)
	{
		go.SetActive(state);
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00018E7C File Offset: 0x0001707C
	public static void SetLayer(GameObject go, int layer)
	{
		go.layer = layer;
		Transform transform = go.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			Transform child = transform.GetChild(i);
			NGUITools.SetLayer(child.gameObject, layer);
			i++;
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00018EC4 File Offset: 0x000170C4
	public static Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00018F0C File Offset: 0x0001710C
	public static void MakePixelPerfect(Transform t)
	{
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.MakePixelPerfect();
		}
		if (t.GetComponent<UIAnchor>() == null && t.GetComponent<UIRoot>() == null)
		{
			t.localPosition = NGUITools.Round(t.localPosition);
			t.localScale = NGUITools.Round(t.localScale);
		}
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			NGUITools.MakePixelPerfect(t.GetChild(i));
			i++;
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00018F9C File Offset: 0x0001719C
	public static bool Save(string fileName, byte[] bytes)
	{
		if (!NGUITools.fileAccess)
		{
			return false;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (bytes == null)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			return true;
		}
		FileStream fileStream = null;
		try
		{
			fileStream = File.Create(path);
		}
		catch (Exception ex)
		{
			NGUIDebug.Log(ex.Message);
			return false;
		}
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Close();
		return true;
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00019038 File Offset: 0x00017238
	public static byte[] Load(string fileName)
	{
		if (!NGUITools.fileAccess)
		{
			return null;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}
		return null;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00019078 File Offset: 0x00017278
	public static Color ApplyPMA(Color c)
	{
		if (c.a != 1f)
		{
			c.r *= c.a;
			c.g *= c.a;
			c.b *= c.a;
		}
		return c;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x000190D8 File Offset: 0x000172D8
	public static void MarkParentAsChanged(GameObject go)
	{
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			componentsInChildren[i].ParentHasChanged();
			i++;
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0001910C File Offset: 0x0001730C
	private static PropertyInfo GetSystemCopyBufferProperty()
	{
		if (NGUITools.mSystemCopyBuffer == null)
		{
			Type typeFromHandle = typeof(GUIUtility);
			NGUITools.mSystemCopyBuffer = typeFromHandle.GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
		}
		return NGUITools.mSystemCopyBuffer;
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060004EE RID: 1262 RVA: 0x00019148 File Offset: 0x00017348
	// (set) Token: 0x060004EF RID: 1263 RVA: 0x00019174 File Offset: 0x00017374
	public static string clipboard
	{
		get
		{
			PropertyInfo systemCopyBufferProperty = NGUITools.GetSystemCopyBufferProperty();
			return (systemCopyBufferProperty == null) ? null : ((string)systemCopyBufferProperty.GetValue(null, null));
		}
		set
		{
			PropertyInfo systemCopyBufferProperty = NGUITools.GetSystemCopyBufferProperty();
			if (systemCopyBufferProperty != null)
			{
				systemCopyBufferProperty.SetValue(null, value, null);
			}
		}
	}

	// Token: 0x04000355 RID: 853
	private static AudioListener mListener;

	// Token: 0x04000356 RID: 854
	private static bool mLoaded = false;

	// Token: 0x04000357 RID: 855
	private static float mGlobalVolume = 1f;

	// Token: 0x04000358 RID: 856
	private static Color mInvisible = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000359 RID: 857
	private static PropertyInfo mSystemCopyBuffer = null;
}
