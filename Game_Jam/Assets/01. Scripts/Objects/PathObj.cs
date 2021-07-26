using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PathData
{
    public InteractableObj obj;
    public bool isReverse;

    public PathData(InteractableObj obj, bool isReverse)
    {
        this.obj = obj;
        this.isReverse = isReverse;
    }
}

public class PathObj : InteractableObj
{
    [SerializeField] private Transform pathParent = null;
    [SerializeField] private LayerMask whatIsInteract;

    [HideInInspector]
    public List<Transform> paths = new List<Transform>();
    private List<Vector3> pos = new List<Vector3>();

    private void Start()
    {
        currentType = ObjType.PATH;

        paths = pathParent.GetComponentsInChildren<Transform>().ToList();
        paths.RemoveAt(0);

        AddPath();

        pos.Add(Vector3.up);
        pos.Add(Vector3.down);
        pos.Add(Vector3.left);
        pos.Add(Vector3.right);
    }

    private void AddPath()
    {
        if (paths.Count <= 0) return;

        Collider2D coll = null;

        coll = paths[0].gameObject.AddComponent<BoxCollider2D>();
        coll.isTrigger = true;

        coll = paths[paths.Count - 1].gameObject.AddComponent<BoxCollider2D>();
        coll.isTrigger = true;
    }

    public PathData GetPathEnd(Transform startPos)
    {
        int currentIdx = paths.FindIndex(x => x.Equals(startPos));
        int idx = 0;

        Collider2D coll = null;

        if (currentIdx.Equals(0))
        {
            idx = paths.Count - 1;
        }
        else
        {
            idx = 0;
        }

        for (int i = 0; i < pos.Count; i++)
        {
            coll = Physics2D.OverlapPoint(paths[idx].position + pos[i], whatIsInteract);

            if (coll != null)
            {
                return new PathData(coll.GetComponent<InteractableObj>(), currentIdx.Equals(0) ? false : true);
            }
        }

        return null;
    }

    protected override void OnChangeProperty(Property prop)
    {
        
    }
}
