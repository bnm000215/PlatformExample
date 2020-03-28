using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayIngredients.Events
{
    public class OnGameManagerLevelStart : EventBase
    {
        public enum GameManagerLevelType
        {
            MainMenu,
            GameLevel
        }

        public GameManagerLevelType levelType { get { return m_LevelType; } }

        [SerializeField]
        protected GameManagerLevelType m_LevelType = GameManagerLevelType.GameLevel;

        string m_Message;

        [ReorderableList]
        public Callable[] OnMessageRecieved;

        void OnEnable()
        {
            m_Message = GetMessage(m_LevelType);
            Messager.RegisterMessage(m_Message, Execute);
        }

        void OnDisable()
        {
            Messager.RemoveMessage(m_Message, Execute);
        }

        static string GetMessage(GameManagerLevelType type)
        {
            return string.Empty;
        }

        void Execute(GameObject instigator = null)
        {
            try
            {
                Callable.Call(OnMessageRecieved, instigator);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError(string.Format("OnMessageEvent : Exception Caught while catching message '{0}' on Object '{1}'", m_Message, gameObject.name));
                UnityEngine.Debug.LogException(e);
            }
        }


    }
}

