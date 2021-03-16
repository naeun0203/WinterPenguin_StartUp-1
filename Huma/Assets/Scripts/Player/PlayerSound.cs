/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.3.10
 * Script Purpose : Player's Sound Effect (SFX) Manager
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
	AudioSource speaker; // Speaker in the Player
	public AudioClip[] clips;
	// Clip list
	// 1. 
	//
	//
	//
	

	private void Start()
	{
		speaker = GetComponent<AudioSource>();
	}

	private IEnumerator SpeakerFunc()
	{
		
		yield return null;
	}

}