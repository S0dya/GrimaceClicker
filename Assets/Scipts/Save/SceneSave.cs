using System.Collections.Generic;

[System.Serializable]
public class SceneSave
{
    public Dictionary<string, int> intDictionary;
    public Dictionary<string, float> floatDictionary;
    public Dictionary<string, float[]> floatArrayDictionary;
    public Dictionary<string, bool> boolDictionary;
    public Dictionary<string, bool[]> boolArrayDictionary;
}
