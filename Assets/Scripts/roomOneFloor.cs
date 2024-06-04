using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomOneFloor : MonoBehaviour
{
    [SerializeField] private GameObject blackCube, whiteCube, greenCube;
    [SerializeField] private GameObject roomOne, roomTwo, roomThree;
    
    private float distanceBetweenTiles = 1f;
    
    [ContextMenu("GenerateCubesRoom1")]
    public void GenerateCubesRoom1(GameObject bC, GameObject wC, GameObject spawnObj, float tilesDistance)
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Vector3 position = new Vector3(i * tilesDistance, 0, j * tilesDistance);
                GameObject cube;
                if ((i + j) % 2 == 0){
                    cube = wC;
                }else{
                    cube = bC;
                }
                Instantiate(cube, position, Quaternion.identity, spawnObj.transform);
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
