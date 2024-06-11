using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorGrid : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public float tileSize = 1.0f;

    [SerializeField] public GameObject[,] gridArray;

    public void Start()
    {
        gridArray = new GameObject[rows, columns];
        FindGridObjects();
    }
    public void FindGridObjects()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Vector3 position = new Vector3(column * tileSize, 0, row * tileSize);
                Collider[] colliders = Physics.OverlapBox(position, Vector3.one * tileSize * 0.5f);

                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("CubeTile"))
                    {
                        gridArray[row, column] = collider.gameObject;
                        break;
                    }
                }
            }
        }
    }
}