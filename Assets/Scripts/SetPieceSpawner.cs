using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPieceSpawner : MonoBehaviour
{
    public List<GameObject> allSetPieces;

    public static SetPieceSpawner find;

    // Start is called before the first frame update
    private void Start()
    {
        find = this; //use for singleton
    }

    public List<GameObject> instantiateSetPiecesAtNode(GameObject node)
    {
        List<GameObject> setPieces = new List<GameObject>();

        for (int i = 0; i < 16; i++)
        {

            for (int j = 0; j < 16; j++)
            {
                /////////////////////////////////////////////////////////////////////////
                
                if (Random.Range(1, 10) == 4)//chance to spawn (1/10) / 256 == 25.
                {
                    int index = 0;
                    if ( i % 3 == 0)
                    {
                        index = 1;
                    }
                    if ( i % 3 == 1)
                    {
                        index = 2;
                    }
                    Debug.Log(index);

                    float x = (i - (16 - i));
                    float y = 0f;
                    float z = (j - (16 - j));

                    RaycastHit info = node.GetComponent<getPointOnMesh>().SurfaceAlignment(new Vector3(x, y, z));
                    if (info.point != Vector3.zero)
                    {
                        GameObject setPiece = Instantiate(allSetPieces[index], info.point, Quaternion.FromToRotation(Vector3.up, info.normal));

                        setPiece.transform.parent = node.transform;

                        float scale = 1;
                        if (index != 2)
                        {
                            scale = Random.Range(.01f, .02f);
                            setPiece.transform.localScale = new Vector3(scale, scale, scale);
                        }
                        else
                        {
                            scale = Random.Range(.002f, .004f);
                            setPiece.transform.localScale = new Vector3(scale, scale, scale);
                        }

                        setPieces.Add(setPiece);
                    }
                    
                }
                ///////////////////////////////////////////////////////////////////////////
                //
                //This whole code section can be extracted out and refined further
                //
                //currently it scans the bounds of the node using i and j
                //but additional logic will need to be added and much further
                //expanded as the amount of different setpieces that need selected,
                //generated, scaled, colored, painted, printed, constructed, reconstituted,
                //and dehydrated will be changing
            }
        }

        return setPieces;
    }
}
