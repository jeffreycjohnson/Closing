using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public int Skip = 0;

	// Use this for initialization
	void Start ()
	{
        Level.LevelIndex = Skip + 1;
        Application.LoadLevelAdditive(Skip + 1);
	}
}
