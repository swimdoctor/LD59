using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{
    public List<GameObject> pathwayNodes;
    private List<Vector2> pathVectors;
    void Start()
    {
        pathVectors = new List<Vector2>();
        foreach(GameObject node in pathwayNodes)
        {
            pathVectors.Add(node.transform.position);
        }
        print(pathVectors);
    }

    public List<Vector2> getPathVectors()
    {
        return pathVectors;
    }

    public Vector2 getStartingPosition()
    {
        return pathVectors[0];
    }
}
