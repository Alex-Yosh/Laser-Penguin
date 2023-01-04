using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float speed = 0.5f;

    [SerializeField]
    protected int lifePoints = 1;

    [SerializeField]
    protected float enemyScoreValue = 10f;

    [SerializeField]
    int cookedFishHealAmount = 5;

    [SerializeField]
    GameObject cookedFish;

    [SerializeField]
    float cookedFishScaling = 1f;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {

            Laser l = other.GetComponent<Laser>();
            lifePoints -= l.GetLaserDamage();

            l.DestroyLaser(); //laser gets blocked by enemies

            if (lifePoints <= 0)
            {
                GameplayUI.points += enemyScoreValue;
                var fish = Instantiate(cookedFish, transform.position, Quaternion.identity);
                fish.transform.localScale = new Vector3(cookedFishScaling, cookedFishScaling, cookedFishScaling);
                var fishMovement = fish.GetComponent<FishMovement>();
                fishMovement.CookFish();
                fishMovement.SetHealAmount(cookedFishHealAmount);
                Destroy(gameObject);
            }
            else
            {
                spriteRenderer.color = Color.red;
                Invoke("RestoreEnemyColor", 0.25f);
            }
        }
    }

    void RestoreEnemyColor()
    {
        spriteRenderer.color = Color.white;
    }

    // in case we want to add cool particles or something
    /*IEnumerator Explode()
    { 
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    } */

}
