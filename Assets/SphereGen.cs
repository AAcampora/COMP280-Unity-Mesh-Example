using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class SphereGen : MonoBehaviour
{
    public int sphereSub = 0;
    public float sphereRad = 1.0f;

    //when called, gen the sphere through the mesh filter 
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = SphereGen.Create(sphereSub, sphereRad);
    }

    public static Mesh Create(int subdivisions, float radius)
    {
        //create tge resolution of the sphere 
       int resolution = 1 << subdivisions;

        //Vertices 
       Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1) * 4 -
             (resolution * 2 - 1) * 3];

        //triangles 
       int[] triangles = new int[(1 << (subdivisions * 2 + 3)) * 3];

        //create the octahedron mesh 
        CreateOctahedron(vertices, triangles, resolution);

        //clamp the amount of sphere divisions 
        if (subdivisions <0)
        {
            subdivisions = 0;
            Debug.LogWarning("Octahedron Sphere set to minimum, which is 0");
        }
        else if (subdivisions > 6 )
        {
            subdivisions = 6;
            Debug.LogWarning("Octahedron set to maximum, which is 6");
        }

        //normalize the vectors 
        Vector3[] normals = new Vector3[vertices.Length];
        Normalize(vertices, normals);

        //set the number of vertices by the radius of the sphere 
        if (radius != 1f)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= radius;
            }
        }

        //generate the new mesh
        Mesh mesh = new Mesh();
        mesh.name = "Octahedron Sphere";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        return mesh;
    }
    
    //normalize the vectors of our sphere 
    private static void Normalize(Vector3[] vertices, Vector3[] normals)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = vertices[i] = vertices[i].normalized;
        }
    }

    //creates the base of our circle
    private static void CreateOctahedron(Vector3[] vertices, int[] triangles, int resolution)
    {
        //track the number of vertices, store them in v and increase by the vertex size 
        int v = 0, vBottom = 0, t = 0;

        for (int i = 0; i < 4; i++)
        {
            vertices[v++] = Vector3.down;
        }


        //create the lower section of the sphere 
        for (int i = 1; i <= resolution; i++)
        {
            float progress = (float)i / resolution;
            Vector3 from, to;
            vertices[v++] = to = Vector3.Lerp(Vector3.down, Vector3.forward, progress);
            for (int d = 0; d < 4; d++)
            {
                from = to;
                to = Vector3.Lerp(Vector3.down, directions[d], progress);
                t = CreateLowerStrip(i, v, vBottom, t, triangles);
                v = CreateVertexLine(from, to, i, v, vertices);
                vBottom += i > 1 ? (i - 1) : 1;
            }
            vBottom = v - 1 - i * 4;
        }

        //create the upper part of the sphere 
        for (int i = resolution - 1; i >= 1; i--)
        {
            float progress = (float)i / resolution;
            Vector3 from, to;
            vertices[v++] = to = Vector3.Lerp(Vector3.up, Vector3.forward, progress);
            for (int d = 0; d < 4; d++)
            {
                from = to;
                to = Vector3.Lerp(Vector3.up, directions[d], progress);
                t = CreateUpperStrip(i, v, vBottom, t, triangles);
                v = CreateVertexLine(from, to, i, v, vertices);
                vBottom += i + 1;
            }
            vBottom = v - 1 - i * 4;
        }

        //update the triangles of the sphere which have been subdivied 
        for (int i = 0; i < 4; i++)
        {
            triangles[t++] = vBottom;
            triangles[t++] = v;
            triangles[t++] = ++vBottom;
            vertices[v++] = Vector3.up;
        }
    }

    //create a new vertex line everytime it's called
    private static int CreateVertexLine(Vector3 from, Vector3 to, int steps, int v, Vector3[] vertices)
    {
        for (int i = 1; i <= steps; i++)
        {
            vertices[v++] = Vector3.Lerp(from, to, (float)i / steps);
        }
        return v;
    }

    //create the triangles for the lower strip
    private static int CreateLowerStrip(int steps, int vTop, int vBottom, int t, int[] triangles)
    {
        for (int i = 1; i < steps; i++)
        {
            triangles[t++] = vBottom;
            triangles[t++] = vTop - 1;
            triangles[t++] = vTop;

            triangles[t++] = vBottom++;
            triangles[t++] = vTop++;
            triangles[t++] = vBottom;
        }

        triangles[t++] = vBottom;
        triangles[t++] = vTop - 1;
        triangles[t++] = vTop;
        return t;
    }

    //create the triangles for the upper strip
    private static int CreateUpperStrip(int steps, int vTop, int vBottom, int t, int[] triangles)
    {
        triangles[t++] = vBottom;
        triangles[t++] = vTop - 1;
        triangles[t++] = ++vBottom;
        for (int i = 1; i <= steps; i++)
        {
            triangles[t++] = vTop - 1;
            triangles[t++] = vTop;
            triangles[t++] = vBottom;

            triangles[t++] = vBottom;
            triangles[t++] = vTop++;
            triangles[t++] = ++vBottom;
        }
        return t;
    }

    //as the sphere is split in 4 parts, we have to set it's directions
    private static Vector3[] directions =
    {
        Vector3.left,
        Vector3.back,
        Vector3.right,
        Vector3.forward
    };
}

