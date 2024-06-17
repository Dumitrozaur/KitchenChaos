using UnityEngine;

public class FloorTile : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
