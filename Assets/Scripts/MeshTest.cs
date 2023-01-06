using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    //MeshTest uses the Texture2D noiseMap to sample a Vector2 position
    //using the X and Z of the Nodes position +/- 16
    //
    //The node 0,0 will sample 256 pixels ranging from -8,-8 to 8, 8

    public List<Texture2D> myNoises;
    private Texture2D myNoise;

    public Texture2D noiseMap;
    private Color noiseColor;

    private Mesh mesh;
    private Vector3[] vertices;

    private float jagedididyAspect = .04f;//harshness of terrain jaged-ness

    private Vector3 myPos;
    private MeshCollider myCollider;

    int buffer = 0;
    int co = 0;
    bool bo = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        myPos = transform.position;
        myCollider = GetComponent<MeshCollider>();

        transform.Rotate(new Vector3(-90f,0f,0f));

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        float vertX;
        float vertY;

        //////////////////////////////////////////////////////////////////////

        //myNoise = myNoises[Random.Range(0, myNoises.Count)];

        for (int i = 0; i < vertices.Length; i++)
        {
            vertX = vertices[i].x;
            vertY = vertices[i].y;//not needed, just helps make the next line smaller

            noiseColor = noiseMap.GetPixel(Mathf.RoundToInt((800f * vertX) + (myPos.x / 2)), Mathf.RoundToInt((800f * vertY) - (myPos.z / 2)));
            //multiply vertX and vertY by 800f to get a minimum and maximum bounds of 8
            //divide myPos by 2 because tiles are positioned 32 meters apart but only sample 16x16 pixels

            vertices[i].z = ((noiseColor.grayscale) * jagedididyAspect) - .04f; 
            //shifts height of vertice, grayscale returns number between 0-1
            //and the -.04f helps shift terrain below world position Z 0


            //noiseColor = myNoise.GetPixel(Mathf.RoundToInt((800f * vertX)+ 8), Mathf.RoundToInt((800f * vertY)+8));
            //vertices[i].z += noiseColor.grayscale * jagedididyAspect * .1f;
        }

        ///////////////////////////////////////////////////////////////////////
        //All logic that currently bakes the noise map into the vertices

        mesh.vertices = vertices;

        mesh.RecalculateBounds();

        myCollider.sharedMesh = mesh;

        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        //try to keep nothing here   
    }
}
