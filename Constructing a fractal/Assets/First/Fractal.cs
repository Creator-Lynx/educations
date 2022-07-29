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
    void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (depth < maxDepth)
        {
            StartCoroutine(Init());
            //new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.back);
            //new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right);
            //new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.forward);
            //new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.left);
        }

    }

    void Initialize(Fractal parent, Vector3 direction, Quaternion rotation)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.rotation = parent.transform.rotation * rotation;
        transform.localPosition = (0.5f + 0.5f * childScale) * direction;
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.4f);
        new GameObject("Fractal Child").AddComponent<Fractal>().
            Initialize(this, Vector3.back, Quaternion.identity);
        new GameObject("Fractal Child").AddComponent<Fractal>().
            Initialize(this, Vector3.right, Quaternion.AngleAxis(90f, Vector3.down));
        new GameObject("Fractal Child").AddComponent<Fractal>().
            Initialize(this, Vector3.left, Quaternion.AngleAxis(-90f, Vector3.down));

        if (depth == 0)
            new GameObject("Fractal Child").AddComponent<Fractal>().
                Initialize(this, Vector3.forward, Quaternion.AngleAxis(180f, Vector3.down));
    }

}