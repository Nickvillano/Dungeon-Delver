using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dray : MonoBehaviour, IFacingMover
{
    public enum eMode { idle, move, attack, transition }
    [Header("Set in Inspector")]
    public float speed = 5;
    public float attackDuration = 0.25f; //number of seconds to attack
    public float attackDelay = 0.5f; // Delay between attacks

    [Header("Set Dynamically")]
    public int dirHeld = -1; // Direction of the held movement key
    public int facing = 1; //Direction Dray is facing
    public eMode mode = eMode.idle;  //a
    private float timeAtkDone = 0;   //b
    private float timeAtkNext = 0;   //c
    private Rigidbody rigid;
    private Animator anim;
    private InRoom inRm;

    private Vector3[] directions = {
        Vector3.right, Vector3.up, Vector3.left, Vector3.down
    };             // a

    private KeyCode[] keys = { KeyCode.RightArrow,
        KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow };

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        inRm = GetComponent<InRoom>();
    }
    void Update()
    {
        //————Handle Keyboard Input and manage eDrayModes———— 
        dirHeld = -1;
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(keys[i])) dirHeld = i;
        }
        // Pressing the attack button(s) 
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= timeAtkNext)
        {       // a 
            mode = eMode.attack;
            timeAtkDone = Time.time + attackDuration;
            timeAtkNext = Time.time + attackDelay;
        }
        //Finishing the attack when it's over 
        if (Time.time >= timeAtkDone)
        {                                      // b 
            mode = eMode.idle;
        }
        //Choosing the proper mode if we're not attacking 
        if (mode != eMode.attack)
        {                                          // c 
            if (dirHeld == -1)
            {
                mode = eMode.idle;
            }
            else
            {
                facing = dirHeld;                                            // d 
                mode = eMode.move;
            }
        }
        //————Act on the current mode———— 
        Vector3 vel = Vector3.zero;
        switch (mode)
        {                                                      // e 
            case eMode.attack:
                anim.CrossFade("Dray_Attack_" + facing, 0);
                anim.speed = 0;
                break;
            case eMode.idle:
                anim.CrossFade("Dray_Walk_" + facing, 0);
                anim.speed = 0;
                break;
            case eMode.move:
                vel = directions[dirHeld];
                anim.CrossFade("Dray_Walk_" + facing, 0);
                anim.speed = 1;
                break;
        }
        rigid.velocity = vel * speed;
    }
    // Implementation of IFacingMover 
    public int GetFacing()
    {                                                 // c 
        return facing;
    }
    public bool moving
    {                                                     // d 
        get
        {
            return (mode == eMode.move);
        }
    }
    public float GetSpeed()
    {                                                // e 
        return speed;
    }
    public float gridMult
    {
        get { return inRm.gridMult; }
    }
    public Vector2 roomPos
    {                                                 // f 
        get { return inRm.roomPos; }
        set { inRm.roomPos = value; }
    }
    public Vector2 roomNum
    {
        get { return inRm.roomNum; }
        set { inRm.roomNum = value; }
    }
    public Vector2 GetRoomPosOnGrid(float mult = -1)
    {
        return inRm.GetRoomPosOnGrid(mult);
    }

}