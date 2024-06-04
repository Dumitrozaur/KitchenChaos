using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private int MovSpeed = 1;
    [SerializeField] private float rotateSpeed = 5f;
    public bool isWalking;
    private void Update()
    {
        Vector2 input = _gameInput.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(input.x, 0f, input.y);
        isWalking = movDir != Vector3.zero;
        transform.position += movDir * (MovSpeed * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
        
    }

    public bool Walking()
    {
        return !isWalking;
    }
}
