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

        public bool Update(Sample context)
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime >= 2.0f)
            {
                context.StateMachine.PushState(new Wait());
                context.StateMachine.PushState(new Output("Output:2"));
                context.StateMachine.PushState(new Output("Output:1"));
                return false;
            }
            context.StateMachine.PushState(this);
            return true;
        }
    }

    public class Output : T2sFSM<Sample>.IState
    {
        string Text;
        public Output(string text)
        {
            Text = text;
        }

        public bool Update(Sample context)
        {
            Debug.Log(Text);
            return false;
        }
    }
}