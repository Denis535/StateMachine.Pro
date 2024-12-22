namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateful<T> where T : StateBase2<T> {

        // State
        protected T? State { get; set; }

        // SetState
        protected void SetState(T? state, object? argument, Action<T>? onRemoved);
        protected void AddState(T state, object? argument);
        protected void RemoveState(T state, object? argument, Action<T>? onRemoved);

        // Helpers
        protected static void SetState(IStateful<T> stateful, T? state, object? argument, Action<T>? onRemoved) {
            if (state != null) {
                Assert.Argument.Message( $"Argument 'state' ({state}) must be inactive" ).Valid( state.Activity is StateBase2<T>.Activity_.Inactive );
                if (stateful.State != null) {
                    stateful.RemoveState( stateful.State, argument, onRemoved );
                }
                stateful.AddState( state, argument );
            } else {
                if (stateful.State != null) {
                    stateful.RemoveState( stateful.State, argument, onRemoved );
                }
            }
        }
        protected static void AddState(IStateful<T> stateful, T state, object? argument) {
            Assert.Argument.Message( $"Argument 'state' must be non-null" ).NotNull( state != null );
            Assert.Argument.Message( $"Argument 'state' must be inactive" ).Valid( state.Activity is StateBase2<T>.Activity_.Inactive );
            Assert.Operation.Message( $"Stateful {stateful} must have no state" ).Valid( stateful.State == null );
            stateful.State = state;
            stateful.State.Attach( stateful, argument );
        }
        protected static void RemoveState(IStateful<T> stateful, T state, object? argument, Action<T>? onRemoved) {
            Assert.Argument.Message( $"Argument 'state' must be non-null" ).NotNull( state != null );
            Assert.Argument.Message( $"Argument 'state' must be active" ).Valid( state.Activity is StateBase2<T>.Activity_.Active );
            Assert.Operation.Message( $"Stateful {stateful} must have {state} state" ).Valid( stateful.State == state );
            stateful.State.Detach( stateful, argument );
            stateful.State = null;
            onRemoved?.Invoke( state );
        }

    }
}
