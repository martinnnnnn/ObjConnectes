using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private Transform   m_SpawnLocation;
    [SerializeField]
    private float       m_TimeBeforeSpawn;
    private float       m_SpawnTimer;
    [SerializeField]
    private float       m_TargetsLifetime;

    [SerializeField]
    private List<GameObject> m_PossibleTargets = new List<GameObject>();
    private Target           m_CurrentTarget;
    private Camera           m_Camera;

    void Start () {
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_SpawnTimer = m_TimeBeforeSpawn;
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

        Vector3 randomViewportPosition = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0);
        Vector3 spawnLocation = m_Camera.ViewportToWorldPoint(randomViewportPosition);
        m_SpawnLocation.position = spawnLocation;

        m_CurrentTarget = Instantiate(m_PossibleTargets[targetIndex], m_SpawnLocation).GetComponent<Target>();
        m_CurrentTarget.Lifetime = m_TargetsLifetime;
        m_CurrentTarget.enabled = true;
    }

    public void ResetSpawn()
    {
        DestroyTarget();
        m_SpawnTimer = m_TimeBeforeSpawn;
    }

    private void DestroyTarget()
    {
        Destroy(m_CurrentTarget.gameObject);
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
