## 🎯 Addressable

### ✅ **사용 배경**
제 게임에선 `ScriptableObject` 기반의 아이템 데이터 `(ItemData)`들을 런타임에 로드하여 플레이어 캐릭터에 적용합니다.  
물론 아이템 종류가 많지 않아 `Resources.Load`나 인스펙터를 통한 수동 할당으로도 충분히 운영 가능했지만, 학습 목적으로 `Unity Addressables` 시스템을 도입하였습니다.

### 🛠 **Addressables 사용 방식**
- `Start()`에서 `LoadItemDatas()` 메서드를 `async/await` 패턴으로 비동기 호출
- `"Sword"` 라벨이 붙은 `ItemData`들을 `Addressables.LoadAssetsAsync<ItemData>`를 통해 로드
- 로딩이 완료되면 플레이어 생성에 필요한 아이템 리스트로 사용됨
- 로드 실패 시 예외 처리 및 빈 리스트 처리 포함

```csharp
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
```


### 💡 **기대 효과**
- **실무 유연성 대비**: 이후 아이템이 수십~수백 개로 늘어나는 경우에도 무리 없이 확장 가능한 구조 경험
- **Addressables 시스템 익숙해짐**: 라벨 관리, 로드 상태 확인, 예외 처리 등 Addressables 전반 구조 학습