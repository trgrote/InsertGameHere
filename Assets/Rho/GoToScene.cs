using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace rho
{
	public class GoToScene : MonoBehaviour
	{
		[SerializeField, Scene] private string sceneName;

		public void OnButtonPressed()
		{
			if (sceneName != "")
			{
				SceneManager.LoadScene(sceneName);
			}
		}
	}
}
