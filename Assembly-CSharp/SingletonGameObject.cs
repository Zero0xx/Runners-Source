using System;
using UnityEngine;

// Token: 0x02000A59 RID: 2649
public class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour
{
	// Token: 0x1700099B RID: 2459
	// (get) Token: 0x0600475C RID: 18268 RVA: 0x00177898 File Offset: 0x00175A98
	public static T Instance
	{
		get
		{
			if (SingletonGameObject<T>.s_instance == null && !SingletonGameObject<T>.s_isDestroy)
			{
				SingletonGameObject<T>.s_instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
				if (SingletonGameObject<T>.s_instance != null)
				{
					string text = typeof(T).ToString();
					if (text.IndexOf("Debug") != -1 || text.IndexOf("debug") != -1)
					{
						UnityEngine.Object.Destroy(SingletonGameObject<T>.s_instance.gameObject);
						SingletonGameObject<T>.s_instance = (T)((object)null);
						SingletonGameObject<T>.s_isDestroy = true;
						global::Debug.Log("debug SingletonGameObject auto delete !!! :" + text);
					}
				}
			}
			return SingletonGameObject<T>.s_instance;
		}
	}

	// Token: 0x0600475D RID: 18269 RVA: 0x00177960 File Offset: 0x00175B60
	private void Awake()
	{
		if (this != SingletonGameObject<T>.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SingletonGameObject<T>.s_instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
		if (!this.m_isOnLoadDestroy)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	// Token: 0x0600475E RID: 18270 RVA: 0x001779C0 File Offset: 0x00175BC0
	public static void Remove()
	{
		if (SingletonGameObject<T>.s_instance != null)
		{
			UnityEngine.Object.Destroy(SingletonGameObject<T>.s_instance.gameObject);
			SingletonGameObject<T>.s_instance = (T)((object)null);
			SingletonGameObject<T>.s_isDestroy = true;
		}
	}

	// Token: 0x04003B6E RID: 15214
	[Header("シーン切替時の削除設定(true:削除)")]
	[SerializeField]
	private bool m_isOnLoadDestroy;

	// Token: 0x04003B6F RID: 15215
	private static bool s_isDestroy;

	// Token: 0x04003B70 RID: 15216
	private static T s_instance;
}
