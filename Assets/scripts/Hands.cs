using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {

    public static Hands instance;
    public static HandState Left { get; private set; } = HandState.NONE;
    public static HandState Right { get; private set; } = HandState.NONE;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        EventSystem.current.OnArrowGrab += GrabArrow;
    }

    private void GrabArrow(Side side) {
        if (side == Side.LEFT) {
            Left = HandState.ARROW;
        } else if (side == Side.RIGHT) {
            Right = HandState.ARROW;
        }
    }
}
