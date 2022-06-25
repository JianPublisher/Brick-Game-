using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed = 20f;
    Rigidbody _rigidbody;
    Vector3 _velocity;
    Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();  //grab access to ball's rigidbody
        Invoke("Launch", 0.5f);
    }
   
    void Launch()
    {
        _rigidbody.velocity = Vector3.up * _speed;  // the velocity of ball ( direction plus speed ) 
    }

    void FixedUpdate()    //FixedUpdate and Update is different
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;  //magnitude of direction is always 1, so speed is always constant.
        _velocity = _rigidbody.velocity; // storing the speed of rigidbody to the variable "_velocity"
        if (!_renderer.isVisible)
        {
            GameManager.Instance.BALLS--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity,collision.contacts[0].normal );        //if theres collision, then the rigidbody's velocity will be reflected ( with angle) 
    }

}
