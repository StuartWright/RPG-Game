using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    [SerializeField] float speed = 3;
    public float Speed
    {
        get
        {
            speed = speed + (SkillManager.Instance.Agility / 10);
            if(speed >= 7)
            {
                speed = 7;
            }
            return speed;
        }
    }
    float zMovement;
    float xMovement;
    public float zoom = 30;
    public float down = 3;
    float Rot = 65;
    bool hasZoomedIn = false;
    bool hasZoomedOut = false;
    public bool canMove = true;
    private GameObject Player;
    private PlayerRotate Rotate;
    private FaceCursor Turn;
    private Rigidbody RB;
    public bool DestroyOnLoad;
    private void Awake()
    {
        if(!DestroyOnLoad)
        DontDestroyOnLoad(gameObject);
        RB = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
         //test = Camera.main.transform.position.z;
        Player = GameObject.Find("Player");
        Rotate = GetComponent<PlayerRotate>();
        Turn = GetComponent<FaceCursor>();
        Turn.enabled = false;
        gameObject.transform.position = new Vector3(0,0,0);
        
    }
    void Update()
    {
        if(canMove)
        {
            zMovement = Input.GetAxis("Vertical");
            xMovement = Input.GetAxis("Horizontal");

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.5f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5f)
                anim.SetBool("condition", true);
            else
            {
                if(anim != null)
                anim.SetBool("condition", false);
            }
                
        }
        else
        {
            anim.SetBool("condition", false);
        }
         

        if (Input.GetKeyDown("space"))
           {
            canMove = false;
            Turn.enabled = true;
            Rotate.enabled = false;
            anim.SetBool("condition", false);
        }
        else if (Input.GetKeyUp("space"))
        {
            canMove = true;
            Rotate.enabled = true;
            Rotate.CanTurn = true;
            Turn.enabled = false;
        }

        
        if (Input.mouseScrollDelta.y > 0)
        {
            if(!hasZoomedIn)
            StartCoroutine(zoomIn());
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (!hasZoomedOut)
                StartCoroutine(zoomOut());
        }

    }

    void FixedUpdate()
    {
        zMovement *= Speed * Time.fixedDeltaTime;
        xMovement *= Speed * Time.fixedDeltaTime;

        RB.MovePosition(transform.position + new Vector3(zMovement, 0, xMovement));
        if(Player != null)
        Player.transform.position = gameObject.transform.position;//The player keeps getting pushed back too much when running into a wall, this is a quick fix for now until I find a better one.
    }

    //A BASIC ZOOM
    private IEnumerator zoomIn()
    {
        hasZoomedIn = true;
        float zoom = 6;
        yield return new WaitForSeconds(0.01f);
        Camera.main.transform.Translate(0,0, zoom * Time.deltaTime);
        if (Camera.main.transform.position.y >= 18)
            StartCoroutine(zoomIn());
        else
            hasZoomedOut = false;
    }
    private IEnumerator zoomOut()
    {
        hasZoomedOut = true;
        float zoom = 6;
        yield return new WaitForSeconds(0.01f);
        Camera.main.transform.Translate(0, 0, -zoom * Time.deltaTime);
        if (Camera.main.transform.position.y <= 24)
            StartCoroutine(zoomOut());
        else
            hasZoomedIn = false;
    }
}




