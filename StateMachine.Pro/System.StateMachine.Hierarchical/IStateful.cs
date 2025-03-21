namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateful<T> where T : notnull, StateBase<T> {

        // State
        protected T? State { get; set; }

        // SetState
        protected void SetState(T? state, object? argument, Action<T>? callback);
        protected void AddState(T state, object? argument);
        protected internal void RemoveState(T state, object? argument, Action<T>? callback);

        // Helpers
        protected static void SetState(IStateful<T> stateful, T? state, object? argument, Action<T>? callback) {
            if (stateful.State != null) {
                stateful.RemoveState( stateful.State, argument, callback );
            }
            if (state != null) {
                stateful.AddState( state, argument );
            }
        }
        protected static void AddState(IStateful<T> stateful, T state, object? argument) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Operation.Valid( $"Stateful {stateful} must have no state", stateful.State == null );
            stateful.State = state;
            stateful.State.Attach( stateful, argument );
        }
        protected static void RemoveState(IStateful<T> stateful, T state, object? argument, Action<T>? callback) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Operation.Valid( $"Stateful {stateful} must have {state} state", stateful.State == state );
            stateful.State.Detach( stateful, argument );
            stateful.State = null;
            callback?.Invoke( state );
        }

    }
}
