#nullable enable
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

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Owner != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", (this.Owner is IStateful<TThis>) || ((StateBase<TThis>) this.Owner).Activity is Activity_.Active or Activity_.Activating );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity_.Inactive );
            this.OnBeforeActivate( argument );
            this.Activity = Activity_.Activating;
            {
                this.OnActivate( argument );
            }
            this.Activity = Activity_.Active;
            this.OnAfterActivate( argument );
        }
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Owner != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", (this.Owner is IStateful<TThis>) || ((StateBase<TThis>) this.Owner).Activity is Activity_.Active or Activity_.Deactivating );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity_.Active );
            this.OnBeforeDeactivate( argument );
            this.Activity = Activity_.Deactivating;
            {
                this.OnDeactivate( argument );
            }
            this.Activity = Activity_.Inactive;
            this.OnAfterDeactivate( argument );
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            this.OnBeforeActivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            this.OnAfterActivateEvent?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivateEvent?.Invoke( argument );
        }

    }
}
