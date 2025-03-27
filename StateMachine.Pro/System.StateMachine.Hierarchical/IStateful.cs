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
            Assert.Argument.NotNull( $"Argument 'stateful' must be non-null", stateful != null );
            if (stateful.State != null) {
                stateful.RemoveState( stateful.State, argument, callback );
            }
            if (state != null) {
                stateful.AddState( state, argument );
            }
        }
        protected static void AddState(IStateful<T> stateful, T state, object? argument) {
            Assert.Argument.NotNull( $"Argument 'stateful' must be non-null", stateful != null );
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Operation.Valid( $"Stateful {stateful} must have no state", stateful.State == null );
            Assert.Operation.Valid( $"State {state} must have no owner", state.Owner == null );
            Assert.Operation.Valid( $"State {state} must be inactive", state.Activity == StateBase<T>.Activity_.Inactive );
            stateful.State = state;
            stateful.State.Attach( stateful, argument );
        }
        protected static void RemoveState(IStateful<T> stateful, T state, object? argument, Action<T>? callback) {
            Assert.Argument.NotNull( $"Argument 'stateful' must be non-null", stateful != null );
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Operation.Valid( $"Stateful {stateful} must have {state} state", stateful.State == state );
            Assert.Operation.Valid( $"State {state} must have owner", state.Owner == stateful );
            Assert.Operation.Valid( $"State {state} must be active", state.Activity == StateBase<T>.Activity_.Active );
            stateful.State.Detach( stateful, argument );
            stateful.State = null;
            callback?.Invoke( state );
        }

    }
}
