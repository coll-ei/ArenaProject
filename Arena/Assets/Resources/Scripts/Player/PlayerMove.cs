using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float _speed;
    private Rigidbody _rigid;


    [Header("Jump settings")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _distanceDown;
    [SerializeField] private LayerMask _groundLayer;
    private bool _grounded;

    [Header("Camera settings")]
    [SerializeField] private float _sensitivity;
    [SerializeField] private Camera _camera;
    private Vector3 _turn;



    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        Moving();
        Rotate();
    }


    private void Moving()
    {
        float Horizontal = Input.GetAxis("Horizontal") * _speed;
        float Vertical = Input.GetAxis("Vertical") * _speed;
        Vector3 move = transform.forward * Vertical + transform.right * Horizontal;
        move.y = _rigid.velocity.y;

        _rigid.velocity = move;

        _grounded = Physics.Raycast(transform.position, Vector3.down, _distanceDown, _groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rigid.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        if (Horizontal == 0 && _grounded == true && Vertical == 0)
        {
            PlayerInfo.instance.AnimMoving(false);
        }
        else
        {
            PlayerInfo.instance.AnimMoving(true);
        }
    }

    private void Rotate()
    {
        _turn.x += Input.GetAxis("Mouse X") * _sensitivity;

        transform.localRotation = Quaternion.Euler(0, _turn.x, 0);
        
    }
}
