using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TreeSort : MonoBehaviour
{
    public bool sortTrees;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (sortTrees)
        {
            sortTrees = false;

            foreach (SpriteRenderer renderer in FindObjectsOfType<SpriteRenderer>())
            {
                if (renderer?.sprite?.name == "tree1" || renderer?.sprite?.name == "barrels")
                {
                    Vector3 copy = renderer.transform.position;
                    copy.z = -1 + copy.y/100;

                    renderer.transform.position = copy;
                }
            }
        }
    }
}
