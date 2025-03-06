namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public abstract partial class StateBase<TThis> where TThis : StateBase<TThis> {

        // Owner
        private protected abstract object? Owner { get; set; }
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

        // Attach
        internal abstract void Attach(TThis owner, object? argument);
        internal abstract void Detach(TThis owner, object? argument);

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected abstract void OnBeforeAttach(object? argument);
        protected abstract void OnAfterAttach(object? argument);

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected abstract void OnBeforeDetach(object? argument);
        protected abstract void OnAfterDetach(object? argument);

    }
    public abstract partial class StateBase<TThis> {

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public abstract bool IsRoot { get; }
        public abstract TThis Root { get; }

        // Parent
        public abstract TThis? Parent { get; }
        public abstract IEnumerable<TThis> Ancestors { get; }
        public abstract IEnumerable<TThis> AncestorsAndSelf { get; }

        // Child
        public abstract TThis? Child { get; private protected set; }
        public abstract IEnumerable<TThis> Descendants { get; }
        public abstract IEnumerable<TThis> DescendantsAndSelf { get; }

        // Constructor
        //public StateBase() {
        //}

        // SetChild
        protected abstract void SetChild(TThis? child, object? argument, Action<TThis>? callback);
        protected abstract void AddChild(TThis child, object? argument);
        protected abstract void RemoveChild(TThis child, object? argument, Action<TThis>? callback);
        protected abstract void RemoveSelf(object? argument, Action<TThis>? callback);

    }
    public abstract partial class StateBase<TThis> {
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
