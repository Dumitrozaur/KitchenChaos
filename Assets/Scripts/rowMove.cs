using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorMover : MonoBehaviour
{
    // List to store rows of the floor
    public List<Transform> floorRows;

    // X position to move rows to
    public float endXPosition = 10f;

    // Initial X position to reset rows to
    public float startXPosition = -10f;

    // Time to move each row
    public float moveTime = 2f;

    // Delay between each row starting its move
    public float rowStartDelay = 0.5f;

    void Start()
    {
        // Start the movement coroutine for each row with a delay
        StartCoroutine(MoveRows());
    }

    IEnumerator MoveRows()
    {
        while (true)
        {
            // Start moving each row with a delay between starts
            for (int i = 0; i < floorRows.Count; i++)
            {
                StartCoroutine(MoveRow(floorRows[i]));
                yield return new WaitForSeconds(rowStartDelay);
            }

            // Wait for all rows to finish their move before starting again
            yield return new WaitForSeconds(moveTime);
        }
    }

    IEnumerator MoveRow(Transform row)
    {
        while (true)
        {
            // Move row to the end x position
            Vector3 targetPosition = new Vector3(endXPosition, row.position.y, row.position.z);
            yield return MoveToPosition(row, targetPosition, moveTime);

            // Reset row to the initial x position
            row.position = new Vector3(startXPosition, row.position.y, row.position.z);
        }
    }

    IEnumerator MoveToPosition(Transform row, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = row.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            row.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        row.position = targetPosition;
    }
}
