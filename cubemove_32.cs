using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cubemove_32 : MonoBehaviour {

    public bool gameover;

    public GameObject GGameover;

 


    //sound
    public AudioSource SFX;
    public AudioClip[] SFXCLIPS;
    public AudioSource BG;



    public static cubemove_32 instance;

    public float CubeSpeed;

    GameObject Particle;
    //score Encryption
    public Bettr_Encryption.Encrypt score;
    public Text ScoreTxt;
    public Text Timer;
    public string SCORE_ENCRYPT;

    bool Isbonus;

    public GameObject particle;

    public float Rate;
   public   float AddedSpeed=1.2f;

    public GameObject CubePrefab;
    public GameObject SingleCubePrefab;
    public GameObject SingleBossCubePrefab;
    public GameObject shooter;
    public GameObject ShooterParent;
    public FastMobileBloom_32 fmb;

    public int bonus=0;
    public Text BonusText;
    public  float upwardForce;


    public ParticleSystem PS;
    public ParticleSystem PSRed;

    public ParticleSystem[] BonusPS;


    public GameObject BossCubePrefab;
 
    public float SpaceTime=0;

    private List<GameObject> CubeList = new List<GameObject>();

    Vector2 position;

    public  int tempi = 0;


    private Animation Anim;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        

    }



    // Use this for initialization
    void Start ()
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        Application.targetFrameRate = 60;

        Anim = GetComponent<Animation>();
      
        position = transform.position;

       



        score = new Bettr_Encryption.Encrypt(0);
        SCORE_ENCRYPT = "0";
        SCORE_ENCRYPT = XOREncryption.encryptDecrypt(SCORE_ENCRYPT);

    }

    // Update is called once per frame
    void Update ()
    {

        if (fmb.intensity != 0) fmb.intensity = Mathf.MoveTowards(fmb.intensity,0,0.1f);

    
            

        ScoreTxt.text = score.ToString();

      


        if (gameover)
        {
            GameOver();
            BG.Stop();
            SFX.PlayOneShot(SFXCLIPS[9]);

        }
       

        StartCoroutine(ClearCube());



        //Controls

        if (Input.GetMouseButtonDown(0))

        {
            //shoot sound
            SFX.PlayOneShot(SFXCLIPS[0]);
           

                //TAG_1
                if (Input.mousePosition.x < Screen.width / 4)
                {
                    GameObject block = Instantiate(shooter, new Vector3(-2f,-4.5f, 0), Quaternion.identity,ShooterParent.transform);
                    block.tag = "TAG_1";
                }

                //TAG_2
                if (Input.mousePosition.x >= Screen.width / 4 && Input.mousePosition.x < Screen.width / 4 * 2)
                {
                    GameObject block = Instantiate(shooter, new Vector3(-0.7f,-4.5f, 0), Quaternion.identity, ShooterParent.transform);
                    block.tag = "TAG_1";
                }

                //TAG_3
                if (Input.mousePosition.x >= Screen.width / 4 * 2 && Input.mousePosition.x < Screen.width / 4 * 3)
                {
                    GameObject block = Instantiate(shooter, new Vector3(0.7f,-4.5f , 0), Quaternion.identity, ShooterParent.transform);
                    block.tag = "TAG_1";
                }

                //TAG_4
                if (Input.mousePosition.x >= Screen.width / 4 * 3)
                {
                    GameObject block = Instantiate(shooter, new Vector3(2f,-4.5f , 0), Quaternion.identity, ShooterParent.transform);
                    block.tag = "TAG_1";
                }


                
          

        }




        shooter.transform.Translate(Vector2.up * CubeSpeed * Time.deltaTime);

        if (Time.time > SpaceTime)
        {

            SpaceTime = Time.time + Rate;
            CubeBarrier();

            if(SpaceTime<40)
                AddedSpeed += 0.01f;


        }



    }

    IEnumerator ClearCube()
    {
        yield return new WaitForEndOfFrame();

        
        if (CubeList[0].gameObject.transform.childCount > 3)
        {
            bonus++;

            if (bonus > 1)
            {

                score +=  new Bettr_Encryption.Encrypt(bonus * 5);
               
               SCORE_ENCRYPT = XOREncryption.encryptDecrypt(SCORE_ENCRYPT);
              SCORE_ENCRYPT = (int.Parse(SCORE_ENCRYPT) + bonus*5).ToString();
               SCORE_ENCRYPT = XOREncryption.encryptDecrypt(SCORE_ENCRYPT);
                BonusText.gameObject.SetActive(true);
                BonusText.text = bonus.ToString() + "X";
                //bloom of combo

                switch (bonus)
                {
                    case 2:
                        //bonus sound
                        SFX.PlayOneShot(SFXCLIPS[4]);
                        fmb.threshold = 0.68f;
                        fmb.intensity = 2f;
                        fmb.blurSize = 2.58f;
                        fmb.blurIterations = 4;
                        
                        break;

                    case 3:
                        SFX.PlayOneShot(SFXCLIPS[5]);
                        fmb.threshold = 0.68f;
                        fmb.intensity = 2f;
                        fmb.blurSize = 2.58f;
                        fmb.blurIterations = 4;
                        break;

                    case 4:
                        SFX.PlayOneShot(SFXCLIPS[6]);
                        fmb.threshold = 0.68f;
                        fmb.intensity = 3f;
                        fmb.blurSize = 2.58f;
                        fmb.blurIterations = 4;
                        break;
                    case 5:
                        SFX.PlayOneShot(SFXCLIPS[7]);
                        fmb.threshold = 0.68f;
                        fmb.intensity = 4f;
                        fmb.blurSize = 2.58f;
                        fmb.blurIterations = 4;
                        break;
                    case 6:
                        SFX.PlayOneShot(SFXCLIPS[8]);
                        fmb.threshold = 0.68f;
                        fmb.intensity = 5f;
                        fmb.blurSize = 2.58f;
                        fmb.blurIterations = 4;
                        break;
                }
              
            }
            else
            {
                BonusText.gameObject.SetActive(false);
            }

            foreach (Transform t in CubeList[0].gameObject.transform)
            {

                if (CubeList[0].gameObject.tag == "TAG_3")
                {

                  //score Encryption
                    score += new Bettr_Encryption.Encrypt(10);
                    SCORE_ENCRYPT = XOREncryption.encryptDecrypt(SCORE_ENCRYPT);
                    SCORE_ENCRYPT = (int.Parse(SCORE_ENCRYPT) + 10).ToString();
                    SCORE_ENCRYPT = XOREncryption.encryptDecrypt(SCORE_ENCRYPT);


                    if (bonus > 1)
                    {
                        foreach (ParticleSystem ps in BonusPS)
                        {
                            Instantiate(ps, t.transform.position, Quaternion.identity, particle.transform);
                        }
                    }
                    else if (bonus == 1)
                    {
                        //brick break sound
                        SFX.PlayOneShot(SFXCLIPS[3], 0.3f);
                        Instantiate(PS, t.transform.position, Quaternion.identity, particle.transform);
                    }
                    upwardForce = 0.2f;

                }
                else
                {
                  

                   

                    if (bonus > 1)
                    {
                        foreach (ParticleSystem ps in BonusPS)
                        {
                            Instantiate(ps, t.transform.position, Quaternion.identity, particle.transform);

                        }

                    }
                    else
                    {
                        SFX.PlayOneShot(SFXCLIPS[3], 0.3f);
                        Instantiate(PS, t.transform.position, Quaternion.identity, particle.transform);
                        //bloom on normal destroy
                        fmb.threshold = 0.38f;
                        fmb.intensity = 1f;
                        fmb.blurSize = 2.18f;
                        fmb.blurIterations = 1;

                    }

                  //score Encryption
                    score += new Bettr_Encryption.Encrypt(5);
                    SCORE_ENCRYPT = XOREncryption.encryptDecrypt(SCORE_ENCRYPT);
                    SCORE_ENCRYPT = (int.Parse(SCORE_ENCRYPT) + 5).ToString();
                    SCORE_ENCRYPT = XOREncryption.encryptDecrypt(SCORE_ENCRYPT);

                    upwardForce = 0.2f;
                }


            }
           
           
            if (CubeList[0].transform.position.y <-2)
                upwardForce *= 4;
            else
                upwardForce *= 1;


            Destroy(CubeList[0].gameObject);
            CubeList.Remove(CubeList[0]);
            
            position.y += upwardForce;                                                             
            transform.position = position;

            yield return new WaitForSeconds(0.1f);
            
            Destroy(particle.transform.GetChild(0).gameObject);
            Destroy(particle.transform.GetChild(1).gameObject);
            Destroy(particle.transform.GetChild(2).gameObject);

            yield return new WaitForSeconds(0.2f);
            bonus = 0;
            
            
        }
        StopCoroutine(ClearCube());
        
        
    }


    //instantiate cube
    public void CubeBarrier ()
    {

        GameObject CubeInstance;
       

        int i = Random.Range(0, 10);

        if (i < 2)
        {
            CubeInstance = Instantiate(BossCubePrefab, transform.position, Quaternion.identity, transform);
            CubeInstance.tag = "TAG_3";
            CubeList.Add(CubeInstance);
           

        }
        else
        {
            CubeInstance = Instantiate(CubePrefab, transform.position, Quaternion.identity, transform);
            CubeList.Add(CubeInstance);
        }

        if (SpaceTime < 40)
        {
            Rate -= 0.0037f;
        }
       
            CubeInstance.GetComponent<Cube_32>().speed = AddedSpeed;
       
            
        




    }


    public void SingleCube(Vector2 pos,Transform Parent)
        
    {
        //new Brick added
        SFX.PlayOneShot(SFXCLIPS[1]);

        GameObject SingleCubeInstance;

        int index = CubeList.IndexOf(Parent.gameObject);
        
        
            if (index==0)
            {
                GameObject SingleCube = new GameObject();
                SingleCube.transform.parent = this.transform;
                

                SingleCube.transform.localScale = Vector3.one;
                SingleCubeInstance = Instantiate(SingleCubePrefab, pos - new Vector2(0,Rate * CubeList[index].GetComponent<Cube_32>().speed), Quaternion.identity, SingleCube.transform);
          
                SingleCubeInstance.transform.localScale = Vector3.one;
                SingleCube.AddComponent<Cube_32>();
            
                SingleCube.GetComponent<Cube_32>().speed = CubeList[index].GetComponent<Cube_32>().speed;
                CubeList.Insert(0, SingleCube);

         

            }
            else if (index > 0)
            {

                if (CubeList[index - 1].tag == "TAG_3")
                {

                    CubeList[index - 1].GetComponent<Animator>().SetBool("CubeChange", true);

                    CubeList[index - 1].GetComponent<Cube_32>().damage -= 1;
                    if (CubeList[index - 1].GetComponent<Cube_32>().damage == 1)
                    {
                        //Destroy blue wall sound
                        SFX.PlayOneShot(SFXCLIPS[2], 1f);
                        foreach (Transform t in CubeList[0].gameObject.transform)
                            Instantiate(PSRed, t.transform.position, Quaternion.identity, particle.transform);
                    }
                    if (CubeList[index - 1].GetComponent<Cube_32>().damage < 1)
                    {
                    SingleCubeInstance = Instantiate(SingleCubePrefab, new Vector2(pos.x, CubeList[index - 1].transform.GetChild(0).position.y), Quaternion.identity, CubeList[index - 1].transform);
                    SingleCubeInstance.transform.localScale = Vector3.one;


                }




                }


                else
                {
                    SingleCubeInstance = Instantiate(SingleCubePrefab, new Vector2(pos.x,CubeList[index-1].transform.GetChild(0).position.y), Quaternion.identity, CubeList[index - 1].transform);
                    SingleCubeInstance.transform.localScale = Vector3.one;
                     
                  

                if (CubeList[index - 1].tag == "TAG_3")
                    {
                        CubeList[index - 1].GetComponent<Animator>().SetBool("CubeChange", true);
                        foreach (Transform t in CubeList[0].gameObject.transform)
                            Instantiate(PSRed, t.transform.position, Quaternion.identity, particle.transform);
                    }
                }
            }


        
    }
    



        


         
    public void GameOverOnTimer()
    {
        gameover = true;

    }
        


    


   public  void GameOver()
    {
        Anim.Play();
        //gameobject set active true
        GGameover.SetActive(true);
      
       
        gameover = false;
      
       
            foreach (GameObject gameObject in CubeList)
            {
                gameObject.GetComponent<Cube_32>().speed = 0;
            }

            
       
        this.enabled = false;

    }

   

   









    }




