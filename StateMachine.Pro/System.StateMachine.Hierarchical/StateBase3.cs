namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class StateBase3<TThis> : StateBase2<TThis> where TThis : StateBase3<TThis> {

        // Activity
        public override Activity_ Activity { get; private protected set; } = Activity_.Inactive;

        // OnActivate
        public override event Action<object?>? OnBeforeActivateEvent;
        public override event Action<object?>? OnAfterActivateEvent;
        public override event Action<object?>? OnBeforeDeactivateEvent;
        public override event Action<object?>? OnAfterDeactivateEvent;

        // Constructor
        public StateBase3() {
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
        internal override void Activate(object? argument) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            OnBeforeActivate( argument );
            Activity = Activity_.Activating;
            {
                OnActivate( argument );
                if (Child != null) {
                    Child.Activate( argument );
                }
            }
            Activity = Activity_.Active;
            OnAfterActivate( argument );
        }
        internal override void Deactivate(object? argument) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"State {this} must be active" ).Valid( Activity is Activity_.Active );
            OnBeforeDeactivate( argument );
            Activity = Activity_.Deactivating;
            {
                if (Child != null) {
                    Child.Deactivate( argument );
                }
                OnDeactivate( argument );
            }
            Activity = Activity_.Inactive;
            OnAfterDeactivate( argument );
        }

        // OnActivate
        //protected override void OnActivate(object? argument) {
        //}
        protected override void OnBeforeActivate(object? argument) {
            OnBeforeActivateEvent?.Invoke( argument );
        }
        protected override void OnAfterActivate(object? argument) {
            OnAfterActivateEvent?.Invoke( argument );
        }

        // OnDeactivate
        //protected override void OnDeactivate(object? argument) {
        //}
        protected override void OnBeforeDeactivate(object? argument) {
            OnBeforeDeactivateEvent?.Invoke( argument );
        }
        protected override void OnAfterDeactivate(object? argument) {
            OnAfterDeactivateEvent?.Invoke( argument );
        }

    }
}
