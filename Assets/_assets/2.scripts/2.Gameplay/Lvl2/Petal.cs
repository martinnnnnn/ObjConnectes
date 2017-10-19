using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petal : MonoBehaviour {

    [SerializeField]
    private bool m_ShouldPhysicalize = true;
    public bool IsUp;
    private Camera m_Camera;
    private Collider2D m_PetalCollider;
    private Rigidbody2D m_Rigidbody;
    private Transform m_NeutralParent;
    private Transform m_ContainerOnFlowerTransform;
    private Flower m_ParentFlower;

	// Use this for initialization
	void Start () {
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_NeutralParent = GameObject.Find("NeutralParent").transform;
        m_ContainerOnFlowerTransform = transform.parent;
        if(m_ShouldPhysicalize)
        {
            m_ParentFlower = m_ContainerOnFlowerTransform.parent.GetComponent<Flower>();
        }
        m_PetalCollider = GetComponent<Collider2D>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsPetalInCameraFrustum())
        {
            NeutralizeRigidbody();
        }
	}

    private void DeparentPetal()
    {
        transform.SetParent(m_NeutralParent);
    }

    private void ReparentPetal()
    {
        transform.SetParent(m_ContainerOnFlowerTransform);
    }

    public void Leave()
    {
        if(m_ShouldPhysicalize)
        {
            DeparentPetal();
            m_ParentFlower.AnimateLosePetal();
            if (!m_Rigidbody)
            {
                m_Rigidbody = GetComponent<Rigidbody2D>();
            }
            m_Rigidbody.simulated = true;
            m_Rigidbody.AddForce(transform.up * 100);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
        IsUp = false;
    }

    private void NeutralizeRigidbody()
    {
        m_Rigidbody.simulated = false;
        m_Rigidbody.velocity = Vector2.zero;
    }

    public void Grow()
    {
        if(m_ShouldPhysicalize)
        {
            ReparentPetal();
            NeutralizeRigidbody();
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            gameObject.SetActive(true);
        }
        
        IsUp = true;
    }

    private bool IsPetalInCameraFrustum()
    {
        if (m_PetalCollider == null)
        {
            return false;
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(m_Camera);
        return GeometryUtility.TestPlanesAABB(planes, m_PetalCollider.bounds);
    }
}
