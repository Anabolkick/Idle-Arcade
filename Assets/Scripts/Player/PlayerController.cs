using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick Joystick;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed;
    [SerializeField] float _rotationSmoothTime;

    private float _currentAngleVelocity, _currentAngle;
    
   

    private void Update()
    {
       Move();
    }

    void Move()
    {
        var movementVector = new Vector3(Joystick.Horizontal, 0f, Joystick.Vertical);
        var movementMagnitude = movementVector.magnitude;
        if (movementMagnitude >= 0.085f)
        {
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
            _currentAngle = Mathf.SmoothDampAngle(_currentAngle, targetAngle, ref _currentAngleVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, _currentAngle, 0);

            _characterController.Move(_moveSpeed * Time.deltaTime * movementVector);
        }
    }
}
