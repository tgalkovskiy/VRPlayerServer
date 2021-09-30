using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float k_MaxDistance = 10;
    internal GameObject m_GazedAtObject = null;
    public EventSystem es;
    public LayerMask filter;

    public string status;
    internal float distance = k_MaxDistance;

    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, k_MaxDistance, filter))
        {
            // GameObject detected in front of the camera.
            // New GameObject.
            if (hit.transform.gameObject != m_GazedAtObject)
            {
                m_GazedAtObject?.SendMessage("OnPointerExit", new PointerEventData(es), SendMessageOptions.DontRequireReceiver);
                m_GazedAtObject = hit.transform.gameObject;
                m_GazedAtObject.SendMessage("OnPointerEnter", new PointerEventData(es), SendMessageOptions.DontRequireReceiver);

                status = "raycast new";
                distance = hit.distance;
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            try
            {
                m_GazedAtObject?.SendMessage("OnPointerExit", new PointerEventData(es),SendMessageOptions.DontRequireReceiver);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            m_GazedAtObject = null;

            status = "no raycast";
            distance = k_MaxDistance;
        }

       // if (status != "no raycast")
         //   Debug.Log("[ST] " + status);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
    }
}
