using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeSample
{
    /// <summary>
    /// デコレータのベースクラス
    /// </summary>
    public abstract class Decorator : Node
    {
        protected Node _targetNode;

        public Decorator(Node targetNode)
        {
            _targetNode = targetNode;
        }

        public override void OnStart()
        {
            base.OnStart();
            _targetNode.OnStart();
        }
    }
}
