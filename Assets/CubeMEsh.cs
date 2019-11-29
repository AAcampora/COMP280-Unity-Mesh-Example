using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMEsh : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        //Create a new Mesh
        Mesh mesh = new Mesh();
        mesh.name = "GeneratedMesh";

        //Grab the mesh filter and assign the newly created mesh
        GetComponent<MeshFilter>().mesh = mesh;

        //Create vertices, we must have these
        Vector3[] vertices =
        {
            new Vector3(0.0f,0.0f,0.0f),//0
            new Vector3(1.0f,0.0f,0.0f),//1
            new Vector3(1.0f,0.0f,1.0f),//2
            new Vector3(0.0f,0.0f,1.0f),//3
            new Vector3(0.0f,1.0f,0.0f),//4
            new Vector3(1.0f,1.0f,0.0f),//5
            new Vector3(1.0f,1.0f,1.0f),//6
            new Vector3(0.0f,1.0f,1.0f),//7

        };

        //Normals and other elements are options but will be required
        //for lighting and texturing to work 
        Vector3[] normals =
        {
            new Vector3(0.0f,0.0f,-1.0f),
            new Vector3(0.0f,0.0f,-1.0f),
            new Vector3(0.0f,0.0f,-1.0f),
            new Vector3(0.0f,0.0f,-1.0f),
            new Vector3(0.0f,0.0f,-1.0f),
            new Vector3(0.0f,0.0f,-1.0f),
            new Vector3(0.0f,0.0f,-1.0f),
            new Vector3(0.0f,0.0f,-1.0f)
        };

        //Indices, called triangles in Unity, these are indices into the Vertex array above
        int[] triangles = {
            0, 1, 2, //left triangle of base
            2, 3, 0, //right triangle of base
            1, 0, 4, // a 1
            4, 5, 1, //a 2
            2, 1, 5, // b 1
            5, 6, 2, // b 2
            3, 2, 6, // c 1
            6, 7, 3, // c 2
            0, 3, 7, // d 1
            7, 4, 0, // d 2
            5, 4, 6, // top 1
            7, 6, 4  //top 2
          
        };


        //Assign all the values in the mesh
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;

    }

}