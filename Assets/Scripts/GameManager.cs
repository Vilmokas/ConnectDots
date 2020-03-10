using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private TextAsset jsonFile; // Levels data file
    public Levels levels;  // Levels data
    public int currentLevel = 0;
    public int previousPoint = -1;
    public int pointCount = 0;
    public Vector3 firstPointPos; // Position of first point, so we know to which point draw role, when all points are clicked

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            ReadLevelsData(); // Read levels data from JSON file at the start of the game
            DontDestroyOnLoad(gameObject); // Keeps GameManager running in all scenes
            _instance = this;
        }
    }

    void ReadLevelsData()
    {
        levels = JsonUtility.FromJson<Levels>(jsonFile.text);
    }

    public bool CheckPoint(int index, Vector3 pos)
    {
        // Checks if point that was clicked is the next point
        if (index == previousPoint + 1)
        {
            previousPoint++;
            return true;
        }

        return false;
    }

    public bool LastPoint()
    {
        // Checks if the last point was clicked, so rope could connect to the first point
        if (previousPoint + 1 == pointCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
