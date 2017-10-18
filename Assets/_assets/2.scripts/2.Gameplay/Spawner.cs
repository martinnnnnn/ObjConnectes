using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private Transform m_SpawnTransform;
    private float m_SpawnTimer;
    [SerializeField]
    private float m_TimeBeforeSpawn;
    [SerializeField]
    private float m_TimeBeforeDestroy;
    [SerializeField]
    private List<GameObject> m_PossibleTargets = new List<GameObject>();
    private Target m_CurrentTarget;
    private Camera m_Camera;

    void Start () {
        m_SpawnTimer = m_TimeBeforeSpawn;
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	void Update () {
        if(m_CurrentTarget == null)
        {
            CountdownSpawn();
        } 
	}

    private void CountdownSpawn()
    {
        m_SpawnTimer -= Time.deltaTime;
        if (m_SpawnTimer < 0)
        {
            Spawn();
            m_SpawnTimer = m_TimeBeforeSpawn + Random.Range(0.0f, m_TimeBeforeSpawn/2) * Random.Range(-1, 1);
        }
    }

    private void Spawn()
    {
        int targetIndex = Random.Range(0, m_PossibleTargets.Count);

        Vector3 ViewportRandomLocation = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0);
        Vector3 spawnLocation = m_Camera.ViewportToWorldPoint(ViewportRandomLocation);
        m_SpawnTransform.position = spawnLocation;

        m_CurrentTarget = Instantiate(m_PossibleTargets[targetIndex], m_SpawnTransform).GetComponent<Target>();
        m_CurrentTarget.Lifetime = m_TimeBeforeDestroy;
        m_CurrentTarget.enabled = true;
    }

    private void DestroyTarget()
    {
        Destroy(m_CurrentTarget.gameObject);
    }

    public void ResetSpawn()
    {
        DestroyTarget();
        m_SpawnTimer = m_TimeBeforeSpawn;
    }

    public bool IsTargetVisible()
    {
        if(!m_CurrentTarget)
        {
            return false;
        }

        return m_CurrentTarget.IsVisible();
    }
}
