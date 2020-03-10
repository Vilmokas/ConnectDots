using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionController : MonoBehaviour
{
    private int levelCount = 0;
    [SerializeField] GameObject buttonPrefab; // Level selection button prefab

    // Start is called before the first frame update
    void Start()
    {
        levelCount = GameManager.Instance.levels.levels.Length;

        // Add level selection buttons to level selection panel
        for (int i = 0; i < levelCount; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponentInChildren<Text>().text = (i + 1).ToString();
            var index = i; // Seperates index value from reference
            button.GetComponent<Button>().onClick.AddListener(() => ChangeLevel(index)); // Add button listener to change level index when clicked
        }
    }

    void ChangeLevel(int index)
    {
        GameManager.Instance.currentLevel = index; // Changes level index, this is needed so LevelController knows which level to load
        SceneManager.LoadScene(1); // Change scene to level scene
    }
}
