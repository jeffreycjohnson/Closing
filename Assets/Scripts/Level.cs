using System.Linq;
using UnityEngine;
using System.Collections;
using System;

public class Level : MonoBehaviour
{
    private static Vector3 _levelPos = new Vector3(0, 0, 0);
    public static int LevelIndex = 0;
    public int OffsetX, OffsetY, OffsetZ;
    private bool _found = false;
    private int index;
    private static bool _first = true;
    public static bool ShouldRestart = false;

    public static string[] SceneNames = { "intro_run1", "intro_run2", "box_intro1", "box_intro2", "box_stack", "box_air_sensor",
                                          "funnel_up", "funnel_cross_pit", "heavy_search", "heavy_push1", "heavy_push2",
                                          "box_platform_simple", "box_stepping_stone", "box_mountain" };

    void Start()
    {
        if (GameObject.Find("Player") == null)
        {
            LevelIndex = Array.FindIndex(SceneNames, 0, x => x.Contains(Application.loadedLevelName));
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
        if ((Input.GetButtonDown("Restart") || ShouldRestart) && index == LevelIndex - 2)
        {
            LevelIndex = 0;
            _first = true;
            _levelPos = new Vector3(0, 0, 0);
            LevelLoader.NewSkip = index;
            Application.LoadLevel(0);
            Placeable.Restart();
            ShouldRestart = false;
        }
        Screen.lockCursor = true;
        GameObject player = GameObject.Find("Player");
        if (!player) return;
        if (!_found && player.transform.position.x > transform.position.x)
        {
            _found = true;
            if (index > FindObjectOfType<LevelLoader>().Skip)
            {
                GameObject level = GameObject.Find(SceneNames[index]);
                level.GetComponentInChildren<DoorClose>().doneMoving = true;
                foreach (PlatformController pc in level.GetComponentsInChildren<PlatformController>())
				{
                    pc.ShouldMove = false;
				}
            }

            GameObject level2 = GameObject.Find(SceneNames[index + 1]);
            level2.GetComponentInChildren<DoorClose>().doneMoving = false;
            
            foreach (PlatformController pc in level2.GetComponentsInChildren<PlatformController>())
            {
                pc.ShouldMove = pc.GetComponent<Target>().Running;
                pc.LevelReached = true;
            }

            FallController[] fcn =
				level2.GetComponentsInChildren<FallController>();

            foreach (FallController fc in fcn)
            {
                fc.Falling = true;
            }

			if (index + 1 < SceneNames.Length) index++;
			Next();
        }
    }

    public static void Next()
    {
        if (LevelIndex + 1 < SceneNames.Length)
        {
            LevelIndex++;
            Application.LoadLevelAdditive(SceneNames[Level.LevelIndex]);
        }
    }
}
