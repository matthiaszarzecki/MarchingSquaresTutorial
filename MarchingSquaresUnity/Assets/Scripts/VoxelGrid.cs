using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public class VoxelGrid : MonoBehaviour {
  public int resolution;
  public GameObject voxelPrefab;

  private Voxel[] voxels;
  private float voxelSize;

  private Material[] voxelMaterials;

  private Mesh mesh;

  private List<Vector3> vertices;
  private List<int> triangles;

  public void Initialize(int resolution, float size) {
    this.resolution = resolution;
    voxelSize = size / resolution;
    voxels = new bool[resolution * resolution];

    voxelMaterials = new Material[voxels.Length];

    for (int i = 0, y = 0; y < resolution; y++) {
      for (int x = 0; x < resolution; x++, i++) {
        CreateVoxel(i, x, y);
      }
    }

    GetComponent<MeshFilter>().mesh = mesh = new Mesh();
    mesh.name = "VoxelGrid Mesh";
    vertices = new List<Vector3>();
    triangles = new List<int>();
    Refresh();
  }

  private void SetVoxelColors() {
    for (int i = 0; i < voxels.Length; i++) {
      voxelMaterials[i].color = voxels[i].state ? Color.black : Color.white;
    }
  }

  public void Apply(VoxelStencil stencil) {
    int xStart = stencil.XStart;
    if (xStart < 0) {
      xStart = 0;
    }
    int xEnd = stencil.XEnd;
    if (xEnd >= resolution) {
      xEnd = resolution - 1;
    }
    int yStart = stencil.YStart;
    if (yStart < 0) {
      yStart = 0;
    }
    int yEnd = stencil.YEnd;
    if (yEnd >= resolution) {
      yEnd = resolution - 1;
    }

    for (int y = yStart; y <= yEnd; y++) {
      int i = y * resolution + xStart;
      for (int x = xStart; x <= xEnd; x++, i++) {
        voxels[i].state = stencil.Apply(x, y, voxels[i].state);
      }
    }

    Refresh();
  }

  private void Refresh() {
    SetVoxelColors();
    Triangulate();
  }

  private void Triangulate() {
    vertices.Clear();
    triangles.Clear();
    mesh.Clear();

    TriangulateCellRows();

    mesh.vertices = vertices.ToArray();
    mesh.triangles = triangles.ToArray();
  }

  private void CreateVoxel(int i, int x, int y) {
    GameObject o = Instantiate(voxelPrefab) as GameObject;
    // Make new objects children of VoxelGrid object
    o.transform.parent = transform;
    o.transform.localPosition = new Vector3((x + 0.5f) * voxelSize, (y + 0.5f) * voxelSize, -0.01f);
    o.transform.localScale = Vector3.one * voxelSize * 0.1f;

    //Vector3 voxelScale = Vector3.one * voxelSize * 0.9f;
    //o.transform.localScale = voxelScale;

    voxelMaterials[i] = o.GetComponent<MeshRenderer>().material;

    voxels[i] = new Voxel(x, y, voxelSize);
  }
}
