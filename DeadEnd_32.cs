using UnityEngine;

public class DeadEnd_32 : MonoBehaviour {


    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="TAG_1")
        {
            cubemove_32.instance.gameover = true;
                
        }

        





    }
}
