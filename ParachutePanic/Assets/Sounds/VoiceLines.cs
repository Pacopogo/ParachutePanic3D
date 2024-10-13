using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VoiceLine", order = 1)]
public class VoiceLines : ScriptableObject
{
    public AudioClip[] line;
}
