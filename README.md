# UniState


## English


### Description
UniState is a Unity state management tool inspired by Zustand.

It allows developers to create Stores, register them with the StoreManager, and receive events when the state changes.

This tool aims to simplify state management in Unity projects, providing a more streamlined and effective development experience.


### Usage
To see how UniState is used, refer to the `Score` class example located in the `Assets/Example` directory of this project.

It demonstrates how to integrate and utilize state management within your Unity games or applications.

```csharp

// Make a store and register it.
var scoreStore = new Store<int>(0);
scoreListener = (value) => this.scoreText.text = value.ToString();
scoreStore.AddListener(scoreListener);
            
StoreManager.Instance.RegisterStore(ScoreKey, scoreStore);

---

// Get the store and update the state.
var scoreStore = StoreManager.Instance.GetStore<int>(ScoreKey);
if (scoreStore != null)
{
	int currentScore = scoreStore.GetState();
	scoreStore.UpdateState(currentScore + amount);
}

---

// Remove the listener
StoreManager.Instance.GetStore<int>(ScoreKey)?.RemoveListener(scoreListener);

```

### License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 한국어


### 설명
UniState는 Zustand 스타일로 Unity에서 상태를 관리하는 도구입니다.

개발자가 스토어를 만들고 StoreManager에 등록하여 상태가 변경될 때 이벤트를 받을 수 있습니다.

이 도구는 Unity 프로젝트의 상태 관리를 단순화하여 보다 효율적인 개발 경험을 제공하는 것을 목표로 합니다.


### 사용 방법
UniState의 사용 방법을 보려면 이 프로젝트의 `Assets/Example` 디렉토리에 위치한 `Score` 클래스 예제를 참조하세요.

이 예제는 Unity 게임이나 애플리케이션 내에서 상태 관리를 통합하고 활용하는 방법을 보여줍니다.

```csharp

// 스토어를 만들고 등록
var scoreStore = new Store<int>(0);
scoreListener = (value) => this.scoreText.text = value.ToString();
scoreStore.AddListener(scoreListener);
            
StoreManager.Instance.RegisterStore(ScoreKey, scoreStore);

---

// 스토어를 가져와 상태 업데이트
var scoreStore = StoreManager.Instance.GetStore<int>(ScoreKey);
if (scoreStore != null)
{
	int currentScore = scoreStore.GetState();
	scoreStore.UpdateState(currentScore + amount);
}

---

// 리스너 해제
StoreManager.Instance.GetStore<int>(ScoreKey)?.RemoveListener(scoreListener);

```



### 라이선스
이 프로젝트는 MIT 라이선스에 따라 라이선스가 부여됩니다 - 자세한 내용은 [LICENSE](LICENSE) 파일을 참조하세요.
