using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Vector3 pos;
    float HorizontalSpeed;
    
	float VerticalRange;

	private bool Cooked = false;
	[SerializeField] int life = 1;
	[SerializeField] int healAmount = 5;

	[SerializeField] Sprite DeadFish;

	[SerializeField] SpriteRenderer spriteRenderer;


	// Start is called before the first frame update
    void Start()
    {
	    pos = transform.position;
		VerticalRange = Random.Range(0.5f, 1.3f);
		HorizontalSpeed = Random.Range(2.8f, 4.0f);
		transform.Rotate(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
	    if (!Cooked)
	    {
		    pos += transform.right * Time.deltaTime * HorizontalSpeed;
		    transform.position = pos + Vector3.up * Mathf.Sin(Time.realtimeSinceStartup) * VerticalRange;
	    }
	    else
	    {
		    transform.position += transform.right * Time.deltaTime * HorizontalSpeed * 0.5f;

	    }
	    
	    if (transform.position.x <= -10.3)
	    {
		    DestroyFish();
	    }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.gameObject.tag == "Laser" && !Cooked)
	    {
		    life--;

		    if (life == 0 && !Cooked)
		    {
				CookFish();
		    }
	    }
	    
	    if (other.gameObject.tag == "Player" && Cooked)
	    {
		    DestroyFish();
	    }
	    
    }

	public void CookFish()
	{
		spriteRenderer.sprite = DeadFish;
		Cooked = true;
	}

	public void SetHealAmount(int amount)
	{
		healAmount = amount;
	}

	public bool GetCookedStatus()
	{
		return Cooked;
	}

	public float GetHealAmount()
	{
		return healAmount;
	}

	public void DestroyFish() //if implementing, put object pooling here
	{
		Destroy(gameObject);
	}
}
