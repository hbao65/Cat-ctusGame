using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {
    private Player player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            player.Damage(1);
            StartCoroutine(player.Knockback(0.02f, 350, player.transform.position));
        }
    }
}
