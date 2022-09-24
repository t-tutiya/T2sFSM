using UnityEngine;
using info.someiyoshino.Tsukasa;

public class Sample : MonoBehaviour
{
    private readonly T2sFSM<Sample> StateMachine = new();

    void Start()
    {
        StateMachine.PushState(new Wait());
    }

    void Update()
    {
        StateMachine.Update(this);
    }

    public class Wait : T2sFSM<Sample>.IState
    {
        public float ElapsedTime = 0.0f;

        public void Update(Sample context, T2sFSM<Sample> stateMachine)
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime >= 2.0f)
            {
                stateMachine.PushState(new Wait());
                stateMachine.PushState(new Output("Output:2"));
                stateMachine.PushState(new Output("Output:1"));
                stateMachine.EnableContinueLoop();
                return ;
            }
            stateMachine.PushState(this);
        }
    }

    public class Output : T2sFSM<Sample>.IState
    {
        readonly string Text;
        public Output(string text)
        {
            Text = text;
        }

        public void Update(Sample context, T2sFSM<Sample> stateMachine)
        {
            Debug.Log(Text);
            stateMachine.EnableContinueLoop();
        }
    }
}