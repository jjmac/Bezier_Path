﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class BezierPoint
{
    public int activeHandleId = 1;

    [SerializeField]
    private BezierHandle[] m_handles = new BezierHandle[2];
    [SerializeField]
    private Vector3 m_position;
    [SerializeField]
    private Vector3 m_localPosition;
    [SerializeField]
    private Quaternion m_rotation;

    [SerializeField]
    private bool m_isAutoSmooth;

    public bool IsAutoSmooth
    {
        get
        {
            return m_isAutoSmooth;
        }

        set
        {
            m_isAutoSmooth = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return m_position;
        }

        set
        {
            m_position = value;
            UpdateHandlesPosition();
        }
    }

    public Vector3 LocalPosition
    {
        get
        {
            return m_localPosition;
        }

        set
        {
            m_localPosition = value;
        }
    }

    public Quaternion Rotation
    {
        get
        {
            return m_rotation;
        }

        set
        {
            m_rotation = value;
            UpdateHandlesPosition();
        }
    }

    /// <summary>
    /// Avoid misset values when deserializing.
    /// Only used when click add point button or create one in game.
    /// </summary>
    /// <param name="_isFirstTime"></param>
    public BezierPoint(bool _isFirstTime)
    {
        if (_isFirstTime == false)
            return;
        
        m_position = Vector3.zero;
        m_rotation = Quaternion.identity;

        m_handles[0] = new BezierHandle();
        m_handles[1] = new BezierHandle();
    }

    /// <summary>
    /// Update handles' position based on their localPosition
    /// Called every frame or everytime changed?
    /// </summary>
    public void UpdateHandlesPosition()
    {
        SetHandleLocalPosition(0, m_handles[0].LocalPosition);
        SetHandleLocalPosition(1, m_handles[1].LocalPosition);
    }

    public BezierHandle GetHandle(int _id)
    {
        return m_handles[_id];
    }

    public void SetHandlePosition(int _id, Vector3 _position)
    {
        m_handles[_id].Position = _position;
        m_handles[_id].LocalPosition = Quaternion.Inverse(this.Rotation) * (_position - this.Position);
    }

    public void SetHandleLocalPosition(int _id, Vector3 _localPosition)
    {
        m_handles[_id].LocalPosition = _localPosition;
        m_handles[_id].Position = this.Rotation * _localPosition + this.Position;
    }

    /// <summary>
    /// Adjsut secondary handle to the opposite postion of primary one.
    /// </summary>
    private void SmoothHandle(bool _basedOnPrimary = true)
    {
        int refId = _basedOnPrimary ? 0 : 1;

        m_handles[1 - refId].Position = 2 * Position - m_handles[refId].Position;
    }

    public void UpdatePosition()
    {
        if (IsAutoSmooth)
        {
            SmoothHandle(activeHandleId == 0);
        }
    }
}
