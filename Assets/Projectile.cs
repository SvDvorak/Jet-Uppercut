using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float Speed;
	public float TimeAliveLeftInSeconds;

	public void Update()
	{
		transform.position += transform.right * Speed * Time.deltaTime;

		TimeAliveLeftInSeconds -= Time.deltaTime;
		if (TimeAliveLeftInSeconds < 0)
		{
			Destroy(gameObject);
		}
	}
}
