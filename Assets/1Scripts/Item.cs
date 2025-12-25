using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Exp, Heart, Weapon }
    public Type type;
    public int value;
}
