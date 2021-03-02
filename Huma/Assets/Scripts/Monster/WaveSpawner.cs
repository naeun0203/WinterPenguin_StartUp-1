using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;
	/*
	public int NextWave
	{
		get { return nextWave + 1; }
	}
	*/
	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	[SerializeField]
	private float waveCountdown;
	[SerializeField]
	private float oneminuteCountup = 0f;
	[SerializeField]
	private float fatserterm = 0f;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

	void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine( SpawnWave ( waves[nextWave] ) );
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
		
		if(state == SpawnState.SPAWNING)
        {
			oneminuteCountup += Time.deltaTime;
            if (oneminuteCountup >= fatserterm)
            {
				
				waves[nextWave].rate-= waves[nextWave].rate * (75f / 1000f);
				oneminuteCountup = 0f;
			}
        }
		/*
		if (state == SpawnState.SPAWNING)
		{
			StartCoroutine(SpawnFaster(waves[nextWave]));
		}
		*/
	}

	void WaveCompleted()
	{
		Debug.Log("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			Debug.Log("ALL WAVES COMPLETE! Looping...");
		}
		else
		{
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Monster") == null)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;
		//StartCoroutine(SpawnFaster(waves[nextWave]));
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( _wave.rate );
		}
		state = SpawnState.WAITING;

		yield break;
	}
	/*
    IEnumerator SpawnFaster(Wave _wave)
    {
        oneminuteCountup += Time.deltaTime;
        if (oneminuteCountup >= 2f)
        {
            Debug.Log("SpawnFaster!");
            _wave.rate -= _wave.rate * (75 / 1000);
        }
        yield break;
    }
	*/
    void SpawnEnemy(Transform _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);

		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];
		Instantiate(_enemy, _sp.position, _sp.rotation);

        //gamemanager.MonsterList.Add(_enemy);
        //Debug.Log(gamemanager.MonsterList.Count);
    }

}
