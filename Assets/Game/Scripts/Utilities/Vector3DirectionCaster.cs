﻿using UnityEngine;

public class Vector3DirectionCaster
{
    public static Vector3 CastOnVector3(Vector3Direction enumDirection) {
        switch (enumDirection) {
            case Vector3Direction.Forward:
                return Vector3.forward;
            case Vector3Direction.Back:
                return Vector3.back;
            case Vector3Direction.Up:
                return Vector3.up;
            case Vector3Direction.Down:
                return Vector3.down;
            case Vector3Direction.Left:
                return Vector3.left;
            case Vector3Direction.Right:
                return Vector3.right;
            case Vector3Direction.One:
                return Vector3.one;
        }

        return Vector3.zero;
    }
}
