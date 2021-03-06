using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMove : MonoBehaviour
{
	private Rigidbody2D _rd;
	private Animator _ra = null;
	[SerializeField]
	private int HP = 3;
	public float speed = 4f;//ę­©ćć¹ććEćE


	private bool _dashAttack_trigger = false;
	private bool isAnim = false;

	//public Vector3 SPEED = new Vector3(0.05f, 0.05f, 0.05f);
	public Vector3 JUMP = new Vector3(0.05f, 0.05f, 0.05f);
	void Start()
	{

	}

	void Update()
	{
		_rd = GetComponent<Rigidbody2D>();
		_ra = GetComponent<Animator>();

	    isAnim = _ra.GetCurrentAnimatorStateInfo(0).IsName("Damege");

        Vector3 Position = transform.position;
        if (Input.GetKey("up"))
		{
			Position.y += JUMP.y;
		}
		if (Input.GetKey(KeyCode.S) && !_dashAttack_trigger )
		{
			_ra.SetBool("Attack", true);
		}
		else
		{
			_ra.SetBool("Attack", false);
		}

		if (Input.GetKey("left"))
		{
			_dashAttack_trigger = true;
			//Position.x -= SPEED.x;
			_ra.SetBool("Walk", true);

		}
		else if (Input.GetKey("right"))
		{
			_dashAttack_trigger = true;
			//Position.x += SPEED.x;
			_ra.SetBool("Walk", true);
			
		}
		else
		{
			_dashAttack_trigger = false;
			_ra.SetBool("Walk", false);
		}

			if (Input.GetKey(KeyCode.S) && _dashAttack_trigger)
			{
			
				_ra.SetBool("DashAttack", true);
			}
			else
			{
				_ra.SetBool("DashAttack", false);
			}

		float x = Input.GetAxisRaw("Horizontal");
		// ćEć©ć«ććå³åćć®ē»åćEå “åE
		// ć¹ć±ć¼ć«å¤åćåŗćE
		//Vector3 scale = transform.localScale;
		//if (x >= 0)
		//{
		//	// å³ę¹åć«ē§»åäø­
		//	scale.x = 1; // ććEć¾ć¾Eå³åćEE
		//}
		//else
		//{
		//	// å·¦ę¹åć«ē§»åäø­
		//	scale.x = -1; // åč»¢ććEå·¦åćEE
		//}
		//// ä»£å„ćē“ćE
		//transform.localScale = scale;

		//transform.position = Position;


		//ć­ć£ć©ćÆćæć¼ć®åćé¢é£------------------------------------------------------------------------------------------
		if (x != 0)
		{
			//å„åę¹åćøē§»åE
			_rd.velocity = new Vector2(x * speed, _rd.velocity.y);
			//localScale.xćE1ć«ćććØē»åćåč»¢ćć
			Vector2 temp = transform.localScale;
			temp.x = x;
			transform.localScale = temp;
			//å·¦ćå³ćåEåćć¦ćEŖćć£ćć
		}
		else
		{
			//ęØŖē§»åćEéåŗ¦ćEć«ćć¦ććæćEØę­¢ć¾ćććE«ćć
			_rd.velocity = new Vector2(0, _rd.velocity.y);
		}
		//----------------------------------------------------------------------------------------------------------------

	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (1 <= HP && !isAnim )
		{
			string tag = collision.gameObject.tag;
			if (tag == "Spike")
			{
				_ra.SetBool("Damege", true);
				HP--;
			}
		
		}

		if( HP <= 0)
        {

			die();
        }
	}

	private void die()
	{
		Destroy(this.gameObject);
	}
	private void DamegeBoool()
	{
			_ra.SetBool("Damege", false);
	}
}