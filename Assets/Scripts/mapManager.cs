using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public static mapManager find;

    public GameObject prefabNode;

    private Vector3 playerPos;

    public List<GameObject> WorldMap;

    private float buffer = .1f;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        find = this; //use for singleton
    }

    // Update is called once per frame
    void Update()
    {
        if (buffer > 0)
        {
            buffer -= Time.deltaTime;
        }
        else
        {
            if (WorldMap.Count == 0)
            {
                instanatiateNodeAtPos(new Vector3(0f, 0f, 0f));
            }
        }
    }

    public bool checkForPosition(Vector3 n)
    {
        //checks WorldMap for any node at positon n

        bool r = false;

        foreach (GameObject node in WorldMap)
        {
            if (Vector3.Distance(node.GetComponent<Node>().myPos,n) < 1)
            {
                r = true;
            }
        }

        return r;
    }

    public Vector3[] getNodesVertices(Vector3 pos,int direction)
    {
        //Old code that used to get the vertices of a node at pos in one of 4 directions
        //
        //could probably be reused in wave function collapse

        Vector3[] verts = new Vector3[10];

        foreach (GameObject node in WorldMap)
        {
            if (Vector3.Distance(node.GetComponent<Node>().myPos, pos) < 1)
            {
                switch(direction)
                {
                    case 1:
                        //verts = node.GetComponent<MeshTest>().getNorthVertices();
                        break;
                    case 2:
                        //verts = node.GetComponent<MeshTest>().getWestVertices();
                        break;
                    case 3:
                        //verts = node.GetComponent<MeshTest>().getEastVertices();
                        break;
                    case 4:
                        //verts = node.GetComponent<MeshTest>().getSouthVertices();
                        break;
                }
            }
        }
        return verts;
    }

    public void instanatiateNodeAtPos(Vector3 n)
    {
        //Called by the script Node to spawn neighbors.
        //
        //n uses the myPos +/- scaleOffset in the X and Z directions

        GameObject newGameObject = Instantiate(prefabNode,n,Quaternion.identity); //instantiate prefab at n with no rotation

        newGameObject.transform.parent = transform; //set parent to mapTransponder gameObject

        addNodeToMap(newGameObject); 

        newGameObject.name = newGameObject.GetComponent<Node>().myPos.x + " | " + newGameObject.GetComponent<Node>().myPos.z; //renames node for reference

    }

    

    public void addNodeToMap(GameObject node)
    {
        WorldMap.Add(node);
    }

    public void removeNodeFromList(GameObject node)
    {
        WorldMap.Remove(node);
        Destroy(node);
    }
}
