namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {

        // Owner
        private object? Owner { get; set; }
        // Stateful
        public IStateful<TThis>? Stateful => (Owner as IStateful<TThis>) ?? (Owner as StateBase<TThis>)?.Stateful;

        // OnAttach
        public event Action<object?>? OnBeforeAttachEvent;
        public event Action<object?>? OnAfterAttachEvent;
        public event Action<object?>? OnBeforeDetachEvent;
        public event Action<object?>? OnAfterDetachEvent;

        // Constructor
        public StateBase() {
        }

        // Attach
        private void AttachBase(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Valid( $"State {this} must have no owner", Owner == null );
            Owner = owner;
            OnBeforeAttach( argument );
            OnAttach( argument );
            OnAfterAttach( argument );
        }
        private void DetachBase(IStateful<TThis> owner, object? argument) {
            Assert.Operation.Valid( $"State {this} must have {owner} owner", Owner == owner );
            OnBeforeDetach( argument );
            OnDetach( argument );
            OnAfterDetach( argument );
            Owner = null;
        }

        // Attach
        private void AttachBase(TThis owner, object? argument) {
            Assert.Operation.Valid( $"State {this} must have no owner", Owner == null );
            Owner = owner;
            OnBeforeAttach( argument );
            OnAttach( argument );
            OnAfterAttach( argument );
        }
        private void DetachBase(TThis owner, object? argument) {
            Assert.Operation.Valid( $"State {this} must have {owner} owner", Owner == owner );
            OnBeforeDetach( argument );
            OnDetach( argument );
            OnAfterDetach( argument );
            Owner = null;
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
            OnBeforeAttachEvent?.Invoke( argument );
        }
        protected virtual void OnAfterAttach(object? argument) {
            OnAfterAttachEvent?.Invoke( argument );
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
            OnBeforeDetachEvent?.Invoke( argument );
        }
        protected virtual void OnAfterDetach(object? argument) {
            OnAfterDetachEvent?.Invoke( argument );
        }

    }
}
