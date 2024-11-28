namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;

    internal class Stateful<T> : IStateful<T> where T : StateBase<T> {

        // State
        T? IStateful<T>.State { get => State; set => State = value; }
        public T? State { get; private set; }

        // Constructor
        public Stateful() {
        }

        // SetState
        public void SetState(T? state, object? argument = null, Action<T>? dispose = null) {
            IStateful<T>.SetState( this, state, argument, dispose );
        }
        public void AddState(T state, object? argument = null) {
            IStateful<T>.AddState( this, state, argument );
        }
        public void RemoveState(T state, object? argument = null, Action<T>? dispose = null) {
            IStateful<T>.RemoveState( this, state, argument, dispose );
        }

    }
    internal abstract class State : StateBase2<State> {

        //public bool IsDisposed { get; private set; }

        public State() {
        }
        //public virtual void Dispose() {
        //    System.Assert.Operation.Message( $"Node {this} must be non-disposed" ).Valid( !IsDisposed );
        //    System.Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
        //    System.Assert.Operation.Message( $"Node {this} must have no stateful" ).Valid( Stateful == null );
        //    IsDisposed = true;
        //}

        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        protected override void OnActivate(object? argument) {
            TestContext.Out.WriteLine( "OnActivate: " + GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            TestContext.Out.WriteLine( "OnDeactivate: " + GetType().Name );
        }

    }
}
