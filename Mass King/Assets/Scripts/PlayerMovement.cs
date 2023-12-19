using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerControls;

    private Rigidbody _rb;
    //private CharacterController _controller;
    private Vector3 move;

    private Vector3 _moveVector = Vector3.zero;

    private float _moveSpeed = 30f;
    private float _jumpPower = 300f;

    private bool _isGrounded = false;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerControls = new PlayerInputActions();
        GetComponent<MeshRenderer>().material.color = GameManager.Instance.normalColor;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
       playerControls.Disable();
    }

    private void Update()
    {
        _moveVector = playerControls.Player.Move.ReadValue<Vector3>() * _moveSpeed;

        playerControls.Player.White.performed += ctx => ChangeColor(Color.white);
        playerControls.Player.Red.performed += ctx => ChangeColor(Color.red);
        playerControls.Player.Green.performed += ctx => ChangeColor(Color.green);
        playerControls.Player.Blue.performed += ctx => ChangeColor(Color.blue);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_moveVector);

        playerControls.Player.Jump.performed += Jump;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the player enters the ground layer
        if(collision.gameObject.layer == 6)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //If the player exits the ground layer
        if (collision.gameObject.layer == 6)
        {
            _isGrounded = false;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        
        if (_isGrounded) _rb.AddForce(Vector3.up * _jumpPower);
    }

    public void ChangeColor(Color color)
    {
        
        if(GameManager.Instance.Energy > 0)
        {
            GetComponent<MeshRenderer>().material.color = color;
        }
        

    }
}

