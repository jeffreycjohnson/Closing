using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    private static int levelIndex = 0;
    void Start()
    {
        Next();
    }

    public static void Next()
    {
        levelIndex++;
        Application.LoadLevelAdditive(levelIndex);
    }
}
