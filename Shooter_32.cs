using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_32 : MonoBehaviour {

    public float speed;

    private Rigidbody2D ShooterRb;


    private void Awake()
    {
        ShooterRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        ShooterRb.AddForce(Vector2.up*speed, ForceMode2D.Impulse);

       
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("TAG_1"))
        {
           
            cubemove_32.instance.SingleCube(collision.transform.position,collision.transform.parent);
            Destroy(gameObject);   
        }
        if(collision.gameObject.CompareTag("TAG_4"))
        {

            Destroy(gameObject);


        }

        
    }

}
