using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyObjectMesh : MonoBehaviour
{
    public Mesh BallTransformMeshFilterer;
    public Material BallTransformMeshRenderer;
    public Mesh Ball1TransformMeshFilterer;
    public Material Ball1TransformMeshRenderer;
    public Mesh Ball2TransformMeshFilterer;
    public Material Ball2TransformMeshRenderer;
    public Mesh BellTransformMeshFilterer;
    public Material BellTransformMeshRenderer;
    public Mesh PackageTransformMeshFilterer;
    public Material PackageTransformMeshRenderer;
    public Mesh RocketTransformMeshFilterer;
    public Material RocketTransformMeshRenderer;
    public Mesh SexDrawingTransformMeshFilterer;
    public Material SexDrawingTransformMeshRenderer;
    public Mesh ToyCarTransformMeshFilterer;
    public Material ToyCarTransformMeshRenderer;
    public Mesh DadTransformMeshFilterer;
    public Material DadTransformMeshRenderer;
    public Mesh MomTransformMeshFilterer;
    public Material MomTransformMeshRenderer;

    public Mesh Porcelain1TransformMeshFilterer;
    public Material Porcelain1TransformMeshRenderer;
    public Mesh Porcelain2TransformMeshFilterer;
    public Material Porcelain2TransformMeshRenderer;
    public Mesh Porcelain3TransformMeshFilterer;
    public Material Porcelain3TransformMeshRenderer;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void SetToBallMesh()
    {
        GetComponent<MeshFilter>().mesh = BallTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = BallTransformMeshRenderer;
    }
    public void SetToBall1Mesh()
    {
        GetComponent<MeshFilter>().mesh = Ball1TransformMeshFilterer;
        GetComponent<MeshRenderer>().material = Ball1TransformMeshRenderer;
    }
    public void SetToBall2Mesh()
    {
        GetComponent<MeshFilter>().mesh = Ball2TransformMeshFilterer;
        GetComponent<MeshRenderer>().material = Ball2TransformMeshRenderer;
    }

    public void SetToBellMesh()
    {
        GetComponent<MeshFilter>().mesh = BellTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = BellTransformMeshRenderer;
    }

    public void SetToPackageMesh()
    {
        GetComponent<MeshFilter>().mesh = PackageTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = PackageTransformMeshRenderer;
    }

    public void SetToRocketMesh()
    {
        GetComponent<MeshFilter>().mesh = RocketTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = RocketTransformMeshRenderer;
    }

    public void SetToSexDrawingMesh()
    {
        GetComponent<MeshFilter>().mesh = SexDrawingTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = SexDrawingTransformMeshRenderer;
    }

    public void SetToToyCarMesh()
    {
        GetComponent<MeshFilter>().mesh = ToyCarTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = ToyCarTransformMeshRenderer;
    }

    public void SetToMomMesh()
    {
        GetComponent<MeshFilter>().mesh = MomTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = MomTransformMeshRenderer;
    }

    public void SetToDadMesh()
    {
        GetComponent<MeshFilter>().mesh = DadTransformMeshFilterer;
        GetComponent<MeshRenderer>().material = DadTransformMeshRenderer;
    }

    public void SetToPorcelain1Mesh()
    {
        GetComponent<MeshFilter>().mesh = Porcelain1TransformMeshFilterer;
        GetComponent<MeshRenderer>().material = Porcelain1TransformMeshRenderer;
    }

    public void SetToPorcelain2Mesh()
    {
        GetComponent<MeshFilter>().mesh = Porcelain2TransformMeshFilterer;
        GetComponent<MeshRenderer>().material = Porcelain2TransformMeshRenderer;
    }

    public void SetToPorcelain3Mesh()
    {
        GetComponent<MeshFilter>().mesh = Porcelain3TransformMeshFilterer;
        GetComponent<MeshRenderer>().material = Porcelain3TransformMeshRenderer;
    }
}
