//T2sFSM
//Copyright (c) 2022 tsukasa TSUCHIYA(T2/t_tutiya)
//This software is released under the MIT License.
//http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;

namespace info.someiyoshino.Tsukasa
{
    //ツカサ式スタック型ステートマシン（Tsukasa system of Stack-Based FSM）
    public class T2sFSM<TContext>
    {
        public interface IState
        {
            public void Update(TContext context, T2sFSM<TContext> StateMachine);
        }

        private readonly Stack<IState> stateStack = new();

        private bool exitLoop;

        private int debugCounter;

        public void Update(TContext context)
        {
            debugCounter = 0;
            while (stateStack.TryPop(out IState result))
            {
                exitLoop = true;

                result.Update(context, this);

                if (exitLoop) break;

                debugCounter++;

                if (debugCounter > 100)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.Break();
#endif
                    return;
                }
            }
        }

        public void PushState(IState state)
        {
            stateStack.Push(state);
        }

        public void EnableContineLoop()
        {
            exitLoop = false;
        }
    }
}