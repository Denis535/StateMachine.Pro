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
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

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
            this.OnBeforeAttachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterAttach(object? argument) {
            this.OnAfterAttachCallback?.Invoke( argument );
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
            this.OnBeforeDetachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDetach(object? argument) {
            this.OnAfterDetachCallback?.Invoke( argument );
        }

    }
}
