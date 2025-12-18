using UnityEngine;

/// <summary>
/// 挂在Spring物体上，Player碰到后自动跳跃
/// </summary>
public class SpringJump : MonoBehaviour
{
    [Header("弹簧跳跃设置")]
    public float springJumpForce = 15f; 
    public bool isOneShot = false; 
    //private bool hasTriggered = false; 


    private void Start()
    {
        isOneShot = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
           
            //if (isOneShot && hasTriggered) return;

            
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);
               
                playerRb.AddForce(Vector2.up * springJumpForce, ForceMode2D.Impulse);

                
                PlaySpringEffect();

                
                //hasTriggered = true;
            }
            else
            {
                Debug.LogError("Player上没有Rigidbody2D组件！无法触发弹簧跳跃");
            }
        }
    }

    /// <summary>
    /// 弹簧触发效果（可选，可添加动画、音效）
    /// </summary>
    private void PlaySpringEffect()
    {
        // 示例1：播放弹簧压缩动画（如果有动画组件）
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            //anim.SetTrigger("SpringTrigger"); // 需在Animator中创建对应的Trigger参数
        }

        // 示例2：播放跳跃音效（如果有AudioSource组件）
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    
    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (!isOneShot && other.CompareTag("Player"))
        {
            hasTriggered = false;
        }
    }*/
}