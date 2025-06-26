namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Stateful : IStateful<State> {

        // State
        State? IStateful<State>.State { get => this.State; set => this.State = value; }
        public State? State { get; private set; }

        // Constructor
        public Stateful() {
        }

        // SetState
        public void SetState(State? state, object? argument, Action<State, object?>? callback) {
            IStateful<State>.SetState( this, state, argument, callback );
        }
        public void AddState(State state, object? argument) {
            IStateful<State>.AddState( this, state, argument );
        }
        public void RemoveState(State state, object? argument, Action<State, object?>? callback) {
            IStateful<State>.RemoveState( this, state, argument, callback );
        }
        public void RemoveState(object? argument, Action<State, object?>? callback) {
            IStateful<State>.RemoveState( this, argument, callback );
        }

    }
}
