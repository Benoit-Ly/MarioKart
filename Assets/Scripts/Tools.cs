using UnityEngine;
using System.Collections;

public static class Tools {

    public static GameObject getObjectRoot(GameObject target)
    {
        if (target == null)
            return null;

        return target.transform.root.transform.gameObject;
    }

    public static int compareSiblingIndex(Transform t1, Transform t2)
    {
        if (t1 == null || t2 == null)
            return 0;

        int index_t1 = t1.GetSiblingIndex();
        int index_t2 = t2.GetSiblingIndex();

        return index_t1.CompareTo(index_t2);
    }
}
