using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]//attribute屬性  強制約束  強制要掛Animator  Unity內若要移除Animator會跳出警示窗     除非先把這個Script刪掉 才可以刪掉Animator
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthComponent))]
public class EnemyBehavior : MonoBehaviour {
    private Animator animator;
    private MeshFader meshFader;
    private AudioSource audioSource;    
    private HealthComponent healthComponent;
    [SerializeField]
    private AudioClip hurtclip;      //開放讓外部可以指定音樂
    [SerializeField]
    private AudioClip deadclip;      //開放讓外部可以指定音樂

    public EnemyData enemyData;

    public bool IsDead
    {
        get
        {
            return healthComponent.IsOver;
        }
    }

    private void Awake() {                             //只要繼承MonoBehavior 就不用建構子 用Awake()
        animator = GetComponent<Animator>();
        meshFader = GetComponent<MeshFader>();
        audioSource = GetComponent<AudioSource>();
        healthComponent = GetComponent<HealthComponent>();        
    }


    private void OnEnable()
    {
        StartCoroutine(meshFader.FadeIn());
    }
    //-----------------------------------------測試
    [ContextMenu("Test Execute")]
    private void TestExecute()
    {
        StartCoroutine(Execute(enemyData));
    }
    //-----------------------------------------
    public IEnumerator Execute(EnemyData enemtData)
    {
        healthComponent.Init(enemyData.health);
        while (IsDead == false)
        {
            yield return null;
        }
        animator.SetTrigger("die");
        audioSource.clip = deadclip;
        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return StartCoroutine(meshFader.FadeOut());
    }
    public void DoDamage(int attack)
    {
        animator.SetTrigger("hurt");
        audioSource.clip = hurtclip;
        audioSource.Play();
        healthComponent.Hurt(attack);
    }

    private void Update()
    {
        if (healthComponent.IsOver)
            return;
        if (Input.GetButtonDown("Fire1"))
        {
            DoDamage(10);
        }
    }


}
