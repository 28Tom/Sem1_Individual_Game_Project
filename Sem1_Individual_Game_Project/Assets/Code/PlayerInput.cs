using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurrent = new UnityEvent<Vector2>();

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GetBodyMovement();
        GetTurrentMovement();
        GetShootingInput();
    }
    public void GetShootingInput()
    {
        if (Input.GetMouseButton(0))
        {
            OnShoot?.Invoke();
        }
    }

    private void GetTurrentMovement()
    {
        OnMoveTurrent?.Invoke(GetMousePosition());
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mousePostion = Input.mousePosition;
        mousePostion.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePostion);
        return mouseWorldPosition;
    }

    private void GetBodyMovement() 
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
    }
}
