using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B9 RID: 441
public class GimmickCounter : MonoBehaviour
{
	// Token: 0x06000C9E RID: 3230 RVA: 0x00047C18 File Offset: 0x00045E18
	private void Start()
	{
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00047C1C File Offset: 0x00045E1C
	private void Update()
	{
		if (this.m_waitTimer == 5)
		{
			this.InitCoroutine();
		}
		this.m_waitTimer++;
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00047C4C File Offset: 0x00045E4C
	private void InitCoroutine()
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		GameObject gameObject = base.gameObject;
		int childCount = gameObject.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = gameObject.transform.GetChild(i);
			if (!(child == null))
			{
				GameObject gameObject2 = child.gameObject;
				if (!(gameObject2 == null))
				{
					string name = gameObject2.name;
					int num3;
					if (name == "MultiSetLine" || name == "MultiSetSector" || name == "MultiSetCircle" || name == "MultiSetParaloopCircle")
					{
						int childCount2 = child.childCount;
						for (int j = 0; j < childCount2; j++)
						{
							Transform child2 = child.GetChild(j);
							if (!(child2 == null))
							{
								GameObject gameObject3 = child2.gameObject;
								if (!(gameObject3 == null))
								{
									string name2 = gameObject3.name;
									int num;
									if (!dictionary.TryGetValue(name2, out num))
									{
										dictionary.Add(name2, 1);
									}
									else
									{
										Dictionary<string, int> dictionary3;
										Dictionary<string, int> dictionary2 = dictionary3 = dictionary;
										string text;
										string key = text = name2;
										int num2 = dictionary3[text];
										dictionary2[key] = num2 + 1;
									}
								}
							}
						}
					}
					else if (!dictionary.TryGetValue(name, out num3))
					{
						dictionary.Add(name, 1);
					}
					else
					{
						Dictionary<string, int> dictionary5;
						Dictionary<string, int> dictionary4 = dictionary5 = dictionary;
						string text;
						string key2 = text = name;
						int num2 = dictionary5[text];
						dictionary4[key2] = num2 + 1;
					}
				}
			}
		}
		global::Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
		string text2 = "BlockID = " + gameObject.name + "\n";
		foreach (KeyValuePair<string, int> keyValuePair in dictionary)
		{
			string text = text2;
			text2 = string.Concat(new string[]
			{
				text,
				keyValuePair.Key,
				":",
				keyValuePair.Value.ToString(),
				"\n"
			});
		}
		global::Debug.Log(text2);
		global::Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
	}

	// Token: 0x040009EE RID: 2542
	private int m_waitTimer;
}
