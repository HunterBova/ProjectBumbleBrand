using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Vector3 playerPos;

    public Vector3 myPos;

    private MeshRenderer myRender;
    private bool rendered;
    private MeshRenderer[] myRenders;

    private float frameBuffer;
    private float distance;

    private float scaleOffset = 32f;

    private bool saved;

    public List<GameObject> mySetPieces;
    private bool setPieceRendered;

    //Node mostly cares about the distance to the player
    //
    //It keeps track of the player and triggers events on the node

    private void Awake()
    {
        saved = false;
        distance = 0;
        frameBuffer = 5;
        rendered = true;
        myPos = this.transform.localPosition;
        myRender = this.GetComponent<MeshRenderer>();
        myRenders = this.GetComponentsInChildren<MeshRenderer>();

        mySetPieces = new List<GameObject>();
        setPieceRendered = false;

        if (myRender != null) myRender.enabled = false;

        else
        {
            foreach (MeshRenderer r in myRenders)
            {
                r.enabled = false;
            }
        }
        

        rendered = false;

        //StartCoroutine(awakeDelay());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        //mapManager.find.removeNodeFromList(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        frameBuffer -= 1;

        if (!rendered && frameBuffer < 0) 
        {
            playerPos = Controller.find.getPosition();

            distance = Vector3.Distance(playerPos, myPos);

            if (distance < 100)
            {
                rendered = true;

                //GetComponent<MeshTest>().renderVertices();

                if (myRender != null) myRender.enabled = true;

                else
                {
                    foreach (MeshRenderer r in myRenders)
                    {
                        r.enabled = true;
                    }
                }
               

                //include logic to call for random
                
                if (!mapManager.find.checkForPosition(myPos + new Vector3(scaleOffset, 0f, 0f)))
                {
                    mapManager.find.instanatiateNodeAtPos(myPos + new Vector3(scaleOffset, 0f, 0f));
                }

                if (!mapManager.find.checkForPosition(myPos + new Vector3(-(scaleOffset), 0f, 0f)))
                {
                    mapManager.find.instanatiateNodeAtPos(myPos + new Vector3(-(scaleOffset), 0f, 0f));
                }

                if (!mapManager.find.checkForPosition(myPos + new Vector3(0f, 0f, scaleOffset)))
                {
                    mapManager.find.instanatiateNodeAtPos(myPos + new Vector3(0f, 0f, scaleOffset));
                }

                if (!mapManager.find.checkForPosition(myPos + new Vector3(0f, 0f, -(scaleOffset))))
                {
                    mapManager.find.instanatiateNodeAtPos(myPos + new Vector3(0f, 0f, -(scaleOffset)));
                }

                if (mySetPieces.Count < 1)
                {
                    GenerateSetPieces();
                    setPieceRendered = false;
                    foreach (GameObject n in mySetPieces)
                    {
                        n.SetActive(false);
                    }
                }
                
            }
            else if (distance > 200)
            {
                TearDownSetPieces();
                mapManager.find.removeNodeFromList(this.gameObject);
            }
            else
            {
                frameBuffer = distance;
            }
        }

        if (rendered && frameBuffer < 0)
        {
            playerPos = Controller.find.getPosition();

            distance = DistanceFromDest(playerPos);

            frameBuffer = distance;

            if (distance < 70 && !setPieceRendered)
            {
                setPieceRendered = true;
                foreach (GameObject n in mySetPieces)
                {
                    n.SetActive(true);
                }
            }

            if (distance > 70 && setPieceRendered)
            {
                setPieceRendered = false;
                foreach (GameObject n in mySetPieces)
                {
                    n.SetActive(false);
                }
            }

            if (distance > 110 && !saved)
            {
                rendered = false;

                if (myRender != null) myRender.enabled = false;

                else
                {
                    foreach (MeshRenderer r in myRenders)
                    {
                        r.enabled = false;
                    }
                }
                
            }

            if (distance > 200 && !saved)
            {
                mapManager.find.removeNodeFromList(this.gameObject);
            }
        }
    }

    public float DistanceFromDest(Vector3 Dest)
    {
        return Vector3.Distance(Dest,myPos);
    }

    public void setSaved(bool set)
    {
        saved = set;
    }

    private void GenerateSetPieces()
    {
        mySetPieces = SetPieceSpawner.find.instantiateSetPiecesAtNode(this.gameObject);
    }

    private void TearDownSetPieces()
    {
        foreach (GameObject n in mySetPieces)
        {
            Destroy(n);
        }
    }

    IEnumerator awakeDelay()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.name = "" + mapManager.find.WorldMap.Count;
        mapManager.find.addNodeToMap(this.gameObject);
        rendered = false;
    }
}
