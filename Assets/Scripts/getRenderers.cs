using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getRenderers : MonoBehaviour
{
    private MeshRenderer[] myRenders;
    private void Awake()
    {
        myRenders = this.GetComponentsInChildren<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableRenders()
    {
        for (int i = 0; i < myRenders.Length;i++)
        {
            myRenders[i].enabled = false;
        }
    }
    
    public void enableRenders()
    {
        for (int i = 0; i < myRenders.Length; i++)
        {
            myRenders[i].enabled = true;
        }
    }
}
