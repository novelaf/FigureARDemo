using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TrajectoryClass{
	public List<Point> points;
	public Pose table_t_robot;
	public bool object_attached;

}

[System.Serializable]

public class Point{
	public Pose pose;
	public float[] positions = new float[6];
	public Time_From_Start time_from_start;
}

[System.Serializable]

public class Pose{
	public Position position;
	public Orientation orientation;
}

[System.Serializable]

public class Position{
	public float x;
	public float y;
	public float z;
}

[System.Serializable]

public class Orientation{
	public float x;
	public float y;
	public float z;
	public float w;
}

[System.Serializable]

public class Time_From_Start{
	public int secs;
	public int nsecs;
}
