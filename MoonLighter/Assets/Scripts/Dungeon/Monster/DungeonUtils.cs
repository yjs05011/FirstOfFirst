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
            // x ��ǥ�� ���� ū ��� (�� or ��)
            if (vector.x > 0)
            {
                // ����
                return Direction.Right;
            }
            else
            {
                // ����
                return Direction.Left;
            }
        }
        else
        {
            // y ��ǥ�� ���� ū ��� (�� or ��)
            if (vector.y > 0)
            {
                // ���
                return Direction.Up;
            }
            else
            {
                // �ϴ�
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
            // x ��ǥ�� ���� ū ��� (�� or ��)
            if (vector.x > 0)
            {
                // ����
                vector = Vector2.right;
            }
            else
            {
                // ����
                vector = Vector2.left;
            }
        }
        else
        {
            // y ��ǥ�� ���� ū ��� (�� or ��)
            if (vector.y > 0)
            {
                // ���
                vector = Vector2.up;
            }
            else
            {
                // �ϴ�
                vector = Vector2.down;
            }
        }
        return vector;
    }
}
