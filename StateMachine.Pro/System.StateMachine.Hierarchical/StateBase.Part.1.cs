#nullable enable
namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {

        // Owner
        internal object? Owner { get; private set; }
        // Stateful
        public IStateful<TThis>? Stateful => (this.Owner as IStateful<TThis>) ?? (this.Owner as StateBase<TThis>)?.Stateful;

        // OnAttach
        public event Action<object?>? OnBeforeAttachEvent;
        public event Action<object?>? OnAfterAttachEvent;
        public event Action<object?>? OnBeforeDetachEvent;
        public event Action<object?>? OnAfterDetachEvent;

        // Constructor
        public StateBase() {
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
