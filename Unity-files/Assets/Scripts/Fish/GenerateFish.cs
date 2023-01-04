using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFish : MonoBehaviour
{
    [SerializeField] GameObject Fish;
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float yMin;
    [SerializeField] float yMax;
	[SerializeField] int NumofFish = 5;

    float xPos;
    float yPos;

    int Fishcount = 0;

    
    void Start()
    {
		StartCoroutine(FishSpawn());	
    }

	void Update()
	{
		Fishcount = transform.childCount;
	}

	IEnumerator FishSpawn()
	{
		while (Fishcount < NumofFish)
		{
			xPos = Random.Range(xMin, xMax);
			yPos = Random.Range(yMin, yMax);
			Instantiate(Fish, new Vector2(xPos, yPos), Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(1f, 2f));
		}
	}

}
