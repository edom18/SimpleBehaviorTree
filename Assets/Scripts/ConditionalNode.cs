using System;

namespace BehaviorTreeSample
{
    public class ConditionalNode : Node
    {
        private Func<BehaviorStatus> _condition;

        public ConditionalNode(Func<BehaviorStatus> condition)
        {
            _condition = condition;
        }

        public override BehaviorStatus OnUpdate()
        {
            return _condition.Invoke();
        }
    }
}
