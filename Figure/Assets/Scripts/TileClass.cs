using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TileClass{
	public List<Tile> tiles;
	public Bin bins;
}

[System.Serializable]

public class Tile{
	public float x;
	public float y;
	public float c1;
	public float c2;
	public float c3;
	public float c4;
	public float c5;
}

[System.Serializable]

public class Bin{
	public int c1;
	public int c2;
	public int c3;
	public int c4;
}
