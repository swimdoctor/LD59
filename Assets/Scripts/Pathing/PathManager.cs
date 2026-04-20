using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{
    private List<GameObject> pathwayNodes = new List<GameObject>();
    private List<Vector2> pathVectors;

	private void Awake()
	{
		for(int i = 0; i < transform.childCount; i++)
        {
            pathwayNodes.Add(transform.GetChild(i).gameObject);
        }
	}

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
