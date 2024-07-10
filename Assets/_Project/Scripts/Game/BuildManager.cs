using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    #endregion

    public enum BuildType
    {
     CSO,
     Tours1,
     Tours2,
     Debug
    }
    public BuildType buildType;
    
    public void SetBuildOptions() {
        switch (buildType)
        {
            case BuildType.CSO:
                // GameData.QuestionDatabaseUrls = new List<string> { "https://storage.googleapis.com/math-database/cso.csv" };
                GameData.QuestionDatabaseUrls = new List<string> { "http://localhost:3000/cso.csv" };

                GameData.TestTimes = new[] { 1080f };
                GameData.QuestionCount = new[] {26};
                break;
            case BuildType.Tours1:
                // GameData.QuestionDatabaseUrls = new List<string> { "https://storage.googleapis.com/math-database/tours_1.csv" };
                GameData.QuestionDatabaseUrls = new List<string> { "http://localhost:3000/tours_1.csv" };

                GameData.TestTimes = new[] { 2400f };
                GameData.QuestionCount = new[] {25};
                break;
            case BuildType.Tours2:
                // GameData.QuestionDatabaseUrls = new List<string> { "https://storage.googleapis.com/math-database/tours_2.csv" };
                GameData.QuestionDatabaseUrls = new List<string> { "http://localhost:3000/tours_2.csv" };

                GameData.TestTimes = new[] { 2700f };
                GameData.QuestionCount = new[] {15};
                break;
            case BuildType.Debug:
                // GameData.QuestionDatabaseUrls = new List<string> { "https://storage.googleapis.com/math-database/debug.csv" };
                GameData.QuestionDatabaseUrls = new List<string> { "http://localhost:3000/debug.csv" };
                // DebugManager.Instance.IsDebugBuild = true;
                GameData.QuestionCount = new[] {25};
                GameData.TestTimes = new[] { 99999f };
                break;
        }
    }
}
