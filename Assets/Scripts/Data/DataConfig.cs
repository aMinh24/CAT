using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Cat",menuName = "Data/Cat")]
public class DataConfig : ScriptableObject
{
    [Header("Cat")]
    public float jumpHeight = 3.5f;
    public float speed = 10;
    public float speedJump = 10;
    public float speedClimb = 5;
    public float speedSlide = 0.5f;
    public float acceleration = 10;
    public float timeCoyote;
    public float gravity = 1;
    public PhysicsMaterial2D slip;

}
