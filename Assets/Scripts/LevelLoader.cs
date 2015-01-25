using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public int Skip = 0;

	void Start()
	{
        Level.LevelIndex = Skip;
        //Level.LevelIndex = Skip;
        // Application.LoadLevelAdditive(Level.SceneNames[Level.LevelIndex]);
        Application.LoadLevelAdditive(Level.SceneNames[Skip]);
	}
}
