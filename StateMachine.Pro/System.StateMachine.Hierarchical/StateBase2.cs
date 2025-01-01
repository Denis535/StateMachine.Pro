namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class StateBase2<TThis> : StateBase<TThis> where TThis : StateBase2<TThis> {
        public enum Activity_ {
            Inactive,
            Activating,
            Active,
            Deactivating,
        }

        // Activity
        public Activity_ Activity { get; private set; } = Activity_.Inactive;

        // Child
        private protected abstract TThis? ChildInternal { get; }

        // OnActivate
        public event Action<object?>? OnBeforeActivateEvent;
        public event Action<object?>? OnAfterActivateEvent;
        public event Action<object?>? OnBeforeDeactivateEvent;
        public event Action<object?>? OnAfterDeactivateEvent;

        // Constructor
        public StateBase2() {
        }

        // Attach
        internal sealed override void Attach(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            base.Attach( owner, argument );
            Activate( argument );
        }
        internal sealed override void Detach(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Message( $"State {this} must be active" ).Valid( Activity is Activity_.Active );
            Deactivate( argument );
            base.Detach( owner, argument );
        }

        // Attach
        internal sealed override void Attach(TThis owner, object? argument) {
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            if (owner.Activity is Activity_.Active) {
                base.Attach( owner, argument );
                Activate( argument );
            } else {
                base.Attach( owner, argument );
            }
        }
        internal sealed override void Detach(TThis owner, object? argument) {
            if (owner.Activity is Activity_.Active) {
                Assert.Operation.Message( $"State {this} must be active" ).Valid( Activity is Activity_.Active );
                Deactivate( argument );
                base.Detach( owner, argument );
            } else {
                Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
                base.Detach( owner, argument );
            }
        }

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            OnBeforeActivate( argument );
            Activity = Activity_.Activating;
            {
                OnActivate( argument );
                if (ChildInternal != null) {
                    ChildInternal.Activate( argument );
                }
            }
            Activity = Activity_.Active;
            OnAfterActivate( argument );
        }
        private void Deactivate(object? argument) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"State {this} must be active" ).Valid( Activity is Activity_.Active );
            OnBeforeDeactivate( argument );
            Activity = Activity_.Deactivating;
            {
                if (ChildInternal != null) {
                    ChildInternal.Deactivate( argument );
                }
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
