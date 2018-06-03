using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCColorChange : MonoBehaviour {

	[SerializeField] Gradient BurnGradient;
	[SerializeField] Gradient TanGradient;

	// [SerializeField] [Range(0,1)] float tanFactor = 0f;
	// [SerializeField] [Range(0,1)] float burnFactor = 0f;
	private Renderer renderer;
	private NPCData data;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		data = transform.parent.GetComponent<NPCData>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        ApplyColor();
    }

    private void ApplyColor()
    {
        Color combinedColor = CombinedColor();

        renderer.material.shader = Shader.Find("_Color");
        renderer.material.SetColor("_Color", combinedColor);
        renderer.material.shader = Shader.Find("Specular");
        renderer.material.SetColor("_SpecColor", new Color(0, 0, 0, 0));
    }

    private Color CombinedColor() {
		float tanFactor = data.tanLevel / 100f;
		// float burnFactor = data.burnLevel / 100f;
		return BurnGradient.Evaluate(burnFactor) * TanGradient.Evaluate(tanFactor);
	}
}
