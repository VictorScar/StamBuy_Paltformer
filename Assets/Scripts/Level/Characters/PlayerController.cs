using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour, IPunObservable, IOnEventCallback
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Gun gun;
    [SerializeField] private GroundCheker groundChecker;
    [SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private Transform aim;
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float jumpDuration = 1f;

    [SerializeField] private float gravityForce = 200f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health = 100f;

    private PointCounter pointCounter;

    private Vector2 _direction;
    private int anamtorState;
    private float syncDeltaTime;

    public PointCounter PointCounter { get => pointCounter; }
    public float MaxHealth { get => maxHealth; }
    public PhotonView PV { get => photonView; }

    public event Action<float> onHealthChanged;
    public event Action onCharacterDied;
    public event Action<PlayerController> onSpawn;

    public void Start()
    {
        pointCounter = new PointCounter();

        GameLevel.Instance.AddPlayerPawn(this);
        //onSpawn?.Invoke(this);
    }

    public void Move(Vector2 direction)
    {
        _direction = direction;

        if (_direction == Vector2.zero)
        {
            anamtorState = 0;
        }
        else
        {
            anamtorState = 2;
        }


        rb.velocity = direction * moveSpeed * 100f * Time.fixedDeltaTime;

    }

    public void Jump()
    {
        if (groundChecker.IsGrounded)
        {
            StartCoroutine(JumpMoving());

        }

    }

    public void Fire()
    {
        gun.Shoot();
    }

    public void GetDamage(float damage)
    {
        photonView.RPC("RPC_GetDamage", RpcTarget.All, damage);
    }

    private void Death()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is Dead!");
        Destroy(gameObject);
    }

    private void PlayerRotate()
    {
        if (_direction == Vector2.right)
        {
            //transform.rotation = Quaternion.identity;
            characterSprite.flipX = false;
            weaponSprite.flipX = false;
            aim.transform.rotation = Quaternion.identity;
        }
        else if (_direction == Vector2.left)
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            characterSprite.flipX = true;
            weaponSprite.flipX = true;
            aim.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator JumpMoving()
    {

        float currentTime = 0;
       // float syncDeltaTime = 0;

        while (currentTime < jumpDuration)
        {
            rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            currentTime += Time.deltaTime;
            yield return null;
        }

        //rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Force);
        //yield return null;

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_direction);
            stream.SendNext(anamtorState);
            stream.SendNext(syncDeltaTime);
        }
        else
        {
            _direction = (Vector2)stream.ReceiveNext();
            anamtorState = (int)stream.ReceiveNext();
            syncDeltaTime = (float)stream.ReceiveNext();
        }

    }

    public void OnEvent(EventData photonEvent)
    {

    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            syncDeltaTime = Time.fixedDeltaTime;
        }
        else
        {
            RunTimeLogger.Log(syncDeltaTime.ToString());
        }
       

        rb.AddForce(Vector2.down * gravityForce * Time.fixedDeltaTime);
        PlayerRotate();
        animator.SetInteger("Animate", anamtorState);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    [PunRPC]
    private void RPC_GetDamage(float damage)
    {
        onHealthChanged?.Invoke(health);

        if (damage < health)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            Death();
        }
    }

}
