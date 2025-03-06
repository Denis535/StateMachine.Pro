namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Stateful : IStateful<State> {

        // State
        State? IStateful<State>.State { get => State; set => State = value; }
        public State? State { get; private set; }

        // Constructor
        public Stateful() {
        }

        // SetState
        public void SetState(State? state, object? argument, Action<State>? callback) {
            IStateful<State>.SetState( this, state, argument, callback );
        }
        public void AddState(State state, object? argument) {
            IStateful<State>.AddState( this, state, argument );
        }
        public void RemoveState(State state, object? argument, Action<State>? callback) {
            IStateful<State>.RemoveState( this, state, argument, callback );
        }

    }
}
