using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyObjectMesh : MonoBehaviour
{
    public Mesh FirstTransformMeshFilterer;
    public Material FirstTransformMeshRenderer;
    public Mesh SecondTransformMeshFilterer;
    public Material SecondTransformMeshRenderer;
    public Mesh ThirdTransformMeshFilterer;
    public Material ThirdTransformMeshRenderer;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void SetToFirstMesh()
    {
        GetComponent<MeshFilter>().mesh = FirstTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = FirstTransformMeshRenderer;
    }

    public void SetToSecondMesh()
    {
        GetComponent<MeshFilter>().mesh = SecondTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = SecondTransformMeshRenderer;
    }

    public void SetToThirdMesh()
    {
        GetComponent<MeshFilter>().mesh = ThirdTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = ThirdTransformMeshRenderer;
    }

}
