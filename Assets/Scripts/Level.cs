using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    public static int LevelCount = 3;
    private static Vector3 _levelPos = new Vector3(0, 0, 0);
    public static int LevelIndex = 0;
    public int OffsetX, OffsetY, OffsetZ;
    private bool _found = false;
    private int index;

    void Start()
    {
        transform.position = _levelPos;
        _levelPos += new Vector3(OffsetX, OffsetY, OffsetZ);
        index = LevelIndex;
        if (LevelIndex == 0)
        {
            Next();
        }
    }

    void Update()
    {
        if (!_found && GameObject.Find("Player").transform.position.x > transform.position.x)
        {
            _found = true;
            if (index > 0)
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

			if (index + 1 < LevelCount) index++;

			Next ();
        }
    }

    public static void Next()
    {
        if (LevelIndex + 1 < LevelCount)
        {
            LevelIndex++;
            Application.LoadLevelAdditive(LevelIndex);
        }
    }
}
