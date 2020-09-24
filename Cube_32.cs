using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_32 : MonoBehaviour
{
   

    public int damage;

    public float speed;

    // Use this for initialization
    void Start()
    {

        int i;

        do
        {
            i = Random.Range(0, 4);
        } while (i == cubemove_32.instance.tempi);

        if (this.gameObject.tag == "TAG_2" || this.gameObject.tag == "TAG_3")
            Destroy(transform.GetChild(i).gameObject);

        cubemove_32.instance.tempi = i;


        if (this.gameObject.tag == "TAG_3")
        {
            damage = 2;
        }


     



    }

    // Update is called once per frame
    void Update()
    {


        transform.Translate(Vector2.down * speed * Time.deltaTime);

       


    }


  
}



    
