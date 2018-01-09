using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject {

    public int characterType;

    // TODO: Add a case for [ShowOnly] displaying GameObjects
    public GameObject instance;

    public int id
    {
        get {
            int id;

            Int32.TryParse(this.name.Last().ToString(), out id);

            return --id;
        }
    }

    public Color color
    {
        get { return this.instance.GetComponent<SpriteRenderer>().color; }
        set { this.instance.GetComponent<SpriteRenderer>().color = value; }
    }

}
