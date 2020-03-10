using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite clickedSprite; // Point sprite after point was clicked
    public int pointIndex = 0;
    private bool clicked = false;
    private Image image; // Point image reference

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // When point is clicked, checks if it was already clicked
        if (!clicked)
        {
            // Checks if clicked point is the next point
            if (GameManager.Instance.CheckPoint(pointIndex, transform.position))
            {
                // If next point, then change point to clicked state
                ChangePoint();
            }
        }
    }

    void ChangePoint()
    {
        clicked = true;
        // If first point was clicked, it's position is passed to GameManager to know the end position of the last rope
        if (pointIndex == 0)
        {
            GameManager.Instance.firstPointPos = GetComponent<RectTransform>().localPosition;
        }
        // Disables point index text, starts fade animation and adds new rope
        gameObject.GetComponentInChildren<Text>().enabled = false;
        StartCoroutine(FadeAway());
        GameObject.Find("_RopeController").GetComponent<RopeController>().AddRopePoint(transform.localPosition);
    }

    private IEnumerator FadeAway()
    {
        // Sprite's oppacity fades to 0
        while(image.color.a > 0)
        {
            Color transparentAplha = new Color(255, 255, 255, image.color.a - 0.05f);
            image.color = transparentAplha;
            yield return new WaitForSeconds(0.03f);
        }
        // Sprite is changed to the clicked sprite prefab
        image.sprite = clickedSprite;
        // Sprite's oppacity fades back to 1
        while (image.color.a < 255)
        {
            Color restoredAplha = new Color(255, 255, 255, image.color.a + 0.05f);
            image.color = restoredAplha;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
