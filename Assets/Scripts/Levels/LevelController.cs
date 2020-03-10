using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject initPointPrefab; // Initial point prefab

    void Start()
    {
        SetupLevel();
    }

    void SetupLevel()
    {
        int currentLevel = GameManager.Instance.currentLevel;
        GameManager.Instance.previousPoint = -1;
        float[] level_data = GameManager.Instance.levels.levels[currentLevel].level_data;
        int pointIndex = 0;

        // Setups level with correponding points
        for (int i = 0; i < level_data.Length; i += 2)
        {
            pointIndex++;
            SpawnPoint(level_data[i], level_data[i + 1], pointIndex);
        }
        GameManager.Instance.pointCount = pointIndex;
    }

    void SpawnPoint(float x, float y, int index)
    {
        // Creates new point GameObject
        GameObject point = Instantiate(initPointPrefab, transform);

        // Sets point's x and y to correct position
        Vector3 pos = new Vector3(x , -y , 0);
        point.transform.localPosition = pos;

        // Changes point's index text to corresponding index
        point.GetComponentInChildren<Text>().text = index.ToString();
        point.GetComponent<PointController>().pointIndex = index - 1;
    }
}
