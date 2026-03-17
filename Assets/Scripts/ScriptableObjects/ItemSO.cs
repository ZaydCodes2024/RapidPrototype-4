using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public bool isStackable = true;

}
