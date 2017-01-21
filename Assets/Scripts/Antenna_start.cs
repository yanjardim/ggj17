using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna_start : MonoBehaviour {
	public float startAngle;
	public int potencia;

	void Start () {
		startAngle = 0;

		potencia = 100000000;
	}
	
	// Update is called once per frame
	void Update () {
        


	}

	void FixedUpdate() {
		//if (startAngle < 360) {
		//gameObject.transform.Rotate (new Vector3(0, 1, 0));

		//startAngle += 1;

		//Vector3 direction = new Vector3 ((int)transform.position.x*1*Time.deltaTime, (int)transform.position.y,(int) transform.position.z);
			//Vector3 fwd = transform.TransformDirection(direction);
		Vector3 fwd = transform.TransformDirection(Vector3.right * startAngle);
		Debug.DrawRay (transform.position, fwd, Color.red);

		RaycastHit hit;
		if (Physics.Raycast (transform.position, fwd, out hit ) &&  hit.transform.gameObject != null) {
			if (hit.transform.gameObject.tag == "Antenna_middle") {
				//
				Antenna_middle scriptMiddle;
				scriptMiddle = hit.transform.GetComponent<Antenna_middle> ();
				scriptMiddle.recebeuOnda = true;
				scriptMiddle.anguloResposta = GetAnguloFinal (this.transform, hit.transform);


			}

		}
	}

	float GetAnguloFinal (Transform atual, Transform proximo){
		if (atual != null && proximo != null) {
			float catetoOposto;
			if (atual.position.x - proximo.position.x > 0)
				catetoOposto = atual.position.x - proximo.position.x;
			else
				catetoOposto = -(atual.position.x - proximo.position.x);


			Vector2 distanceCalculatedX = new Vector2 (atual.position.x, proximo.position.x);
			Vector2 distanceCalculatedZ = new Vector2 (atual.position.z, proximo.position.z);
			float hip = Vector2.Distance (distanceCalculatedX, distanceCalculatedZ);


			return Mathf.Asin(catetoOposto/hip);
		}
		return 0;
	}
}
