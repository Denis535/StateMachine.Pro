#nullable enable
namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class StateBase<TThis> {

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
        //public StateBase() {
        //}

        // SetChild
        protected void SetChild(TThis? child, object? argument, Action<TThis>? callback) {
            if (this.Child != null) {
                this.RemoveChild( this.Child, argument, callback );
            }
            if (child != null) {
                this.AddChild( child, argument );
            }
        }
        protected void AddChild(TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no owner", child.Owner == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity_.Inactive );
            Assert.Operation.Valid( $"State {this} must have no child", this.Child == null );
            this.Child = child;
            this.Child.Attach( (TThis) this, argument );
        }
        protected void RemoveChild(TThis child, object? argument, Action<TThis>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} owner", child.Owner == this );
            Assert.Operation.Valid( $"State {this} must have {child} child", this.Child == child );
            this.Child.Detach( (TThis) this, argument );
            this.Child = null;
            callback?.Invoke( child );
        }
        protected void RemoveSelf(object? argument, Action<TThis>? callback) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Owner != null );
            if (this.Parent != null) {
                this.Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                this.Stateful!.RemoveState( (TThis) this, argument, callback );
            }
        }

    }
}
