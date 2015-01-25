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

    public static string[] SceneNames = { "intro1", "intro2", "box_intro1", "box_intro2", "box_stack", "air_sensor",
                                          "intro_funnel", "funnel_pit", "heavy_search", "heavy_intro", "voyage",
                                          "step_down" };

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
        if (Input.GetButtonDown("Restart") && _found)
        {
            Debug.Log(index);
            Debug.Log(SceneNames[index]);
            GameObject.Find(SceneNames[index]).GetComponentInChildren<DoorClose>().Restart();
            foreach (Placeable p in GameObject.Find(SceneNames[index]).GetComponentsInChildren<Placeable>())
            {
                p.Restart();
            }
            foreach (Activator a in GameObject.Find(SceneNames[index]).GetComponentsInChildren<Activator>())
            {
                a.Restart();
            }
            GameObject.Find("Player").transform.position = transform.position;
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
                pc.ShouldMove = true;
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
