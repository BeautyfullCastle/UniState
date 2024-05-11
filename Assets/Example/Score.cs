using System;
using UnityEngine.UI;
using System.Collections;
using UniState.Core;
using UnityEngine;

namespace Example
{
    public class Score: MonoBehaviour
    {
        [SerializeField] private Text scoreText;

        private Action<int> scoreListener;
        
        private const string ScoreKey = "score";
        

        private IEnumerator Start()
        {
            var scoreStore = new Store<int>(0);
            scoreListener = (value) => this.scoreText.text = value.ToString();
            scoreStore.AddListener(scoreListener);
            
            StoreManager.Instance.RegisterStore(ScoreKey, scoreStore);

            yield return IncrementScoreEveryFiveSeconds();
        }

        private void OnDestroy()
        {
            StoreManager.Instance.GetStore<int>(ScoreKey)?.RemoveListener(scoreListener);
        }

        private IEnumerator IncrementScoreEveryFiveSeconds()
        {
            while (this.gameObject.activeSelf)
            {
                yield return new WaitForSeconds(0.1f);
                IncreaseScore(1);
            }

            yield break;
        }

        public void IncreaseScore(int amount)
        {
            var scoreStore = StoreManager.Instance.GetStore<int>(ScoreKey);
            if (scoreStore != null)
            {
                int currentScore = scoreStore.GetState();
                scoreStore.UpdateState(currentScore + amount);
            }
        }
    }
}