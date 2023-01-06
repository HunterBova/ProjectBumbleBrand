using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{

    public List<GameObject> myNodes;

    private Vector3 myPos;

    private void Awake()
    {
        myPos = this.transform.localPosition;
        //find Nodes in range
        List<GameObject> allNodes = mapManager.find.WorldMap;

        foreach(GameObject node in allNodes)
        {
            if (node.GetComponent<Node>().DistanceFromDest(myPos) < 200)
            {
                node.GetComponent<Node>().setSaved(true);
                myNodes.Add(node);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
