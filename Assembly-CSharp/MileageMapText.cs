using System;
using Text;
using UnityEngine;

// Token: 0x0200067A RID: 1658
public class MileageMapText : MonoBehaviour
{
	// Token: 0x06002C2C RID: 11308 RVA: 0x0010B830 File Offset: 0x00109A30
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06002C2D RID: 11309 RVA: 0x0010B83C File Offset: 0x00109A3C
	public static void Load(ResourceSceneLoader sceneLoader, int episode, int pre_episode)
	{
		TextManager.Load(sceneLoader, TextManager.TextType.TEXTTYPE_MILEAGE_MAP_COMMON, "text_mileage_map_common_text");
		int startEpisode = MileageMapText.GetStartEpisode(episode);
		if (MileageMapText.m_start_episode != -1 && MileageMapText.m_start_episode != startEpisode)
		{
			TextManager.UnLoad(TextManager.TextType.TEXTTYPE_MILEAGE_MAP_EPISODE);
		}
		TextManager.Load(sceneLoader, TextManager.TextType.TEXTTYPE_MILEAGE_MAP_EPISODE, MileageMapText.GetTextFileName(startEpisode));
		MileageMapText.m_start_episode = startEpisode;
		if (pre_episode != -1)
		{
			int startEpisode2 = MileageMapText.GetStartEpisode(pre_episode);
			if (startEpisode != startEpisode2)
			{
				TextManager.Load(sceneLoader, TextManager.TextType.TEXTTYPE_MILEAGE_MAP_PRE_EPISODE, MileageMapText.GetTextFileName(startEpisode2));
				MileageMapText.m_start_pre_episode = startEpisode2;
			}
		}
	}

	// Token: 0x06002C2E RID: 11310 RVA: 0x0010B8B4 File Offset: 0x00109AB4
	public static void Setup()
	{
		TextManager.Setup(TextManager.TextType.TEXTTYPE_MILEAGE_MAP_COMMON, "text_mileage_map_common_text");
		if (MileageMapText.m_start_episode != -1)
		{
			TextManager.Setup(TextManager.TextType.TEXTTYPE_MILEAGE_MAP_EPISODE, MileageMapText.GetTextFileName(MileageMapText.m_start_episode));
		}
		if (MileageMapText.m_start_pre_episode != -1)
		{
			TextManager.Setup(TextManager.TextType.TEXTTYPE_MILEAGE_MAP_PRE_EPISODE, MileageMapText.GetTextFileName(MileageMapText.m_start_pre_episode));
		}
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x0010B904 File Offset: 0x00109B04
	public static void DestroyPreEPisodeText()
	{
		if (MileageMapText.m_start_pre_episode != -1)
		{
			TextManager.UnLoad(TextManager.TextType.TEXTTYPE_MILEAGE_MAP_PRE_EPISODE);
			MileageMapText.m_start_pre_episode = -1;
		}
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x0010B920 File Offset: 0x00109B20
	public static string GetText(int episode, string label)
	{
		if (label.IndexOf("cmn_") == 0)
		{
			TextManager.TextType type = TextManager.TextType.TEXTTYPE_MILEAGE_MAP_COMMON;
			return TextUtility.GetText(type, "MileageMap", label);
		}
		int startEpisode = MileageMapText.GetStartEpisode(episode);
		if (MileageMapText.m_start_episode == startEpisode)
		{
			TextManager.TextType type2 = TextManager.TextType.TEXTTYPE_MILEAGE_MAP_EPISODE;
			return TextUtility.GetText(type2, "MileageMap", label);
		}
		if (MileageMapText.m_start_pre_episode == startEpisode)
		{
			TextManager.TextType type3 = TextManager.TextType.TEXTTYPE_MILEAGE_MAP_PRE_EPISODE;
			return TextUtility.GetText(type3, "MileageMap", label);
		}
		return null;
	}

	// Token: 0x06002C31 RID: 11313 RVA: 0x0010B988 File Offset: 0x00109B88
	public static string GetMapCommonText(string label)
	{
		if (label.IndexOf("cmn_") == 0)
		{
			TextManager.TextType type = TextManager.TextType.TEXTTYPE_MILEAGE_MAP_COMMON;
			return TextUtility.GetText(type, "MileageMap", label);
		}
		return null;
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x0010B9B8 File Offset: 0x00109BB8
	public static string GetName(string name)
	{
		TextManager.TextType textType = TextManager.TextType.TEXTTYPE_MILEAGE_MAP_COMMON;
		TextObject text = TextManager.GetText(textType, "Name", name);
		if (text != null && text.text != null)
		{
			return text.text;
		}
		return null;
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x0010B9F0 File Offset: 0x00109BF0
	private static int GetStartEpisode(int episode)
	{
		if (episode > 0)
		{
			int num = episode / 10;
			if (episode % 10 == 0)
			{
				num--;
			}
			int num2 = num * 10;
			return num2 + 1;
		}
		return 1;
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x0010BA24 File Offset: 0x00109C24
	private static string GetTextFileName(int start_episode)
	{
		if (start_episode > 0)
		{
			int num = start_episode + 9;
			return string.Concat(new string[]
			{
				"text_mileage_map_episode_",
				start_episode.ToString("D2"),
				"_to_",
				num.ToString("D2"),
				"_text"
			});
		}
		return "text_mileage_map_episode_01_to_10_text";
	}

	// Token: 0x04002943 RID: 10563
	private static int m_start_episode = -1;

	// Token: 0x04002944 RID: 10564
	private static int m_start_pre_episode = -1;
}
