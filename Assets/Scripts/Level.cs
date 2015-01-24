using System.Net.Mail;
using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    public static int LevelCount = 2;
    private static Vector3 _levelPos = new Vector3(0, 0, 0);
    private static int _levelIndex = 0;
    public int OffsetX, OffsetY, OffsetZ;
    void Start()
    {
        transform.position = _levelPos;
        _levelPos += new Vector3(OffsetX, OffsetY, OffsetZ);
        if (_levelIndex == 0)
        {
            Next();
        }
    }

    public static void Next()
    {
        if (_levelIndex + 1 < LevelCount)
        {
            _levelIndex++;
            Application.LoadLevelAdditive(_levelIndex);
        }
    }
}
