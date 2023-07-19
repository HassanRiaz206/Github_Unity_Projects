using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Camera cam;
	Rigidbody2D rb;
	
	[SerializeField] float PlayerSpeed;
	bool isMoving = false;

	Vector2 pos;
	float screenBounds;
	float velocityX;
	GameObject Player;

	void Start ()
	{
		cam = Camera.main;

		rb = GetComponent<Rigidbody2D> ();
		pos = rb.position;

		screenBounds = Game.Instance.screenWidth - 0.56f;
	}

	void Update ()
	{
		//Check player input ( hand or mouse drag)
		isMoving = Input.GetMouseButton (0);

		if (isMoving) {
			pos.x = cam.ScreenToWorldPoint (Input.mousePosition).x;
		}
	}

	void FixedUpdate()
	{
		//Move the cannon
		if (isMoving)
		{
			rb.MovePosition(Vector2.Lerp(rb.position, pos, PlayerSpeed * Time.fixedDeltaTime));
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
	}


}
