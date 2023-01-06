using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Controller player;

    // Start is called before the first frame update
    void Start()
    {
        player = Controller.find;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.cameraPos, .4f);
        transform.LookAt(player.transform, player.transform.forward);  
    }
}
