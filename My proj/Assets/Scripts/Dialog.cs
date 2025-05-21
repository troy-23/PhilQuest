using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public string speakerName;
    [TextArea(1, 3)]
    public string text;
    public Sprite speakerPortrait; // name must match usage
}

[CreateAssetMenu(menuName = "Dialog/New Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] private List<DialogLine> lines;
    public List<DialogLine> Lines => lines; // public accessor
}
