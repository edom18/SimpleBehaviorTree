using System;
using UnityEngine;

namespace BehaviorTreeSample
{
    /// <summary>
    /// 実際のアクションを行うノード
    /// </summary>
    public class ActionNode : Node
    {
        private Func<GameObject, BehaviorStatus> _action;

        public ActionNode(Func<GameObject, BehaviorStatus> action)
        {
            _action = action;
        }

        public override BehaviorStatus OnUpdate()
        {
            base.OnUpdate();
            _status = _action.Invoke(Owner);
            return _status;
        }
    }
}
