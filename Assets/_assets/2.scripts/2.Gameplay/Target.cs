using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private Renderer m_Renderer;
    private Camera m_Camera;
    private Collider2D m_TargetCollider;

    void Start () {
        m_Renderer = GetComponent<Renderer>();
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_TargetCollider = GetComponent<Collider2D>();

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
        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider2D>().bounds))
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

        RaycastHit2D hit;
        Vector3 direction = transform.position - transform.forward;
        hit = Physics2D.Raycast(transform.position, direction);
        if (!hit)
        {
            return false;
        }
        return hit.collider.CompareTag("Obstructer");
    }


}
