using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public int Skip = 0;
    public static int NewSkip = 0;

	void Start()
	{
	    if (NewSkip > Skip)
	    {
	        Skip = NewSkip;
	    }
	    if (Level.LevelIndex == 0)
	    {
			Level.LevelIndex = Skip;
			Application.LoadLevelAdditive(Level.SceneNames[Skip]);
	    }
	}
}
