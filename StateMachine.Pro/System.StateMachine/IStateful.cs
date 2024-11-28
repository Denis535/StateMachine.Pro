namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateful<T> where T : StateBase<T> {

        // State
        protected T? State { get; set; }

        // SetState
        protected void SetState(T? state, object? argument, Action<T>? dispose);
        protected void AddState(T state, object? argument);
        protected void RemoveState(T state, object? argument, Action<T>? dispose);

        // Helpers
        protected static void SetState(IStateful<T> stateful, T? state, object? argument, Action<T>? dispose) {
            if (state != null) {
                Assert.Argument.Message( $"Argument 'state' ({state}) must be inactive" ).Valid( state.Activity is StateBase<T>.Activity_.Inactive );
                if (stateful.State != null) {
                    stateful.RemoveState( stateful.State, argument, dispose );
                }
                stateful.AddState( state, argument );
            } else {
                if (stateful.State != null) {
                    stateful.RemoveState( stateful.State, argument, dispose );
                }
            }
        }
        protected static void AddState(IStateful<T> stateful, T state, object? argument) {
            Assert.Argument.Message( $"Argument 'state' must be non-null" ).NotNull( state != null );
            Assert.Argument.Message( $"Argument 'state' must be inactive" ).Valid( state.Activity is StateBase<T>.Activity_.Inactive );
            Assert.Operation.Message( $"Stateful {stateful} must have no state" ).Valid( stateful.State == null );
            stateful.State = state;
            stateful.State.Attach( stateful, argument );
        }
        protected static void RemoveState(IStateful<T> stateful, T state, object? argument, Action<T>? dispose) {
            Assert.Argument.Message( $"Argument 'state' must be non-null" ).NotNull( state != null );
            Assert.Argument.Message( $"Argument 'state' must be active" ).Valid( state.Activity is StateBase<T>.Activity_.Active );
            Assert.Operation.Message( $"Stateful {stateful} must have {state} state" ).Valid( stateful.State == state );
            stateful.State.Detach( stateful, argument );
            stateful.State = null;
            dispose?.Invoke( state );
        }

    }
}
