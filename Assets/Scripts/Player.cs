using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private int MovSpeed = 1;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float _playerRadius = .7f;
    [SerializeField] private float _playerHeight = 2f;
    public bool isWalking;
    private void Update()
    {
        
        Vector2 input = _gameInput.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(input.x, 0f, input.y);
        
        var moveDistance = MovSpeed * Time.deltaTime;
        var step = movDir * moveDistance;
        
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, movDir, moveDistance);
        
        isWalking = movDir != Vector3.zero;
        if (!canMove)
        {
            var moveDirX = new Vector3(movDir.x, 0, 0); 
            bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirX, moveDistance);
            if (canMoveX)
            {
                canMove = true;
                step = moveDirX * moveDistance;
            }

            if (!canMoveX)
            {
                var moveDirZ = new Vector3(0, 0, moveDirX.y);
                bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirZ, moveDistance);
                if (canMoveZ)
                {
                    canMove = true;
                    step = moveDirZ * moveDistance;
                }
            }
        }
        if (canMove)
        {
            transform.position += step;
        }

        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
        
    }

    public bool Walking()
    {
        return !isWalking;
    }
}
