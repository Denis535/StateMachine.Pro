namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> {
        public enum Activity_ {
            Inactive,
            Activating,
            Active,
            Deactivating,
        }

        // Activity
        public Activity_ Activity { get; private set; } = Activity_.Inactive;

        // OnActivate
        public event Action<object?>? OnBeforeActivateEvent;
        public event Action<object?>? OnAfterActivateEvent;
        public event Action<object?>? OnBeforeDeactivateEvent;
        public event Action<object?>? OnAfterDeactivateEvent;

        // Constructor
        //public StateBase() {
        //}

        // Attach
        internal void Attach(IStateful<TThis> owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"State {this} must have no owner", Owner == null );
            Assert.Operation.Valid( $"State {this} must be inactive", Activity is Activity_.Inactive );
            {
                Owner = owner;
                OnBeforeAttach( argument );
                OnAttach( argument );
                OnAfterAttach( argument );
            }
            Activate( argument );
        }
        internal void Detach(IStateful<TThis> owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"State {this} must have {owner} owner", Owner == owner );
            Assert.Operation.Valid( $"State {this} must be active", Activity is Activity_.Active );
            Deactivate( argument );
            {
                OnBeforeDetach( argument );
                OnDetach( argument );
                OnAfterDetach( argument );
                Owner = null;
            }
        }

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", Owner != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", (Owner is IStateful<TThis>) || ((StateBase<TThis>) Owner).Activity is Activity_.Active or Activity_.Activating );
            Assert.Operation.Valid( $"State {this} must be inactive", Activity is Activity_.Inactive );
            OnBeforeActivate( argument );
            Activity = Activity_.Activating;
            {
                OnActivate( argument );
            }
            Activity = Activity_.Active;
            OnAfterActivate( argument );
        }
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", Owner != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", (Owner is IStateful<TThis>) || ((StateBase<TThis>) Owner).Activity is Activity_.Active or Activity_.Deactivating );
            Assert.Operation.Valid( $"State {this} must be active", Activity is Activity_.Active );
            OnBeforeDeactivate( argument );
            Activity = Activity_.Deactivating;
            {
                OnDeactivate( argument );
            }
            Activity = Activity_.Inactive;
            OnAfterDeactivate( argument );
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            OnBeforeActivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            OnAfterActivateEvent?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            OnBeforeDeactivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            OnAfterDeactivateEvent?.Invoke( argument );
        }

    }
}
