using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorHighlight : MonoBehaviour
{
    public FloorGrid gridManager;
    public Color highlightColor = Color.red;

    private int currentRow;
    private int currentCol;

    void Update()
    {
        Vector3 playerPos = transform.position;
        int newRow = Mathf.FloorToInt(playerPos.z / gridManager.tileSize);
        int newCol = Mathf.FloorToInt(playerPos.x / gridManager.tileSize);

        if (newRow != currentRow || newCol != currentCol)
        {
            currentRow = newRow;
            currentCol = newCol;
            HighlightGrid();
        }
    }

    void HighlightGrid()
    {
        // Reset all tiles to default color
        foreach (var tile in gridManager.gridArray)
        {
            tile.GetComponent<Renderer>().material.color = Color.white;
        }

        // Highlight the row
        for (int col = 0; col < gridManager.columns; col++)
        {
            gridManager.gridArray[currentRow, col].GetComponent<Renderer>().material.color = highlightColor;
        }

        // Highlight the column
        for (int row = 0; row < gridManager.rows; row++)
        {
            gridManager.gridArray[row, currentCol].GetComponent<Renderer>().material.color = highlightColor;
        }
    }
}
