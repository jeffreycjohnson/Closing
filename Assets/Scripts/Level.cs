using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    private static Vector3 _levelPos = new Vector3(0, 0, 0);
    public static int LevelIndex = 0;
    public int OffsetX, OffsetY, OffsetZ;
    private bool _found = false;
    private int index;
    private static bool _first = true;

    void Start()
    {
        if (GameObject.Find("Player") == null)
        {
            LevelIndex = Application.loadedLevel;
            Application.LoadLevelAdditive(0);
            LevelLoader.NewSkip = LevelIndex;
        }
        transform.position = _levelPos;
        _levelPos += new Vector3(OffsetX, OffsetY, OffsetZ);
        index = LevelIndex - 1;
        if (_first)
        {
            _first = false;
            Next();
        }
    }

    void Update()
    {
        Screen.lockCursor = true;
        if (!_found && GameObject.Find("Player").transform.position.x > transform.position.x)
        {
            _found = true;
            if (index > GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Skip)
            {
                GameObject.Find("Level " + index).GetComponentInChildren<DoorClose>().doneMoving = true;

				PlatformController[] pcs = 
					GameObject.Find("Level " + index).GetComponentsInChildren<PlatformController>();

				foreach (PlatformController pc in pcs)
				{
					pc.ShouldMove = false;
				}
            }
            GameObject.Find("Level " + (index + 1)).GetComponentInChildren<DoorClose>().doneMoving = false;

			PlatformController[] pcsn = 
				GameObject.Find("Level " + (index + 1)).GetComponentsInChildren<PlatformController>();
			
			foreach (PlatformController pc in pcsn)
			{
				pc.ShouldMove = true;
			}

			if (index + 1 < Application.levelCount) index++;

			Next ();
        }
    }

    public static void Next()
    {
        if (LevelIndex + 1 < Application.levelCount)
        {
            LevelIndex++;
            Application.LoadLevelAdditive(LevelIndex);
        }
    }
}
