using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public int Skip = 0;
    public static int NewSkip = 0;

	// Use this for initialization
	void Start ()
	{
	    if (NewSkip > Skip)
	    {
	        Skip = NewSkip;
	    }
	    if (Level.LevelIndex == 0)
	    {
	        Level.LevelIndex = Skip + 1;
	        Application.LoadLevelAdditive(Skip + 1);
	    }
	}
}
