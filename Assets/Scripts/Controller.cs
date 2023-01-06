using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller find;
    public GameObject prefabSaveStation;
    public Vector3 cameraPos;

    private Rigidbody player;

    private float thrust = 10;
    private float Delay = 0;

    private bool move;
    private float MAX_SPEED = .1375f;
    private float cameraHeight = 7f;
    private float mouseY;
    private float mouseWheel;
    private float cameraDistance = 10f;

    private bool Lock = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = this.gameObject.GetComponent<Rigidbody>();
        find = this; //use for singleton
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Delay -= Time.deltaTime;

        cameraPos = new Vector3(transform.position.x, transform.position.y+cameraHeight, transform.position.z) - (cameraDistance * transform.forward);
        
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.A))
            {
                player.AddForce(-transform.right*thrust, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.AddForce(transform.right*thrust, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.W))
            {
                player.AddForce(transform.forward*thrust, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.AddForce(-transform.forward*thrust, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.Space) && Delay < 0)
            {
                Delay = .25f;
                player.AddForce(transform.up*5,ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.E) && Delay < 0)
            {
                Delay = .25f;
                GameObject newGameObject = Instantiate(prefabSaveStation, transform.position, Quaternion.identity);
                newGameObject.name = "SaveStation";
            }
            if (Input.GetKey(KeyCode.Q) && Delay < 0)
            {
                if (Lock)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Lock = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Lock = true;
                }
                Delay = 1f;
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        mouseY = Input.GetAxis("Mouse Y");
        mouseWheel = -(Input.GetAxis("Mouse ScrollWheel"));

        if (Lock) 
        { 
            transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X"), 0f));

            if (mouseY > 0 && cameraHeight < 20 || mouseY < 0 && cameraHeight > 5)
            {
                cameraHeight += mouseY * .2f;
            }
            if (mouseWheel > 0 && cameraDistance < 30f || mouseWheel < 0 && cameraDistance > 5)
            {
                cameraDistance += mouseWheel * 3;
            }
        } 

        

        //if (move) transform.position = Vector3.Lerp(transform.position, target ,MAX_SPEED);
        //move towards target
    }

    public void SetMove(bool input)
    {
        move = input;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }
}
