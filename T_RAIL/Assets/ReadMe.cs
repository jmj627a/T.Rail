

public class ReadMe
{



    // [ 물리 엔진]

    // 1.Rigidbody가 없는 고정 충돌체 -> 움직이면 CPU에 부하가 생기게 됨
    // 그러니까 rigidbody 추가하고 iskinematic 옵션 사용

    // 2. 물리처리 시 태그보다 레이어 처리가 더 유리!
    // 성능과 메모리 측면에서 장점

    // 3. 메쉬 콜라이더 절-대 사용 X

    // 4. raycast와 sphere check같은 충돌 감지 요소 최소화
    // ex) UI 에 image 오브젝트 안에도 raycast 요소 있음

    // Fixed Update 주기조절 ★
    // TimeManager에서 수정 가능한데 게임에 따라 0.2초로 수정해도 문제 없음


    // [셰이더]

    // 셰이더 실수 연산자ㅏ는 float사용 X 
    //ㅇ float : 32 bit - 버텍스 변환에 사용.아주 느린성능(픽셀 셰이더에서의 사용은 피함)

    //    ㅇ Half : 16bit - 텍스쳐 uv에 적합.대략 2배 빠름

    //    ㅇ fixed : 10bit - 컬러, 라이트 계산과 같은 고성능 연산에 적합.대략 4배빠름

    // [ 컬링]

    // 유니티의 각 Layer별로 컬링 거리 설정 가능
    // 그러니까 뒤에 산같은 배경들에 각각 layer구분해주고 각 layer별로 컬링 거리 설정
    // 멀리보이는 산같은건 거리를 멀게 설정하고 중요도가 낮은 풀이나 나무는 컬링거리를 짧게

    // 오클루젼 컬링 window->occlusion culling


    // [리소스]
    // Resource. 이렇게 로드하는건 다른함수에 비해서 훨씬 느리니까
    // 아이템 창 만들 때 resource 필요한건 미리 다 캐싱해놓고 쓰자
    //   - Import 시에 언제나 "Optimize Mesh" 옵션을 사용한다.
    //-> 변환 전/ 변환 후 버텍스 캐쉬를 최적화 해준다




    // 그리고 문자열은 readonly혹은 const 키워드를 써서 가비지 컬렉션에 안먹히게 사용ㅎㅏ자

    //모든 비교문에서.equals()를 사용하도록 하자. == 구문으로 사용하면 임시적인 메모리가 나오게 되며
    // 가비지 컬렉션의 먹이를 준다.

    ////////////////////////////////////////////////////////


    //    - Magnitude 보다 sqrMagnitude를 사용하여 비교해서 쓴다. (제곱근 계산 X)

    // -> Unity 공식 문서에도 써져있다.

    // --> If you only need to compare magnitudes of some vectors, you can compare squared magnitudes of them using sqrMagnitude (computing squared magnitudes is faster).

    //    - 나눗셈 말고 곱셈의 사용.

    // -> 나눗셈은 곱셈보다 연산속도가 월등히 느리다 100 / 10 이런식으로 사용하지말고 100 * 0.1 을 사용하라는 소리.


    //- 삼각함수의 값은 상수로 저장하고 사용하는게 좋다.


    //출처: https://vallista.tistory.com/entry/Unity-게임-최적화-기법 [VallistA]

    ////////////////////////////////////////////////////////

}
