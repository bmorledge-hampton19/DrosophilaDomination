using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarManager : MonoBehaviour {

	private List<Jar> jars;

	// Use this for initialization
	void Start () {
		
		jars = new List<Jar>();
		jars.Add(new Jar(TraitData.TraitTier.research));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
