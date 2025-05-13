using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdShitUI : MonoBehaviour
{
    public RectTransform maskParent;      // UI container (e.g., Panel)
    public GameObject maskPrefab;         // Prefab (UI Image with RectTransform)
    public RectTransform myImageRect;     // The image to be covered
    public RectTransform maskingAreaRect; // New: UI area where bird shit is allowed to appear

    public int gridSize = 10;
    public float coverageThreshold = 0.95f; // 95% coverage required

    private bool[,] coverageGrid;
    private bool pressed;
    private bool scriptEnabled = true;
    private List<GameObject> masks = new List<GameObject>();

    private Rect imageWorldRect;

    void Start()
    {
        coverageGrid = new bool[gridSize, gridSize];
        imageWorldRect = myImageRect.GetWorldRect();
    }

    void Update()
    {
        if (!scriptEnabled)
            return;

        // Check if mouse is within the masking area (ignores clicks outside it)
        if (RectTransformUtility.RectangleContainsScreenPoint(maskingAreaRect, Input.mousePosition, null))
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                maskParent, Input.mousePosition, null, out localPoint);

            if (pressed)
            {
                GameObject ob = Instantiate(maskPrefab, maskParent);
                ob.GetComponent<RectTransform>().anchoredPosition = localPoint;
                masks.Add(ob);
                MarkCoveredCells(ob.GetComponent<RectTransform>());
            }
        }

        if (Input.GetMouseButtonDown(0))
            pressed = true;
        else if (Input.GetMouseButtonUp(0))
            pressed = false;

        if (CheckCoverage())
        {
            Debug.Log("Fully covered!");
            myImageRect.gameObject.SetActive(false);
            foreach (var m in masks)
                m.SetActive(false);
            masks.Clear();
            scriptEnabled = false;
        }
    }

    void MarkCoveredCells(RectTransform maskRect)
    {
        Rect maskWorld = maskRect.GetWorldRect();

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Rect cell = GetGridCellRect(x, y);
                if (maskWorld.Overlaps(cell, true))
                {
                    coverageGrid[x, y] = true;
                }
            }
        }
    }

    bool CheckCoverage()
    {
        int covered = 0;
        int total = gridSize * gridSize;
        foreach (bool cell in coverageGrid)
            if (cell) covered++;
        return ((float)covered / total) >= coverageThreshold;
    }

    Rect GetGridCellRect(int x, int y)
    {
        float cellWidth = imageWorldRect.width / gridSize;
        float cellHeight = imageWorldRect.height / gridSize;
        Vector2 min = new Vector2(
            imageWorldRect.xMin + x * cellWidth,
            imageWorldRect.yMin + y * cellHeight);
        return new Rect(min, new Vector2(cellWidth, cellHeight));
    }
}
