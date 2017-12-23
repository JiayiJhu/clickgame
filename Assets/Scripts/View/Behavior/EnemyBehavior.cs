using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]//attribute屬性  強制約束  強制要掛Animator  Unity內若要移除Animator會跳出警示窗     除非先把這個Script刪掉 才可以刪掉Animator
public class EnemyBehavior : MonoBehaviour {
    private Animator animator;

    private void Awake() {                             //只要繼承MonoBehavior 就不用建構子 用Awake()
        animator = GetComponent<Animator>();           
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
