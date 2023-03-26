using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonUtils
{
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public static Direction Convert2CardinalDirectionsEnum(Vector2 vector)
    {
        vector = vector.normalized;

        float xAbs = Mathf.Abs(vector.x);
        float yAbs = Mathf.Abs(vector.y);

        if (xAbs > yAbs)
        {
            // x 좌표가 가장 큰 경우 (좌 or 우)
            if (vector.x > 0)
            {
                // 우측
                return Direction.Right;
            }
            else
            {
                // 좌측
                return Direction.Left;
            }
        }
        else
        {
            // y 좌표가 가장 큰 경우 (상 or 하)
            if (vector.y > 0)
            {
                // 상단
                return Direction.Up;
            }
            else
            {
                // 하단
                return Direction.Down;
            }
        }
    }

    public static Vector2 Convert2CardinalDirections(Vector2 vector)
    {
        vector = vector.normalized;

        float xAbs = Mathf.Abs(vector.x);
        float yAbs = Mathf.Abs(vector.y);

        if (xAbs > yAbs)
        {
            // x 좌표가 가장 큰 경우 (좌 or 우)
            if (vector.x > 0)
            {
                // 우측
                vector = Vector2.right;
            }
            else
            {
                // 좌측
                vector = Vector2.left;
            }
        }
        else
        {
            // y 좌표가 가장 큰 경우 (상 or 하)
            if (vector.y > 0)
            {
                // 상단
                vector = Vector2.up;
            }
            else
            {
                // 하단
                vector = Vector2.down;
            }
        }
        return vector;
    }
}
