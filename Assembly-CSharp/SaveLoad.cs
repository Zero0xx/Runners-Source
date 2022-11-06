using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SaveData;

// Token: 0x020002B0 RID: 688
public class SaveLoad
{
	// Token: 0x0600134C RID: 4940 RVA: 0x0006996C File Offset: 0x00067B6C
	public static void SaveData<Type>(Type obj)
	{
		Type type = obj.GetType();
		if (type == typeof(PlayerData))
		{
			SaveLoad.CreateSaveData<Type>(obj, "Assets/Runners Assets/Save/save_data_player.xml");
		}
		else if (type == typeof(CharaData))
		{
			SaveLoad.CreateSaveData<Type>(obj, "Assets/Runners Assets/Save/save_data_chara.xml");
		}
		else if (type == typeof(ChaoData))
		{
			SaveLoad.CreateSaveData<Type>(obj, "Assets/Runners Assets/Save/save_data_chao.xml");
		}
		else if (type == typeof(ItemData))
		{
			SaveLoad.CreateSaveData<Type>(obj, "Assets/Runners Assets/Save/save_data_item.xml");
		}
		else if (type == typeof(OptionData))
		{
			SaveLoad.CreateSaveData<Type>(obj, "Assets/Runners Assets/Save/save_data_option.xml");
		}
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x00069A24 File Offset: 0x00067C24
	public static void LoadSaveData<Type>(ref Type obj)
	{
		Type type = obj.GetType();
		if (type == typeof(PlayerData))
		{
			SaveLoad.LoadSaveData<Type>(ref obj, "Assets/Runners Assets/Save/save_data_player.xml");
		}
		else if (type == typeof(CharaData))
		{
			SaveLoad.LoadSaveData<Type>(ref obj, "Assets/Runners Assets/Save/save_data_chara.xml");
		}
		else if (type == typeof(ChaoData))
		{
			SaveLoad.LoadSaveData<Type>(ref obj, "Assets/Runners Assets/Save/save_data_chao.xml");
		}
		else if (type == typeof(ItemData))
		{
			SaveLoad.LoadSaveData<Type>(ref obj, "Assets/Runners Assets/Save/save_data_item.xml");
		}
		else if (type == typeof(OptionData))
		{
			SaveLoad.LoadSaveData<Type>(ref obj, "Assets/Runners Assets/Save/save_data_option.xml");
		}
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x00069ADC File Offset: 0x00067CDC
	private static void LoadSaveData<Type>(ref Type obj, string path)
	{
		if (SaveLoad.CheckSaveData(path))
		{
			if (!SaveLoad.LoadXMLSaveData<Type>(ref obj, path))
			{
				SaveLoad.CreateSaveData<Type>(obj, path);
			}
		}
		else
		{
			SaveLoad.CreateSaveData<Type>(obj, path);
		}
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x00069B20 File Offset: 0x00067D20
	private static void CreateSaveData<Type>(Type obj, string path)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(Type));
		StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8);
		if (streamWriter != null)
		{
			if (obj != null)
			{
				xmlSerializer.Serialize(streamWriter, obj);
			}
			streamWriter.Close();
		}
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x00069B70 File Offset: 0x00067D70
	private static bool LoadXMLSaveData<Type>(ref Type obj, string path)
	{
		bool result = false;
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(Type));
		StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
		if (streamReader != null)
		{
			object obj2 = (Type)((object)xmlSerializer.Deserialize(streamReader));
			if (obj2 != null)
			{
				obj = (Type)((object)obj2);
				result = true;
			}
			streamReader.Close();
		}
		return result;
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x00069BD0 File Offset: 0x00067DD0
	public static bool CheckSaveData(string path)
	{
		return File.Exists(path);
	}

	// Token: 0x040010D1 RID: 4305
	private const string PLAYER_FILE_PATH = "Assets/Runners Assets/Save/save_data_player.xml";

	// Token: 0x040010D2 RID: 4306
	private const string CHAO_FILE_PATH = "Assets/Runners Assets/Save/save_data_chao.xml";

	// Token: 0x040010D3 RID: 4307
	private const string OPTION_FILE_PATH = "Assets/Runners Assets/Save/save_data_option.xml";

	// Token: 0x040010D4 RID: 4308
	private const string ITEM_FILE_PATH = "Assets/Runners Assets/Save/save_data_item.xml";

	// Token: 0x040010D5 RID: 4309
	private const string CHARA_FILE_PATH = "Assets/Runners Assets/Save/save_data_chara.xml";
}
