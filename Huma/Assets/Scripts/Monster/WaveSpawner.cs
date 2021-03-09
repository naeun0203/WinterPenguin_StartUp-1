//***************************************
// EDITOR : CHA Hee Gyoung
// LAST EDITED DATE : 2021.3.09
// Script Purpose : MonsterSpawner
//***************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };
	
	
	[System.Serializable]
	public class Monster
	{
		public string name;
		public Transform enemy;
		public float rate;
	}
	public Monster[] monsters;

	[SerializeField] private float waveCountdown;
	[SerializeField] private float oneminuteCountup = 0f;
	[SerializeField] private float fatserterm = 0f;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	public Transform[] spawnPoints;

	private SpawnState state = SpawnState.SPAWNING;
	public bool StartOrNotCoroutine = false;
	public float timeBeforeStartSpawn = 5f;

	public Gamemanager gamemanager;

	public int zombie = 0;
	public int devildog = 0;
	public int spiter = 0;
	public int tanker = 0;

	private IEnumerator enumerator_0;
	private IEnumerator enumerator_1;
	private IEnumerator enumerator_2;
	private IEnumerator enumerator_3;

	Coroutine coroutine_0;
	Coroutine coroutine_1;
	Coroutine coroutine_2;
	Coroutine coroutine_3;

	void Start()
	{
		zombie = Gamemanager.zombieCount;
	    devildog = Gamemanager.devildogCount;
	    spiter = Gamemanager.spiterCount;
	    tanker = Gamemanager.tankerCount;

		enumerator_0 = ZombieSpawnWave(monsters[0]);
		enumerator_1 = DevilDogSpawnWave(monsters[1]);
		enumerator_2 = SpiterSpawnWave(monsters[2]);
		enumerator_3 = TankerSpawnWave(monsters[3]);

		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = timeBeforeStartSpawn;
	}

	void Update()
	{
        if (gamemanager.CurrentMonster >= 100)
        {
			state = SpawnState.WAITING;
        }
		else if (gamemanager.CurrentMonster < 100)
        {
			state = SpawnState.SPAWNING;
        }
		/*
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
		*/

		if (waveCountdown <= 0)
		{
			if (StartOrNotCoroutine == false)
			{
				if(zombie!=0)
					StartCoroutine(enumerator_0);
				if(devildog!=0)
					StartCoroutine(enumerator_1);
				if(spiter!=0)
					StartCoroutine(enumerator_2);
				if(tanker!=0)
					StartCoroutine(enumerator_3);

				StartOrNotCoroutine =true;
			}
			if(StartOrNotCoroutine == true)
            {
				if (zombie == 0)
					StopCoroutine(enumerator_0);
				if (devildog == 0)
					StopCoroutine(enumerator_1);
				if (spiter == 0)
					StopCoroutine(enumerator_2);
				if (tanker == 0)
					StopCoroutine(enumerator_3);
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
				monsters[0].rate-= monsters[0].rate * (75f / 1000f);//일단 좀비만
				oneminuteCountup = 0f;
			}
        }
	}

	IEnumerator ZombieSpawnWave(Monster _monster)
	{
        while (zombie > 0)
        {
			if (state == SpawnState.SPAWNING)
			{
				SpawnEnemy(_monster.enemy);
				gamemanager.CurrentMonster += 1;
				zombie--;
				yield return new WaitForSeconds(_monster.rate);
			}
			else 
			{
				yield return null; 
			}
				
		}
		state = SpawnState.WAITING; //zombie==0일때로 바뀔수도 있음
		yield break;
	}
	IEnumerator DevilDogSpawnWave(Monster __monster)
	{
		while (devildog > 0)
		{
			if (state == SpawnState.SPAWNING)
			{
				SpawnEnemy(__monster.enemy);
				gamemanager.CurrentMonster += 1;
				devildog--;
				yield return new WaitForSeconds(__monster.rate);
			}
			else
			{
				yield return null;
			}
		}
		state = SpawnState.WAITING; //zombie==0일때로 바뀔수도 있음
		yield break;
	}
	IEnumerator SpiterSpawnWave(Monster _monster)
	{
		while (spiter > 0)
		{
			if (state == SpawnState.SPAWNING)
			{
				SpawnEnemy(_monster.enemy);
				gamemanager.CurrentMonster += 1;
				spiter--;
				yield return new WaitForSeconds(_monster.rate);
			}
			else
			{
				yield return null;
			}
		}
		state = SpawnState.WAITING; //zombie==0일때로 바뀔수도 있음
		yield break;
	}
	IEnumerator TankerSpawnWave(Monster _monster)
	{
		while (tanker > 0)
		{
			if (state == SpawnState.SPAWNING)
			{
				SpawnEnemy(_monster.enemy);
				gamemanager.CurrentMonster += 1;
				tanker--;
				yield return new WaitForSeconds(_monster.rate);
			}
			else
			{
				yield return null;
			}
		}
		state = SpawnState.WAITING; //zombie==0일때로 바뀔수도 있음
		yield break;
	}
	void SpawnEnemy(Transform _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);

		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];
		Instantiate(_enemy, _sp.position, _sp.rotation);
    }

}
