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

    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float jumpDuration = 1f;

    [SerializeField] private float gravityForce = 200f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health = 100f;

    private PointCounter pointCounter;

    private Vector2 _direction;

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
        //PlayerRotate(direction);
        rb.velocity = direction * moveSpeed * 100f * Time.fixedDeltaTime;
        //transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
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

        while (currentTime < jumpDuration)
        {
            rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            currentTime += Time.fixedDeltaTime;
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
        }
        else
        {
            _direction = (Vector2)stream.ReceiveNext();
        }

    }

    public void OnEvent(EventData photonEvent)
    {

    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.down * gravityForce * Time.fixedDeltaTime);
        PlayerRotate();
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
