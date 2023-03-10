using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBouncePad : MonoBehaviour, IBouncePad
{
    [Header("在吸收子弹后吐出子弹所需要的时间")]
    [SerializeField]
    private float secondsConsumingBullets;

    private Queue<Coroutine> coroutines = new Queue<Coroutine>();
    private Animator anim;

    public E_Team SourceTeam => E_Team.Carrots;

    public E_Team TargetTeam => E_Team.Player;

    public void Bounce(Bullet bullet)
    {
        AudioManager.Instance.PlayRandomEffectAudio("eat1", "eat2", "eat3");
        anim.SetTrigger("Eat");
        coroutines.Enqueue(StartCoroutine(GagBullet(bullet)));
        Main.Instance.Cursor.CursorStatus = CursorController.Status.Targeting;
    }
    private IEnumerator GagBullet(Bullet bullet)
    {
        float originalSpeed = bullet.Speed;
        bullet.gameObject.SetActive(false);
        bullet.Speed = 0;
        yield return new WaitForSeconds(secondsConsumingBullets);
        if(bullet != null) 
        {
            AudioManager.Instance.PlayRandomEffectAudio("spit1", "spit4");
            anim.SetTrigger("Spit");
            bullet.gameObject.SetActive(true);
            bullet.transform.position = transform.position;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bullet.transform.right = new Vector3(worldPos.x, worldPos.y) - GameController.Instance.PlayerController.Player.PlayerPosition;
            bullet.Speed = originalSpeed;
        }
        //最先进去的必然最先出来
        coroutines.Dequeue();
        if(coroutines.Count == 0)
        {
            anim.SetBool("HaveBulletLeft", false); 
            Main.Instance.Cursor.CursorStatus = CursorController.Status.None;
        }
        else
        {
            anim.SetBool("HaveBulletLeft", true);
        }
    }


    void StartRefresh()
    {
        GameController.Instance.StartGenerateCarrots();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
    }
}
