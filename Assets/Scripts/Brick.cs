using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int points = 10;
    public Vector3 rotatespeed;
    public Material forhitmaterial; //after 1 hit

    Material _originalMaterial; // after the forhitmaterial, needa set it back to the original
    Renderer _renderer; //to access it 

    void Start()
    {
        transform.Rotate(rotatespeed * (transform.position.x + transform.position.y)*0.6f );
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.sharedMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        //make it nicer
        transform.Rotate(rotatespeed * Time.deltaTime);   //time.deltatime makes sure the brick is moving at the same speed
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        if (hits <= 0)
        {
            GameManager.Instance.SCORE += points;
            Destroy(gameObject);
           
        }
        _renderer.sharedMaterial = forhitmaterial;
        Invoke("RestoreMaterial", 0.06f);  //will call the function in 0.06 seconds, when function called, will restore back to original color
    }

    void RestoreMaterial()
    {
        _renderer.sharedMaterial = _originalMaterial;
    }
}
