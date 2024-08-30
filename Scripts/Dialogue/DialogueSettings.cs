using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="New Dialogue", menuName ="New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence; 

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
}

// IF UNITY EDITOR SERVE PARA FAZER O BOTÃO FUNCIONAR APENAS SE ESTIVER COM A UNITY ABERTA, SE NÃO, BUGA
#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI() // esse override é para modificar
    {
        DrawDefaultInspector();

        DialogueSettings ds = (DialogueSettings)target;

        Languages l = new Languages();
        l.portuguese = ds.sentence;

        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.sentence = l;

        if(GUILayout.Button("Create Dialogue"))
        {
            if(ds.sentence != "")
            {
                ds.dialogues.Add(s);
                ds.speakerSprite = null;
                ds.sentence = "";

            }
        }
    }
}
#endif