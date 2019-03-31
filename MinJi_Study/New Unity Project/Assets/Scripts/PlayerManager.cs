using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.myGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region IPunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                //우리는 이 플레이어를 소유중- 다른사람에게 우리의 데이터를 send
                stream.SendNext(IsFiring);  //총알을 발사 했는지
                stream.SendNext(Health);    //체력 동기화
            }
            else
            {
                //네트워크 플레이어, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }

        }
        #endregion

        #region Private Fields

        [Tooltip("The Beams GameObject to control")]
        [SerializeField]
        private GameObject beams;

        //true - 플레이어가 발사하면
        bool IsFiring;
        #endregion

        #region Public Fields
        [Tooltip("The current Health of our player")]
        public float Health = 1f;

        [Tooltip("The local player instance. Use this to know if the local plater is represented int the Scene")]
        public static GameObject LocalPlayerInstance;

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        private GameObject playerUiPrefab;
        #endregion

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            if(beams == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }

            //used in GameManager.cs : 레벨이 동기화 될 때 인스턴스화를 방지하기 위해 localPlayer 인스턴스를 추적함.?
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // 인스턴스가 레벨 동기화를 견뎌 낼 수 있도록 로드시 파괴하지 않도록 플래그를 지정하여 레벨 로드시 원활히 제공.?
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
            if(_cameraWork != null)
            {
                if(photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }

            if(playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }

            //유니티 버전에 따라 다른 코드 필요. 로딩되는 레벨 감시.
#if UNITY_5_4_OR_NEWER
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
            {
                this.CalledOnLevelWasLoaded(scene.buildIndex);
            };
#endif
        }

        private void Update()
        {
            if(photonView.IsMine)
            {
                ProcessInputs();
            }

            if(beams != null && IsFiring != beams.activeSelf)
            {
                beams.SetActive(IsFiring);
            }

            if(Health <= 0.0f)
            {
                GameManager.Instance.LeaveRoom();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f;
        }

        void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f * Time.deltaTime;
        }
        
        //5.4보다 낮은버전이라면 유니티 콜백인 OnLevelWasLoaded사용.
        //5.4이상을 사용하면 OnLevelWasLoaded는 사용불가. SceneManagement시스템 사용.
#if !UNITY_5_4_OR_NEWER
        
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
        
#endif

        void CalledOnLevelWasLoaded(int level)
        {
            //현재 플레이어의 위치를 따라 우리가 어떤 것을 치는지 알아보기
            if(!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                GameObject _uiGo = Instantiate(this.playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);

                transform.position = new Vector3(0f, 5f, 0f);
            }
        }

        #endregion

        #region Custom
        void ProcessInputs()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if(IsFiring)
                {
                    IsFiring = false;
                }
            }
        }
#endregion
    }
}