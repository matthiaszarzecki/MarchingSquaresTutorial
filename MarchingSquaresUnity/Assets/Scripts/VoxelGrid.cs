﻿using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public class VoxelGrid : MonoBehaviour {
  public int resolution;
  public GameObject voxelPrefab;

  private bool[] voxels;
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

    SetVoxelColors();
  }

  private void SetVoxelColors() {
    for (int i = 0; i < voxels.Length; i++) {
      voxelMaterials[i].color = voxels[i] ? Color.black : Color.white;
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
        voxels[i] = stencil.Apply(x, y, voxels[i]);
      }
    }
    SetVoxelColors();
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
  }
}
