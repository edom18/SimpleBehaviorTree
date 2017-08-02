using System;
using UnityEngine;

namespace BehaviorTreeSample
{
    public class ConditionalNode : Node
    {
        private Func<GameObject, BehaviorStatus> _condition;

        public ConditionalNode(Func<GameObject, BehaviorStatus> condition)
        {
            _condition = condition;
        }

        public override BehaviorStatus OnUpdate()
        {
            base.OnUpdate();
            _status = _condition.Invoke(Owner);
            return _status;
        }
    }
}
