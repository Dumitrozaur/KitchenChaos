using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorGrid : MonoBehaviour
{
    public GameObject cubeTile;
    public int rows = 10;
    public int columns = 10;
    public float tileSize = 1.0f;

    public GameObject[,] gridArray;

    public void Start()
    {
        gridArray = new GameObject[rows, columns];
        CreateGrid();
    }

    public void CreateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject tile = Instantiate(cubeTile, new Vector3(column * tileSize, 0, row * tileSize),
                    Quaternion.identity);
                tile.transform.parent = this.transform;
                gridArray[row, column] = tile;
            }
        }
    }
}
