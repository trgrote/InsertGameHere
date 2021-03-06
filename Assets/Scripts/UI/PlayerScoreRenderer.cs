﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreRenderer : MonoBehaviour 
{
	[SerializeField] Text tannedText;
	[SerializeField] Text burnedText;

	void Update()
	{
		tannedText.text = string.Format("Tanned: {0}", PlayerScore.NumTanned);
		burnedText.text = string.Format("BURNED: {0}", PlayerScore.NumBurned);
	}
}
