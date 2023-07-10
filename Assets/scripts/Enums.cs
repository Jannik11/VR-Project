using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BowState {
    AIMING, IDLE, NONE
}

public enum ArrowState {
    INHAND, INBOW, FLYING, NONE
}

public enum HandState {
    ARROW, STRING, BOW, NONE
}
public enum Side {
    LEFT, RIGHT
}

public enum HitZoneType { _2by2, _1by3, _3by2, _2by3, _3by1 }
public enum AttachmentType { TOP, RIGHT, BOTTOM, LEFT, VERTICAL, HORIZONTAL }
