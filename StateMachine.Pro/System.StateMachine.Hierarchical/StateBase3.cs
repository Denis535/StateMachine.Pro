namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract class StateBase3<TThis> : StateBase2<TThis> where TThis : StateBase3<TThis> {

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
        private protected override TThis? ChildInternal => Child;
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
        public StateBase3() {
        }

        // SetChild
        protected void SetChild(TThis child, object? argument, Action<TThis>? callback) {
            if (Child != null) {
                RemoveChild( child, argument, callback );
            }
            if (child != null) {
                AddChild( child, argument );
            }
        }
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"State {this} must have no child {child} state" ).Valid( Child == null );
            Child = child;
            Child.Attach( (TThis) this, argument );
        }
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis>? callback) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"State {this} must have child {child} state" ).Valid( Child == child );
            Child.Detach( (TThis) this, argument );
            Child = null;
            callback?.Invoke( child );
        }
        protected void RemoveSelf(object? argument, Action<TThis>? callback) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( Owner != null );
            if (Parent != null) {
                Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Stateful!.RemoveState( (TThis) this, argument, callback );
            }
        }

    }
}
