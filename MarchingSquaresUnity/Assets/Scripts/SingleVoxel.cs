using UnityEngine;
using System;

[Serializable]
// A single Voxel Element.
public class SingleVoxel {
  public bool state;
  public Vector2 position;
  public Vector2 xEdgePosition;
  public Vector2 yEdgePosition;

  public SingleVoxel(int x, int y, float size) {
    position.x = (x + 0.5f) * size;
    position.y = (y + 0.5f) * size;

    xEdgePosition = position;
    xEdgePosition.x += size * 0.5f;
    yEdgePosition = position;
    yEdgePosition.y += size * 0.5f;
  }

  // Empty Constructor for Dummy Voxels.
  public SingleVoxel() { }

  public void BecomeXDummyOf(SingleVoxel voxel, float offset) {
    state = voxel.state;
    position = voxel.position;
    xEdgePosition = voxel.xEdgePosition;
    yEdgePosition = voxel.yEdgePosition;
    position.x += offset;
    xEdgePosition.x += offset;
    yEdgePosition.x += offset;
  }

  public void BecomeYDummyOf(SingleVoxel voxel, float offset) {
    state = voxel.state;
    position = voxel.position;
    xEdgePosition = voxel.xEdgePosition;
    yEdgePosition = voxel.yEdgePosition;
    position.y += offset;
    xEdgePosition.y += offset;
    yEdgePosition.y += offset;
  }

  public void BecomeXYDummyOf(SingleVoxel voxel, float offset) {
    state = voxel.state;
    position = voxel.position;
    xEdgePosition = voxel.xEdgePosition;
    yEdgePosition = voxel.yEdgePosition;
    position.x += offset;
    position.y += offset;
    xEdgePosition.x += offset;
    xEdgePosition.y += offset;
    yEdgePosition.x += offset;
    yEdgePosition.y += offset;
  }
}