using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {

    public static Hands instance;
    public HandState Left { get; private set; } = HandState.NONE;
    public HandState Right { get; private set; } = HandState.NONE;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        EventManager.current.OnArrowGrab += GrabArrow;
        EventManager.current.OnArrowNock += NockArrow;
        EventManager.current.OnStringGrab += GrabString;
        EventManager.current.OnStringRelease += ReleaseString;
        EventManager.current.OnBowGrab += GrabBow;
        EventManager.current.OnBowRelease += ReleaseBow;
    }

    private void GrabArrow(Side side) {
        if (side == Side.LEFT) {
            Left = HandState.ARROW;
        } else if (side == Side.RIGHT) {
            Right = HandState.ARROW;
        }
    }

    private void NockArrow() {
        if (Left == HandState.ARROW) {
            Left = HandState.NONE;
        }
        if (Right == HandState.ARROW) {
            Right = HandState.NONE;
        }
    }

    private void GrabString(Side side) {
        if (side == Side.LEFT) {
            Left = HandState.STRING;
        } else if (side == Side.RIGHT) {
            Right = HandState.STRING;
        }
    }

    private void ReleaseString() {
        if (Left == HandState.STRING) {
            Left = HandState.NONE;
        }
        if (Right == HandState.STRING) {
            Right = HandState.NONE;
        }
    }

    private void GrabBow(Side side) {
        if (side == Side.LEFT) {
            Left = HandState.BOW;
        } else if (side == Side.RIGHT) {
            Right = HandState.BOW;
        }
    }

    private void ReleaseBow() {
        if (Left == HandState.BOW) {
            Left = HandState.NONE;
        }
        if (Right == HandState.BOW) {
            Right = HandState.NONE;
        }
    }


    public bool IsArrowInAnyHand() {
        return Left == HandState.ARROW || Right == HandState.ARROW;
    }

    public bool IsStringInAnyHand() {
        return Left == HandState.STRING || Right == HandState.STRING;
    }
}
