using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need a mesh filter for this example
[RequireComponent(typeof(MeshFilter))]
public class GenerateGeometry : MonoBehaviour
{
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
            new Vector3(0.5f,1.0f,0.5f) //4
        };

        //Normals and other elements are options but will be required
        //for lighting and texturing to work 
        Vector3[] normals =
        {
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
            0, 4, 1, //face a
            1, 4, 2, //face b
            2, 4 ,3, //face c
            3, 4, 0  //face d
        };

        // piramid vertices 
        //Indices, called triangles in Unity, these are indices into the Vertex array above
        //int[] triangles = {
        //    0, 1, 2, //left triangle of base
        //    2, 3, 0, //right triangle of base
        //    0, 4, 1, //face a
        //    1, 4, 2, //face b
        //    2, 4 ,3, //face c
        //    3, 4, 0  //face d
        //};

        //Assign all the values in the mesh
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;

    }
}
