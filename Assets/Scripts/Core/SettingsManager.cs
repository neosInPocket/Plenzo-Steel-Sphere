using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private bool removeSettings;
    private static string saveFilePath => Application.persistentDataPath + "/GameMainSettings.json";
    public static MainData Settings { get; private set; }

    private void Awake()
    {
        if (removeSettings)
        {
            Settings = new MainData();
            SetData();
        }
        else
        {
            GetSettings();
        }
    }

    public static void SetData()
    {
        if (!File.Exists(saveFilePath))
        {
            CreateNewSaveFile();
        }
        else
        {
            WriteDataFile();
        }
    }

    public static void GetSettings()
    {
        if (!File.Exists(saveFilePath))
        {
            CreateNewSaveFile();
        }
        else
        {
            string text = File.ReadAllText(saveFilePath);
            Settings = JsonUtility.FromJson<MainData>(text);
        }
    }

    private static void CreateNewSaveFile()
    {
        Settings = new MainData();
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(Settings));
    }

    private static void WriteDataFile()
    {
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(Settings));
    }
}
