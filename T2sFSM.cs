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
            public bool Update(TContext context);
        }

        private readonly Stack<IState> StateStack = new();

        public void Update(TContext context)
        {
            while (StateStack.Count > 0)
            {
                if (StateStack.Pop().Update(context)) return;
            }
        }

        public void PushState(IState state)
        {
            StateStack.Push(state);
        }
    }
}