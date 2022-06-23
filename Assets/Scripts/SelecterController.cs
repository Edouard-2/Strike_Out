using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterController : MonoBehaviour
{
    private void Awake()
    {
        SponsorManager.Instance.Add(this);
        gameObject.name += $"_{SponsorManager.Instance.m_id}";
    }
}
