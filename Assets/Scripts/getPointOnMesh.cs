using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPointOnMesh : MonoBehaviour
{
    int layerMask;

    //script on each mesh to return RaycastHits

    public RaycastHit SurfaceAlignment(Vector3 Pos)
    {
        layerMask = LayerMask.GetMask("Ground");

        Vector3 target = transform.position + Pos; //builds the target to shoot ray from

        Ray ray = new Ray(target,Vector3.down ); //builds the ray with a direction
        RaycastHit info = new RaycastHit(); //instantiates a new RaycastHit to store info

        if (Physics.Raycast(ray, out info, Mathf.Infinity, layerMask))
        {
            return info; //if the ray hits, return info
        }

        return new RaycastHit(); //otherwise return a new RaycastHit
    }
}
