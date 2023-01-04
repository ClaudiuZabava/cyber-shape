using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, Health, Damage, Shield}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;

    private Player _player;

    // Use this for initialization
    private void Awake () {

        _player = GetComponentInParent<Player>();
    }
	
	// Update is called once per frame
	private void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	private void Collect()
	{
		if (collectSound)
		{
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		}

		if (collectEffect)
		{
			Instantiate(collectEffect, transform.position, Quaternion.identity);
		}

		//Below is space to add in your code for what happens based on the collectible type

		if (CollectibleType == CollectibleTypes.NoType) 
		{
			//Add in code here;
		}

		if (CollectibleType == CollectibleTypes.Health) 
		{
			_player.Heal(1);
		}

		if (CollectibleType == CollectibleTypes.Damage) 
		{
			_player.DamageBuff = true;
			StartCoroutine(CancelEffectDamage());
		}

		if (CollectibleType == CollectibleTypes.Shield) 
		{
			_player.CanTakeDamage = false;
            StartCoroutine(CancelEffectShield());
        }

		Destroy (gameObject);
	}

    private IEnumerator CancelEffectDamage()
    {
		yield return new WaitForSeconds(5f);
        _player.DamageBuff = false;
        yield break;
    }

    private IEnumerator CancelEffectShield()
    {
        yield return new WaitForSeconds(5f);
        _player.CanTakeDamage = true;
		yield break;
    }
}
