namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;

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
    internal abstract class State : StateBase2<State> {

        //public bool IsDisposed { get; private set; }

        public State() {
        }
        //public virtual void Dispose() {
        //    System.Assert.Operation.Message( $"State {this} must be non-disposed" ).Valid( !IsDisposed );
        //    System.Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
        //    System.Assert.Operation.Message( $"State {this} must have no stateful" ).Valid( Stateful == null );
        //    IsDisposed = true;
        //}

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            TestContext.Out.WriteLine( "OnActivate: " + GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            TestContext.Out.WriteLine( "OnDeactivate: " + GetType().Name );
        }

    }
}
