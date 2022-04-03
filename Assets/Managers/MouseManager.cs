using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public GameManager GameManager;
    
    public LayerMask ClickableLayers;
    
    private Vector3 m_lastClickLocation;

    public MouseClickLocationEvent MouseClickLocationEvent;

    public GameObject ClickMarkerPrefab;

    private GameObject m_MarkerInstance;

    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameOver) return;
        
        if (!GameManager.IsDay) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, ClickableLayers))
            {
                m_lastClickLocation = hit.point;
                MouseClickLocationEvent.Invoke(hit.point);
            }

        }
    }
}

[Serializable]
public class MouseClickLocationEvent : UnityEvent<Vector3> {}
