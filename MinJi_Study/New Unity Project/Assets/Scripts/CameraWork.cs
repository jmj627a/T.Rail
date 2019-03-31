using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.myGame
{
    public class CameraWork : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("The distance in the local x-z plane to the target")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("The height we want the camera to be above the target")]
        [SerializeField]
        private float height = 3.0f;

        [Tooltip("The Smooth time lag for the height of the camera.")]
        [SerializeField]
        private float heightSmoothLag = 0.3f;

        [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        //네트워크 환경이 아는 곳에서 사용할 때 true, 테스트 씬이나 다른 시나리오??에서...
        //네트워크 기반 게임에서 실행할때 OnStartFollowing()호출. -> PlayerManager 스크립트 안에서 수행
        [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
        [SerializeField]
        private bool followOnStart = false;


        //타겟 위치
        Transform cameraTransform;


        // 타겟을 잃어 버렸거나 카메라가 전환 된 경우 내부적으로 다시 연결하기위한 플래그 유지
        bool isFollowing;


        // 현재 속도. 이 값은 호출 할 때마다 SmoothDamp()에 의해 수정됨.
        private float heightVelocity;


        //SmoothDamp()를 사용하여 도달하려는 위치
        private float targetHeight = 100000.0f;


        #endregion


        #region MonoBehaviour CallBacks


        void Start()
        {
            // 타겟을 따라가기 시작
            if (followOnStart)
            {
                OnStartFollowing();
            }
        }

        void LateUpdate()
        {
            //새 장면을로드 할 때마다 재연결
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }
            // only follow is explicitly declared
            if (isFollowing)
            {
                Apply();
            }
        }
        #endregion


        #region Public Methods

        public void OnStartFollowing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;
            //카메라 샷을 바로 잡는다.
            Cut();
        }

        #endregion


        #region Private Methods

        //부드럽게 타겟 따라가기
        void Apply()
        {
            Vector3 targetCenter = transform.position + centerOffset;
            // 현재와 타겟 회전 각도 계산
            float originalTargetAngle = transform.eulerAngles.y;
            float currentAngle = cameraTransform.eulerAngles.y;
            // 카메라가 잠겼을 때 진짜 타겟 앵글 적용
            float targetAngle = originalTargetAngle;
            currentAngle = targetAngle;
            targetHeight = targetCenter.y + height;

            // 높이
            float currentHeight = cameraTransform.position.y;
            currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref heightVelocity, heightSmoothLag);
            // 각도를 회전으로 변환하면 카메라의 위치가 바뀜
            Quaternion currentRotation = Quaternion.Euler(0, currentAngle, 0);
            cameraTransform.position = targetCenter;
            cameraTransform.position += currentRotation * Vector3.back * distance;
            // 카메라 높이 설정
            cameraTransform.position = new Vector3(cameraTransform.position.x, currentHeight, cameraTransform.position.z);
            // 항상 타겟을 보기
            SetUpRotation(targetCenter);
        }


        // 카메라를 지정된 타겟 및 중앙에 직접 배치
        void Cut()
        {
            float oldHeightSmooth = heightSmoothLag;
            heightSmoothLag = 0.001f;
            Apply();
            heightSmoothLag = oldHeightSmooth;
        }


        // 카메라 회전을 항상 타겟 뒤로
        void SetUpRotation(Vector3 centerPos)
        {
            Vector3 cameraPos = cameraTransform.position;
            Vector3 offsetToCenter = centerPos - cameraPos;
            //오직 y축회전
            Quaternion yRotation = Quaternion.LookRotation(new Vector3(offsetToCenter.x, 0, offsetToCenter.z));
            Vector3 relativeOffset = Vector3.forward * distance + Vector3.down * height;
            cameraTransform.rotation = yRotation * Quaternion.LookRotation(relativeOffset);
        }

        #endregion
    }
}