using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float    Lifetime;
    protected float m_StartTime;
    protected float m_Time
    {
        get
        {
            return Time.time - m_StartTime;
        }
    }

    private LevelManager m_LevelManager;
    private Renderer     m_Renderer;
    private Camera       m_Camera;
    private Collider2D   m_TargetCollider;

    protected void Start () {
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        m_TargetCollider = GetComponent<Collider2D>();
        m_Renderer = GetComponent<Renderer>();
        m_StartTime = Time.time;
    }

    protected void Update()
    {
        ControlLifetime();
    }

    #region Lifetime
    private void ControlLifetime()
    {
        if (m_Time > Lifetime)
        {
            m_LevelManager.TargetDisappearedBeforeAnyReaction();
            Destroy(gameObject);
        }
    }
    #endregion

    #region Visibility
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
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider2D>().bounds);    
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
    #endregion

}
