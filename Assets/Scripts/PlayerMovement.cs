using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerWalkSpeed, _playerDashSpeed;
    [SerializeField] private Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0)*Time.deltaTime*_playerWalkSpeed;

        _rb2d.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0)*Time.deltaTime*_playerWalkSpeed;
    }
}
