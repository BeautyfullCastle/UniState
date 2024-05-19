using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UniState.Core;
using UnityEngine;

namespace UniState.Example
{
    using CollectionStoreType = ListStore<int>;
    
    public class Scores: MonoBehaviour
    {
        [SerializeField] private Text text;

        private Action<List<int>> listener;
        
        private const string key = "scores";
        
        private IEnumerator Start()
        {
            var scoreStore = new CollectionStoreType(new List<int>{0, 0});
            listener = (list) =>
            {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("[");
                    for (int i = 0; i < list.Count; i++)
                    {
                        stringBuilder.Append(list[i]);
                        if(i < list.Count -1)
                            stringBuilder.Append(", ");
                    }
                    stringBuilder.Append("]");
                    text.text = stringBuilder.ToString();
            };
            scoreStore.AddListener(listener);
            
            StoreManager.Instance.RegisterStore(key, scoreStore);

            yield return IncrementScores();
        }

        private void OnDestroy()
        {
            if(StoreManager.Instance.GetStore(key) is CollectionStoreType listStore)
                listStore.RemoveListener(listener);
        }

        private IEnumerator IncrementScores()
        {
            var waiter = new WaitForSeconds(0.1f);
            while (this.gameObject.activeSelf)
            {
                yield return waiter;
                IncreaseScore(0, 1);
                IncreaseScore(1, 2);
            }

            yield break;
        }

        private void IncreaseScore(int index, int amount)
        {
            if (StoreManager.Instance.GetStore(key) is CollectionStoreType listStore)
            {
                listStore[index] += amount;
            }
        }
        
        public void IncreaseScoreAtZero(int amount)
        {
            IncreaseScore(0, amount);
        }
        
        public void IncreaseScoreAtOne(int amount)
        {
            IncreaseScore(1, amount);
        }
    }
}