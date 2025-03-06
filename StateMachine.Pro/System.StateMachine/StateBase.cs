namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> where TThis : StateBase<TThis> {

        // Owner
        private protected abstract IStateful<TThis>? Owner { get; set; }
        // Stateful
        public abstract IStateful<TThis>? Stateful { get; }

        // OnAttach
        public abstract event Action<object?>? OnBeforeAttachEvent;
        public abstract event Action<object?>? OnAfterAttachEvent;
        public abstract event Action<object?>? OnBeforeDetachEvent;
        public abstract event Action<object?>? OnAfterDetachEvent;

        // Constructor
        public StateBase() {
        }

        // Attach
        internal abstract void Attach(IStateful<TThis> owner, object? argument);
        internal abstract void Detach(IStateful<TThis> owner, object? argument);

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected abstract void OnBeforeAttach(object? argument);
        protected abstract void OnAfterAttach(object? argument);

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected abstract void OnBeforeDetach(object? argument);
        protected abstract void OnAfterDetach(object? argument);

    }
    public abstract partial class StateBase<TThis> where TThis : StateBase<TThis> {
        public enum Activity_ {
            Inactive,
            Activating,
            Active,
            Deactivating,
        }

        // Activity
        public abstract Activity_ Activity { get; private protected set; }

        // OnActivate
        public abstract event Action<object?>? OnBeforeActivateEvent;
        public abstract event Action<object?>? OnAfterActivateEvent;
        public abstract event Action<object?>? OnBeforeDeactivateEvent;
        public abstract event Action<object?>? OnAfterDeactivateEvent;

        // Constructor
        //public StateBase() {
        //}

        // Activate
        internal abstract void Activate(object? argument);
        internal abstract void Deactivate(object? argument);

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected abstract void OnBeforeActivate(object? argument);
        protected abstract void OnAfterActivate(object? argument);

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected abstract void OnBeforeDeactivate(object? argument);
        protected abstract void OnAfterDeactivate(object? argument);

    }
}
