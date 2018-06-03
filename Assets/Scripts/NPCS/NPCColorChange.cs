using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCColorChange : MonoBehaviour {

	[SerializeField] Gradient BurnGradient;
	[SerializeField] Gradient TanGradient;

	// [SerializeField] [Range(0,1)] float tanFactor = 0f;
	// [SerializeField] [Range(0,1)] float burnFactor = 0f;
	private Renderer rend;
	private NPCData data;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
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

        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", combinedColor);
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", new Color(0, 0, 0, 0));
    }

    private Color CombinedColor() {
		float tanFactor = data.tanLevel / 100f;
		float burnFactor = data.burnLevel / 100f;
		return BurnGradient.Evaluate(burnFactor) * TanGradient.Evaluate(tanFactor);
	}
}
