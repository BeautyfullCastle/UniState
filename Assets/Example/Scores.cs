using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UniState.Core;
using UnityEngine;

namespace Example
{
    using CollectionStoreType = ListStore<int>;
    
    public class Scores: MonoBehaviour
    {
        [SerializeField] private Text scoreText;

        private Action<ICollection<int>> scoreListener;
        
        private const string ScoreKey = "score";
        
        private IEnumerator Start()
        {
            var list = new List<int>();
            list.Add(0);
            var scoreStore = new CollectionStoreType(list);
            scoreListener = (value) =>
            {
                if (value is List<int> l)
                {
                    var sb = new StringBuilder();
                    l.ForEach(item => sb.Append(item).Append(", "));
                    this.scoreText.text = sb.ToString();
                }
                    
            };
            scoreStore.AddListener(scoreListener);
            
            StoreManager.Instance.RegisterStore(ScoreKey, scoreStore);

            yield return IncrementScoreEveryFiveSeconds();
        }

        private void OnDestroy()
        {
            if(StoreManager.Instance.GetStore(ScoreKey) is CollectionStoreType store)
                store.RemoveListener(scoreListener);
        }

        private IEnumerator IncrementScoreEveryFiveSeconds()
        {
            while (this.gameObject.activeSelf)
            {
                yield return new WaitForSeconds(0.5f);
                IncreaseScore(1);
            }

            yield break;
        }

        public void IncreaseScore(int amount)
        {
            var store = StoreManager.Instance.GetStore(ScoreKey);
            if (store is CollectionStoreType s)
            {
                s.Add(amount);
            }
        }
    }
}