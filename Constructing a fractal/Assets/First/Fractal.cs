using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{

    [SerializeField]
    Mesh mesh;
    [SerializeField]
    Material material;

    [SerializeField]
    int maxDepth;
    int depth;

    [SerializeField]
    float childScale = 0.7f;

    static Vector3[] childDirections =
    {
        Vector3.back,
        Vector3.right,
        Vector3.left,
        Vector3.up,
        Vector3.down,
        Vector3.forward
    };
    static Quaternion[] childRotations =
    {
        Quaternion.identity,
        Quaternion.AngleAxis(90f, Vector3.down),
        Quaternion.AngleAxis(-90f, Vector3.down),
        Quaternion.AngleAxis(90, Vector3.right),
        Quaternion.AngleAxis(-90, Vector3.right),
        Quaternion.AngleAxis(180f, Vector3.down)
    };
    void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }

    }

    void Initialize(Fractal parent, int childId)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.rotation = parent.transform.rotation * childRotations[childId];
        transform.localPosition = (0.5f + 0.5f * childScale) * childDirections[childId];
    }

    IEnumerator CreateChildren()
    {
        for (int i = 0; i < childDirections.Length - 1; i++)
        {
            yield return new WaitForSeconds(0.4f);
            new GameObject("Fractal Child").AddComponent<Fractal>().
                Initialize(this, i);
        }


        if (depth == 0)
        {
            yield return new WaitForSeconds(0.2f);
            new GameObject("Fractal Child").AddComponent<Fractal>().
                Initialize(this, childDirections.Length - 1);
        }
    }

}
