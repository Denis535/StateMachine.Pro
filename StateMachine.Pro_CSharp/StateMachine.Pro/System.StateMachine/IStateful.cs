#nullable enable
namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateful<T> where T : notnull, StateBase<T> {

        // State
        protected T? State { get; set; }

        // SetState
        protected void SetState(T? state, object? argument, Action<T, object?>? callback);
        protected void AddState(T state, object? argument);
        protected void RemoveState(T state, object? argument, Action<T, object?>? callback);
        protected void RemoveState(object? argument, Action<T, object?>? callback);

        // Helpers
        protected static void SetState(IStateful<T> stateful, T? state, object? argument, Action<T, object?>? callback) {
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
            Assert.Argument.Valid( $"Argument 'stateful' ({stateful}) must have no {stateful.State} state", stateful.State == null );
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Stateful} stateful", state.Stateful == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be inactive", state.Activity == StateBase<T>.Activity_.Inactive );
            stateful.State = state;
            stateful.State.Attach( stateful, argument );
        }
        protected static void RemoveState(IStateful<T> stateful, T state, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'stateful' must be non-null", stateful != null );
            Assert.Argument.Valid( $"Argument 'stateful' ({stateful}) must have {state} state", stateful.State == state );
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have {stateful} stateful", state.Stateful == stateful );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be active", state.Activity == StateBase<T>.Activity_.Active );
            stateful.State.Detach( stateful, argument );
            stateful.State = null;
            callback?.Invoke( state, argument );
        }
        protected static void RemoveState(IStateful<T> stateful, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'stateful' must be non-null", stateful != null );
            Assert.Argument.Valid( $"Argument 'stateful' ({stateful}) must have state", stateful.State != null );
            stateful.RemoveState( stateful.State, argument, callback );
        }

    }
}
