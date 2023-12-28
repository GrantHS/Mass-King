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

    private PlatformColor _platColor = PlatformColor.White;

    public bool _isGrounded = false;

    public PlatformColor playerColor;


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

        playerControls.Player.White.performed += ctx => ChangeColor(Color.white, PlatformColor.White);
        playerControls.Player.Red.performed += ctx => ChangeColor(Color.red, PlatformColor.Red);
        playerControls.Player.Green.performed += ctx => ChangeColor(Color.green, PlatformColor.Green);
        playerControls.Player.Blue.performed += ctx => ChangeColor(Color.blue, PlatformColor.Blue);

        if (GameManager.Instance.isDrained)
        {
            GameManager.Instance.isDrained = false;
            GameManager.Instance.CheckPlatform(_platColor, playerColor);
        }
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

        if (collision.gameObject.GetComponent<Platform>())
        {
            Debug.Log("Hit Platform");
            _platColor = collision.gameObject.GetComponent<Platform>().color;
            GameManager.Instance.CheckPlatform(_platColor, playerColor);
            


        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _isGrounded = true;
        }

        if (collision.gameObject.GetComponent<Platform>())
        {
            Debug.Log("Hit Platform");
            GameManager.Instance.CheckPlatform(collision.gameObject.GetComponent<Platform>().color, playerColor);
            /*
            if (collision.gameObject.GetComponent<Platform>().color != PlatformColor.White && collision.gameObject.GetComponent<Platform>().color != playerColor)
            {
                Debug.Log("Wrong Color");
                StartCoroutine(GameManager.Instance.RespawnPlayer());
                playerColor = PlatformColor.White;
            }
            */
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

    public void ChangeColor(Color color, PlatformColor id)
    {
        
        if(GameManager.Instance.Energy > 0)
        {
            GetComponent<MeshRenderer>().material.color = color;
            playerColor = id;
        }


    }
}

