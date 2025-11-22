using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerAttack : MonoBehaviour
{
    // 子物体 Eye
    public GameObject eye;
    
    public Rigidbody2D rb;
    //public Vector2 direction;
    //敌人碰撞器
    
    public Collider2D monkeyCollider;
    // Eye 向前移动的速度
    public Vector2 moveSpeed;
    // Eye 向前移动的最大距离
    public float maxDistance = 5f;
    // 攻击动画
    public Animator eyeAnimator;
    // 攻击伤害
    public int attackDamage;

    private Vector3 startPosition;
    private bool isAttacking = false;
    private float distanceMoved = 0f;

    private void Start()
    {
        //Input
        //InputManger.Instance?.SwitchToMouse();
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        rb = GameObject.FindGameObjectWithTag("Eye").GetComponent<Rigidbody2D>();
        monkeyCollider = enemyObject.GetComponent<Collider2D>();
        
        // 初始时隐藏 Eye
        eye.SetActive(false);
    }

    private void Update()
    {
        // 按下 F 键触发攻击
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            eye.SetActive(true);
            StartAttack();
        }

        if (isAttacking)
        {
            eye.SetActive(true);
            eyeAnimator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
            MoveEyeForward();

        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        // 显示 Eye
        eye.SetActive(true);
        
        distanceMoved = 0f;
        // 播放攻击动画
       
            eyeAnimator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        
    }

    private void MoveEyeForward()
    {
        //计算方向
       float direction = transform.localScale.x;
        //direction = (GameObject.FindGameObjectWithTag("Enemy").transform.position - eye.transform.position).normalized;
        // 计算 Eye 向前移动的距离
        float step = moveSpeed.x * Time.deltaTime;
        
        rb.velocity = new Vector2(direction * moveSpeed.x, rb.velocity.y);
        distanceMoved += step;

       
        // 如果达到最大距离，回到初始位置
        if (distanceMoved >= maxDistance)
        {
            EndAttack();
        }
    }

   
    private void OnTriggerStay2D(Collider2D eyeCollider)
    {
        monkeyCollider.GetComponent<EnemyHealth>()?.TakeDamage(20);

    }
    

    private void EndAttack()
    {
        eye.transform.position = transform.position;
        isAttacking = false;
        // 隐藏 Eye
        eye.SetActive(false);
        // 重置 Eye 的位置
        
    }
    
}


