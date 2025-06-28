#nullable enable
namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {
        public enum Activity_ {
            Inactive,
            Activating,
            Active,
            Deactivating,
        }

        // Owner
        private object? Owner { get; set; }
        // Stateful
        public IStateful<TThis>? Stateful => (this.Owner as IStateful<TThis>) ?? (this.Owner as StateBase<TThis>)?.Stateful;
        private IStateful<TThis>? Stateful_NoRecursive => this.Owner as IStateful<TThis>;

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => this.Parent == null;
        public TThis Root => this.Parent?.Root ?? (TThis) this;

        // Parent
        public TThis? Parent => this.Owner as TThis;
        public IEnumerable<TThis> Ancestors {
            get {
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<TThis> AncestorsAndSelf => this.Ancestors.Prepend( (TThis) this );

        // Activity
        public Activity_ Activity { get; private set; } = Activity_.Inactive;

        // Child
        public TThis? Child { get; private set; }
        public IEnumerable<TThis> Descendants {
            get {
                if (this.Child != null) {
                    yield return this.Child;
                    foreach (var i in this.Child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<TThis> DescendantsAndSelf => this.Descendants.Prepend( (TThis) this );

        // Constructor
        public StateBase() {
        }

    }
    public abstract partial class StateBase<TThis> {

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Attach
        internal void Attach(IStateful<TThis> stateful, object? argument) {
            Assert.Argument.NotNull( $"Argument 'stateful' must be non-null", stateful != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Stateful_NoRecursive} stateful", this.Stateful_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity_.Inactive );
            {
                this.Owner = stateful;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            {
                this.Activate( argument );
            }
        }
        internal void Attach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Stateful_NoRecursive} stateful", this.Stateful_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity_.Inactive );
            {
                this.Owner = parent;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (parent.Activity is Activity_.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        internal void Detach(IStateful<TThis> stateful, object? argument) {
            Assert.Argument.NotNull( $"Argument 'stateful' must be non-null", stateful != null );
            Assert.Operation.Valid( $"State {this} must have {stateful} stateful", this.Stateful_NoRecursive == stateful );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity_.Active );
            {
                this.Deactivate( argument );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }
        internal void Detach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"State {this} must have {parent} parent", this.Parent == parent );
            if (parent.Activity is Activity_.Active) {
                Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity_.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity_.Inactive );
            }
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
    public abstract partial class StateBase<TThis> {

        // OnActivate
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Stateful_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Stateful_NoRecursive != null || this.Parent!.Activity is Activity_.Active or Activity_.Activating );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity_.Inactive );
            this.OnBeforeActivate( argument );
            this.Activity = Activity_.Activating;
            {
                this.OnActivate( argument );
                if (this.Child != null) {
                    this.Child.Activate( argument );
                }
            }
            this.Activity = Activity_.Active;
            this.OnAfterActivate( argument );
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Stateful_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Stateful_NoRecursive != null || this.Parent!.Activity is Activity_.Active or Activity_.Deactivating );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity_.Active );
            this.OnBeforeDeactivate( argument );
            this.Activity = Activity_.Deactivating;
            {
                if (this.Child != null) {
                    this.Child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
            }
            this.Activity = Activity_.Inactive;
            this.OnAfterDeactivate( argument );
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            this.OnBeforeActivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            this.OnAfterActivateCallback?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivateCallback?.Invoke( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        // SetChild
        protected void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback) {
            if (this.Child != null) {
                this.RemoveChild( this.Child, argument, callback );
            }
            if (child != null) {
                this.AddChild( child, argument );
            }
        }

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Stateful_NoRecursive} stateful", child.Stateful_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity_.Inactive );
            Assert.Operation.Valid( $"State {this} must have no {this.Child} child", this.Child == null );
            this.Child = child;
            this.Child.Attach( (TThis) this, argument );
        }

        // RemoveChild
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} parent", child.Parent == this );
            if (this.Activity == Activity_.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity_.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity_.Inactive );
            }
            Assert.Operation.Valid( $"State {this} must have {child} child", this.Child == child );
            this.Child.Detach( (TThis) this, argument );
            this.Child = null;
            callback?.Invoke( child, argument );
        }
        protected void RemoveChild(object? argument, Action<TThis, object?>? callback) {
            Assert.Operation.Valid( $"State {this} must have child", this.Child != null );
            this.RemoveChild( this.Child, argument, callback );
        }

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            if (this.Parent != null) {
                this.Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Assert.Operation.Valid( $"State {this} must have stateful", this.Stateful_NoRecursive != null );
                this.Stateful_NoRecursive.RemoveState( (TThis) this, argument, callback );
            }
        }

    }
}
