using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody _rigidbody;
    
    void Start()
    {
        
        _rigidbody = GetComponent<Rigidbody>();   
    }

  
    void Update()
    {
        _rigidbody.MovePosition(new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,0,50)).x, -16,0));  //everytime you want to move the rigidbody, use move position.
                                                                               //need to do screentoworld because unity operates on world space
                                                                               //mouseposition.x because we move x, 0 because y we wont move, and 50 because we moved the camera -50
    }
}
