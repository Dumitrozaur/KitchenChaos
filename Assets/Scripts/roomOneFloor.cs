using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomOneFloor : MonoBehaviour
{
    [SerializeField] private GameObject blackCube, whiteCube, greenCube;
    [SerializeField] private GameObject roomOne, roomTwo, roomThree;
    
    private float distanceBetweenTiles = 1f;
    
    [ContextMenu("GenerateCubesRoom1")]
    private void GenerateCubesRoom1()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Vector3 position = new Vector3(i * distanceBetweenTiles, 0, j * distanceBetweenTiles);
                GameObject cube;
                if ((i + j) % 2 == 0){
                    cube = whiteCube;
                }else{
                    cube = blackCube;
                }
                Instantiate(cube, position, Quaternion.identity, roomOne.transform);
            }
        }
    }

    [ContextMenu("GenerateCubeWalls")]
    private void GenerateCubesWall()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Vector3 position = new Vector3(i * distanceBetweenTiles, j * distanceBetweenTiles, 0);
                Instantiate(whiteCube, position, Quaternion.identity, roomThree.transform);
            }
        }
    }
}
