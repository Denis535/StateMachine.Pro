namespace System.StateMachine {
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
        internal override void Attach(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            base.Attach( owner, argument );
            Activate( argument );
        }
        internal override void Detach(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Message( $"State {this} must be active" ).Valid( Activity is Activity_.Active );
            Deactivate( argument );
            base.Detach( owner, argument );
        }

        // Activate
        internal override void Activate(object? argument) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            OnBeforeActivate( argument );
            Activity = Activity_.Activating;
            {
                OnActivate( argument );
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
