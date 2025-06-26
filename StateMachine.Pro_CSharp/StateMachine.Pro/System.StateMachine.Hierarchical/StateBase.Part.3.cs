#nullable enable
namespace System.StateMachine.Hierarchical {
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
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

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
                if (this.Child != null) {
                    this.Child.Activate( argument );
                }
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
                if (this.Child != null) {
                    this.Child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
            }
            this.Activity = Activity_.Inactive;
            this.OnAfterDeactivate( argument );
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            this.OnBeforeActivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            this.OnAfterActivateCallback?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivateCallback?.Invoke( argument );
        }

    }
}
