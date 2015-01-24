using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    private static int _levelIndex = 0;
    public static int LevelCount = 1;
    void Start()
    {
        Next();
    }

    public static void Next()
    {
        if (_levelIndex < LevelCount)
        {
            _levelIndex++;
            Application.LoadLevelAdditive(_levelIndex);
        }
    }
}
