using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private float m_SpawnTimer;
    private float m_DestroyTimer;
    [SerializeField]
    private float m_TimeBeforeSpawn;
    [SerializeField]
    private float m_TimeBeforeDestroy;
    [SerializeField]
    private List<GameObject> m_PossibleTargets = new List<GameObject>();
    private Target m_CurrentTarget;
    public bool TargetDisappeared;

    // Use this for initialization
    void Start () {
        m_SpawnTimer = m_TimeBeforeSpawn;
        m_DestroyTimer = m_TimeBeforeDestroy;
	}
	
	// Update is called once per frame
	void Update () {
        if(m_CurrentTarget == null)
        {
            CountdownSpawn();
        }
        else
        {
            Debug.Log(IsTargetVisible());
            CountdownDestroy();
        }
        
	}

    private void CountdownSpawn()
    {
        m_SpawnTimer -= Time.deltaTime;
        if (m_SpawnTimer < 0)
        {
            TargetDisappeared = false;
            Spawn();
            m_SpawnTimer = m_TimeBeforeSpawn;
        }
    }

    private void Spawn()
    {
        int targetIndex = Random.Range(0, m_PossibleTargets.Count);
        m_CurrentTarget = Instantiate(m_PossibleTargets[targetIndex]).GetComponent<Target>();
    }

    private void CountdownDestroy()
    {
        m_DestroyTimer -= Time.deltaTime;
        if(m_DestroyTimer < 0)
        {
            DestroyTarget();
            m_DestroyTimer = m_TimeBeforeDestroy;
            TargetDisappeared = true;
        }
    }

    private void DestroyTarget()
    {
        Destroy(m_CurrentTarget.gameObject);
    }

    public void ResetSpawn()
    {
        DestroyTarget();
        m_SpawnTimer = m_TimeBeforeSpawn;
        m_DestroyTimer = m_TimeBeforeDestroy;
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
