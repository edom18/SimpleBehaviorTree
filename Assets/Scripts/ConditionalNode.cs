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
            return _condition.Invoke(Owner);
        }
    }
}
