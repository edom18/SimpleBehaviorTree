using System;

namespace BehaviorTreeSample
{
    /// <summary>
    /// 実際のアクションを行うノード
    /// </summary>
    public class ActionNode : Node
    {
        private Func<BehaviorStatus> _action;

        public ActionNode(Func<BehaviorStatus> action)
        {
            _action = action;
        }

        public override BehaviorStatus OnUpdate()
        {
            return _action.Invoke();
        }
    }
}
