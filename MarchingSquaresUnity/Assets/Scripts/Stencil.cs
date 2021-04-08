﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Stencil that selects Voxels in a square
// shape centered around the cursor.
public class Stencil {
  protected bool fillType;
  protected int centerX;
  protected int centerY;
  protected int radius;

  public virtual void Initialize(bool fillType, int radius) {
    this.fillType = fillType;
    this.radius = radius;
  }

  public virtual void SetCenter(int x, int y) {
    centerX = x;
    centerY = y;
  }

  public virtual bool Apply(int x, int y, bool voxel) {
    return fillType;
  }

  public int XStart {
    get {
      return centerX - radius;
    }
  }

  public int XEnd {
    get {
      return centerX + radius;
    }
  }

  public int YStart {
    get {
      return centerY - radius;
    }
  }

  public int YEnd {
    get {
      return centerY + radius;
    }
  }
}
