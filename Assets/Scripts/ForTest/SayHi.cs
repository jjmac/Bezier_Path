﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayHi : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<BezierPathMover>().onCompleteEveryNode += SayHiAfterReachNode;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SayHiAfterReachDst()
    {
        print("Say Hi");
    }

    public void SayHiAfterReachNode(int _id)
    {
        print("Say Hi " + _id);
    }
}
