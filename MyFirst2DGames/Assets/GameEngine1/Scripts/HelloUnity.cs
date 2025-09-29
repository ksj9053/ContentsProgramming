using UnityEngine;

public class HelloUnity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 우리의 첫 프로그램!
        Debug.Log("안녕하세요! Unity와 C#의 세계에 오신 것을 환영합니다!");
        
        // 숫자도 출력해봅시다
        Debug.Log("오늘은 5주차 수업입니다!");
        
        // 간단한 계산도 가능합니다
        Debug.Log("10 + 20 = " + (10 + 20));
        
        // 이 GameObject의 이름도 출력해봅시다
        Debug.Log("제가 붙어있는 오브젝트 이름: " + gameObject.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
