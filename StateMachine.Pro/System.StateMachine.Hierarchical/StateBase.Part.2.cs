namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class StateBase<TThis> {

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => Parent == null;
        public TThis Root => Parent?.Root ?? (TThis) this;

        // Parent
        public TThis? Parent => Owner as TThis;
        public IEnumerable<TThis> Ancestors {
            get {
                if (Parent != null) {
                    yield return Parent;
                    foreach (var i in Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<TThis> AncestorsAndSelf => Ancestors.Prepend( (TThis) this );

        // Child
        public TThis? Child { get; private set; }
        public IEnumerable<TThis> Descendants {
            get {
                if (Child != null) {
                    yield return Child;
                    foreach (var i in Child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<TThis> DescendantsAndSelf => Descendants.Prepend( (TThis) this );

        // Constructor
        //public StateBase() {
        //}

        // SetChild
        protected void SetChild(TThis? child, object? argument, Action<TThis>? callback) {
            if (Child != null) {
                RemoveChild( Child, argument, callback );
            }
            if (child != null) {
                AddChild( child, argument );
            }
        }
        protected void AddChild(TThis child, object? argument) {
            Debug2.Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Debug2.Assert.Operation.Valid( $"State {this} must have no child {child} state", Child == null );
            Child = child;
            Child.Attach( (TThis) this, argument );
        }
        protected void RemoveChild(TThis child, object? argument, Action<TThis>? callback) {
            Debug2.Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Debug2.Assert.Operation.Valid( $"State {this} must have child {child} state", Child == child );
            Child.Detach( (TThis) this, argument );
            Child = null;
            callback?.Invoke( child );
        }
        protected void RemoveSelf(object? argument, Action<TThis>? callback) {
            Debug2.Assert.Operation.Valid( $"State {this} must have owner", Owner != null );
            if (Parent != null) {
                Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Stateful!.RemoveState( (TThis) this, argument, callback );
            }
        }

    }
}
