using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private Renderer m_Renderer;
    private Camera m_Camera;
    private Collider m_TargetCollider;

    void Start () {
        m_Renderer = GetComponent<Renderer>();
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_TargetCollider = GetComponent<Collider>();

    }

    public bool IsVisible()
    {
        return IsTargetInCameraFrustum() && !IsTargetObstructed();
    }

    private bool IsTargetInCameraFrustum()
    {
        if (m_TargetCollider == null)
        {
            return false;
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(m_Camera);
        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsTargetObstructed()
    {
        if (!m_Renderer.isVisible)
        {
            return false;
        }

        RaycastHit hit;
        Vector3 direction = transform.position - transform.forward;
        if (!Physics.Raycast(transform.position, direction, out hit))
        {
            return false;
        }
        return hit.collider.CompareTag("Obstructer");
    }


}
