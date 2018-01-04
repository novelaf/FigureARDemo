using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Media
{
	public List<ElliotImage> images;
}

[System.Serializable]
public class MultipleChoiceData
{
	public Media media;
	public List<string> options;
	public int suggestion;
}

[System.Serializable]
public class ElliotImage
{
	public string media_id;
	public string source;
	public string caption;
	public string url;
}

[System.Serializable]
public class Suggestion
{
	public int x;
	public int y;
	public int width;
	public int height;
}

[System.Serializable]
public class BoundingBoxData
{
	public ElliotImage image;
	public Suggestion suggestion;
}

[System.Serializable]
public class Field
{
	public string name;
	public string label;
	public string type;
	public MultipleChoiceData multipleChoiceData;
	public BoundingBoxData boundingBoxData;
	public CustomData customData;
}

[System.Serializable]
public class CustomData
{
	public float table_x;
	public float table_y;
}

[System.Serializable]
public class Form
{
	public List<Field> fields;
}

[System.Serializable]
public class BoundingBoxValue
{
	public int x;
	public int y;
	public int width;
	public int height;
}

[System.Serializable]
public class FieldValue
{
	public string name;
	public int multipleChoiceValue;
	public BoundingBoxValue boundingBoxValue;
}

[System.Serializable]
public class Response
{
	public int id;
	public int requestId;
	public List<FieldValue> fieldValues;
}

[System.Serializable]
public class ElliotIntervention
{
	public int id;
	public string name;
	public Form form;
	public int fleetId;
	public int severity;
	public DateTime createdAt;
	public List<Response> responses;
}

[System.Serializable]
public class ElliotInterventions{
	public List<ElliotIntervention> interventions;
}
