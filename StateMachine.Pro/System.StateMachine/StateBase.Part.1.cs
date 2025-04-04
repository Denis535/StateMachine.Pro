#nullable enable
namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {

        // Owner
        internal IStateful<TThis>? Owner { get; private set; }
        // Stateful
        public IStateful<TThis>? Stateful => this.Owner;

        // OnAttach
        public event Action<object?>? OnBeforeAttachEvent;
        public event Action<object?>? OnAfterAttachEvent;
        public event Action<object?>? OnBeforeDetachEvent;
        public event Action<object?>? OnAfterDetachEvent;

        // Constructor
        public StateBase() {
        }

        // Attach
        internal void Attach(IStateful<TThis> owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"State {this} must have no owner", this.Owner == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity_.Inactive );
            {
                this.Owner = owner;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            this.Activate( argument );
        }
        internal void Detach(IStateful<TThis> owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"State {this} must have {owner} owner", this.Owner == owner );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity_.Active );
            this.Deactivate( argument );
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
            this.OnBeforeAttachEvent?.Invoke( argument );
        }
        protected virtual void OnAfterAttach(object? argument) {
            this.OnAfterAttachEvent?.Invoke( argument );
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
            this.OnBeforeDetachEvent?.Invoke( argument );
        }
        protected virtual void OnAfterDetach(object? argument) {
            this.OnAfterDetachEvent?.Invoke( argument );
        }

    }
}
