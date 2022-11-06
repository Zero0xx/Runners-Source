using System;
using System.Collections.Generic;
using UnityEngine;

namespace Text
{
	// Token: 0x02000A29 RID: 2601
	public class TextManager : MonoBehaviour
	{
		// Token: 0x060044DF RID: 17631 RVA: 0x00162844 File Offset: 0x00160A44
		public static void SetLanguageName()
		{
			TextManager instance = TextManager.GetInstance();
			instance.m_languageName = TextUtility.GetXmlLanguageDataType();
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x00162864 File Offset: 0x00160A64
		public static void SetSuffixeName()
		{
			TextManager instance = TextManager.GetInstance();
			instance.m_suffixeName = TextUtility.GetSuffixe();
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x00162884 File Offset: 0x00160A84
		private void Awake()
		{
			if (TextManager.m_instance == null)
			{
				TextManager.m_instance = this;
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				Language.InitAppEnvLanguage();
				this.m_languageName = TextUtility.GetXmlLanguageDataType();
				this.m_suffixeName = TextUtility.GetSuffixe();
				this.m_textDictionary = new Dictionary<TextManager.TextType, TextLoadImpl>();
				this.SetupFixationText();
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x001628F0 File Offset: 0x00160AF0
		private void Start()
		{
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x001628F4 File Offset: 0x00160AF4
		private void Update()
		{
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x001628F8 File Offset: 0x00160AF8
		private void OnDestroy()
		{
			if (TextManager.m_instance == this)
			{
				this.m_textDictionary.Clear();
				TextManager.m_instance = null;
			}
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x0016291C File Offset: 0x00160B1C
		private void SetupFixationText()
		{
			TextManager.TextType key = TextManager.TextType.TEXTTYPE_FIXATION_TEXT;
			Dictionary<TextManager.TextType, TextLoadImpl> textDictionary = this.m_textDictionary;
			if (textDictionary.ContainsKey(key))
			{
				return;
			}
			textDictionary.Add(key, new TextLoadImpl());
			string fileName = "TextData/text_fixation_text_" + this.m_suffixeName;
			if (!textDictionary[key].IsSetup())
			{
				textDictionary[key].LoadResourcesSetup(fileName, this.m_languageName);
			}
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x00162980 File Offset: 0x00160B80
		public static void NotLoadSetupCommonText()
		{
			TextManager.NotLoadSetupText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "text_common_text");
			TextManager.SetupCommonText();
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x00162994 File Offset: 0x00160B94
		public static void NotLoadSetupChaoText()
		{
			TextManager.NotLoadSetupText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "text_chao_text");
			TextManager.SetupChaoText();
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x001629A8 File Offset: 0x00160BA8
		public static void NotLoadSetupEventText()
		{
			TextManager.NotLoadSetupText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "text_event_common_text");
			TextManager.SetupEventText();
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x001629BC File Offset: 0x00160BBC
		private static void NotLoadSetupText(TextManager.TextType textType, string baseName)
		{
			TextManager instance = TextManager.GetInstance();
			if (instance == null)
			{
				return;
			}
			Dictionary<TextManager.TextType, TextLoadImpl> textDictionary = instance.m_textDictionary;
			if (textDictionary.ContainsKey(textType))
			{
				GameObject gameObject = GameObject.Find(baseName + "_" + instance.m_suffixeName);
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				return;
			}
			textDictionary.Add(textType, new TextLoadImpl());
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x00162A28 File Offset: 0x00160C28
		public static void LoadCommonText(ResourceSceneLoader sceneLoader)
		{
			TextManager.Load(sceneLoader, TextManager.TextType.TEXTTYPE_COMMON_TEXT, "text_common_text");
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x00162A38 File Offset: 0x00160C38
		public static void LoadChaoText(ResourceSceneLoader sceneLoader)
		{
			TextManager.Load(sceneLoader, TextManager.TextType.TEXTTYPE_CHAO_TEXT, "text_chao_text");
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x00162A48 File Offset: 0x00160C48
		public static void LoadEventText(ResourceSceneLoader sceneLoader)
		{
			TextManager.Load(sceneLoader, TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "text_event_common_text");
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x00162A58 File Offset: 0x00160C58
		private static string GetEventProductionTextName(int specificId)
		{
			if (specificId > 0)
			{
				return "text_event_" + specificId.ToString() + "_text";
			}
			return null;
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x00162A7C File Offset: 0x00160C7C
		public static void LoadEventProductionText(ResourceSceneLoader sceneLoader)
		{
			if (EventManager.Instance != null && !EventUtility.IsExistSpecificEventText(EventManager.Instance.Id))
			{
				return;
			}
			int specificId = EventManager.GetSpecificId();
			string eventProductionTextName = TextManager.GetEventProductionTextName(specificId);
			if (!string.IsNullOrEmpty(eventProductionTextName))
			{
				TextManager.Load(sceneLoader, TextManager.TextType.TEXTTYPE_EVENT_SPECIFIC, eventProductionTextName);
			}
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x00162AD0 File Offset: 0x00160CD0
		public static void SetupCommonText()
		{
			TextManager.Setup(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "text_common_text");
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x00162AE0 File Offset: 0x00160CE0
		public static void SetupChaoText()
		{
			TextManager.Setup(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "text_chao_text");
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x00162AF0 File Offset: 0x00160CF0
		public static void SetupEventText()
		{
			TextManager.Setup(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "text_event_common_text");
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x00162B00 File Offset: 0x00160D00
		public static void SetupEventProductionText()
		{
			if (EventManager.Instance != null && !EventUtility.IsExistSpecificEventText(EventManager.Instance.Id))
			{
				return;
			}
			int specificId = EventManager.GetSpecificId();
			string eventProductionTextName = TextManager.GetEventProductionTextName(specificId);
			if (!string.IsNullOrEmpty(eventProductionTextName))
			{
				TextManager.Setup(TextManager.TextType.TEXTTYPE_EVENT_SPECIFIC, eventProductionTextName);
			}
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x00162B54 File Offset: 0x00160D54
		public static void Load(ResourceSceneLoader sceneLoader, TextManager.TextType textType, string fileName)
		{
			TextManager instance = TextManager.GetInstance();
			if (instance == null)
			{
				return;
			}
			Dictionary<TextManager.TextType, TextLoadImpl> textDictionary = instance.m_textDictionary;
			if (textDictionary.ContainsKey(textType))
			{
				return;
			}
			textDictionary.Add(textType, new TextLoadImpl(sceneLoader, fileName, instance.m_suffixeName));
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x00162B9C File Offset: 0x00160D9C
		public static void Setup(TextManager.TextType textType, string fileName)
		{
			TextManager instance = TextManager.GetInstance();
			if (instance == null)
			{
				return;
			}
			Dictionary<TextManager.TextType, TextLoadImpl> textDictionary = instance.m_textDictionary;
			if (!textDictionary.ContainsKey(textType))
			{
				return;
			}
			if (!textDictionary[textType].IsSetup())
			{
				textDictionary[textType].LoadSceneSetup(fileName, instance.m_languageName, instance.m_suffixeName);
			}
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x00162BFC File Offset: 0x00160DFC
		public static void UnLoad(TextManager.TextType textType)
		{
			TextManager instance = TextManager.GetInstance();
			if (instance == null)
			{
				return;
			}
			Dictionary<TextManager.TextType, TextLoadImpl> textDictionary = instance.m_textDictionary;
			if (!textDictionary.ContainsKey(textType))
			{
				return;
			}
			textDictionary.Remove(textType);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x00162C38 File Offset: 0x00160E38
		public static TextObject GetText(TextManager.TextType textType, string categoryName, string cellName)
		{
			TextManager instance = TextManager.GetInstance();
			if (instance == null)
			{
				return new TextObject(string.Empty);
			}
			Dictionary<TextManager.TextType, TextLoadImpl> textDictionary = instance.m_textDictionary;
			if (!textDictionary.ContainsKey(textType))
			{
				return new TextObject(string.Empty);
			}
			return new TextObject(textDictionary[textType].GetText(categoryName, cellName));
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x00162C94 File Offset: 0x00160E94
		public static int GetCategoryCellCount(TextManager.TextType textType, string categoryName)
		{
			TextManager instance = TextManager.GetInstance();
			if (instance == null)
			{
				return -1;
			}
			Dictionary<TextManager.TextType, TextLoadImpl> textDictionary = instance.m_textDictionary;
			if (!textDictionary.ContainsKey(textType))
			{
				return -1;
			}
			return textDictionary[textType].GetCellCount(categoryName);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x00162CD8 File Offset: 0x00160ED8
		private static TextManager GetInstance()
		{
			if (TextManager.m_instance == null)
			{
				GameObject gameObject = new GameObject("TextManager");
				gameObject.AddComponent<TextManager>();
			}
			return TextManager.m_instance;
		}

		// Token: 0x040039C0 RID: 14784
		private static TextManager m_instance;

		// Token: 0x040039C1 RID: 14785
		private Dictionary<TextManager.TextType, TextLoadImpl> m_textDictionary;

		// Token: 0x040039C2 RID: 14786
		private string m_languageName;

		// Token: 0x040039C3 RID: 14787
		private string m_suffixeName;

		// Token: 0x02000A2A RID: 2602
		public enum TextType
		{
			// Token: 0x040039C5 RID: 14789
			TEXTTYPE_NONE = -1,
			// Token: 0x040039C6 RID: 14790
			TEXTTYPE_COMMON_TEXT,
			// Token: 0x040039C7 RID: 14791
			TEXTTYPE_MILEAGE_MAP_COMMON,
			// Token: 0x040039C8 RID: 14792
			TEXTTYPE_MILEAGE_MAP_EPISODE,
			// Token: 0x040039C9 RID: 14793
			TEXTTYPE_MILEAGE_MAP_PRE_EPISODE,
			// Token: 0x040039CA RID: 14794
			TEXTTYPE_FIXATION_TEXT,
			// Token: 0x040039CB RID: 14795
			TEXTTYPE_EVENT_COMMON_TEXT,
			// Token: 0x040039CC RID: 14796
			TEXTTYPE_EVENT_SPECIFIC,
			// Token: 0x040039CD RID: 14797
			TEXTTYPE_CHAO_TEXT,
			// Token: 0x040039CE RID: 14798
			TEXTTYPE_END
		}
	}
}
