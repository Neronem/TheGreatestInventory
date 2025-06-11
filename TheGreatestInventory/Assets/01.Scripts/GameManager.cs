using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

/// <summary>
/// 게임 전반의 핵심 관리 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤
    [field: SerializeField] public Character player { get; private set; } // 캐릭터 생성

    public List<ItemData> itemDatas; // 로드할 아이템 데이터들 (player에 넣는)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private async void Start() // Addressable 써야해서 async
    {
        // Addressable 비동기 로드 대기
        await LoadItemDatas();

        // 로드가 완료되면 데이터세팅
        SetData();
    }
    
    /// <summary>
    /// Addressable 아이템 로딩
    /// </summary>
    private async Task LoadItemDatas()
    {
        var handle = Addressables.LoadAssetsAsync<ItemData>("Sword", null); // Sword라벨 ItemData들 불러옴
        await handle.Task; // 완료될때까지 대기

        if (handle.Status == AsyncOperationStatus.Succeeded) // 완료 시
        {
            itemDatas = new List<ItemData>(handle.Result); // itemDatas에 넣기
            Util.Log($"Addressable에서 아이템 데이터 {itemDatas.Count}개 로드 완료");
        }
        else
        { // 예외처리
            Util.Log("아이템 데이터 로드 실패");
            itemDatas = new List<ItemData>(); 
        }
    }

    private void SetData()
    {
        // player 생성
        player = new Character("낭만용사", 1, "얼큰오이티라미수 \nvs \n제티마라탕", 10, 100, 5, 5, 100, 10, itemDatas);

        // 각 UI들 세팅
        UIManager.instance.MainMenu.SetUIMainMenu(player);
        UIManager.instance.Status.SetUIStatus(player);
        UIManager.instance.Inventory.SetInventory(player);
    }
}