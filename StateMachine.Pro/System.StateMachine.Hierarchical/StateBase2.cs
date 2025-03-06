namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class StateBase2<TThis> : StateBase<TThis> where TThis : StateBase2<TThis> {

        // Owner
        private protected override object? Owner { get; set; } = null;
        // Stateful
        public override IStateful<TThis>? Stateful => (Owner as IStateful<TThis>) ?? (Owner as StateBase<TThis>)?.Stateful;

        // OnAttach
        public override event Action<object?>? OnBeforeAttachEvent;
        public override event Action<object?>? OnAfterAttachEvent;
        public override event Action<object?>? OnBeforeDetachEvent;
        public override event Action<object?>? OnAfterDetachEvent;

        // Constructor
        public StateBase2() {
        }

        // Attach
        internal override void Attach(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Message( $"State {this} must have no owner" ).Valid( Owner == null );
            Owner = owner;
            OnBeforeAttach( argument );
            OnAttach( argument );
            OnAfterAttach( argument );
        }
        internal override void Detach(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Message( $"State {this} must have {owner} owner" ).Valid( Owner == owner );
            OnBeforeDetach( argument );
            OnDetach( argument );
            OnAfterDetach( argument );
            Owner = null;
        }

        // Attach
        internal override void Attach(TThis owner, object? argument) {
            Assert.Operation.Message( $"State {this} must have no owner" ).Valid( Owner == null );
            Owner = owner;
            OnBeforeAttach( argument );
            OnAttach( argument );
            OnAfterAttach( argument );
        }
        internal override void Detach(TThis owner, object? argument) {
            Assert.Operation.Message( $"State {this} must have {owner} owner" ).Valid( Owner == owner );
            OnBeforeDetach( argument );
            OnDetach( argument );
            OnAfterDetach( argument );
            Owner = null;
        }

        // OnAttach
        //protected override void OnAttach(object? argument) {
        //}
        protected override void OnBeforeAttach(object? argument) {
            OnBeforeAttachEvent?.Invoke( argument );
        }
        protected override void OnAfterAttach(object? argument) {
            OnAfterAttachEvent?.Invoke( argument );
        }

        // OnDetach
        //protected override void OnDetach(object? argument) {
        //}
        protected override void OnBeforeDetach(object? argument) {
            OnBeforeDetachEvent?.Invoke( argument );
        }
        protected override void OnAfterDetach(object? argument) {
            OnAfterDetachEvent?.Invoke( argument );
        }

    }
}
