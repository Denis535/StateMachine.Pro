namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public class State : StateBase<State> {

        //public bool IsDisposed { get; private set; }

        public State() {
        }
        //public virtual void Dispose() {
        //    System.Assert.Operation.Message( $"State {this} must be non-disposed" ).Valid( !IsDisposed );
        //    System.Assert.Operation.Message( $"State {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
        //    System.Assert.Operation.Message( $"State {this} must have no stateful" ).Valid( Stateful == null );
        //    IsDisposed = true;
        //}

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            Trace.WriteLine( "OnActivate: " + this.GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            Trace.WriteLine( "OnDeactivate: " + this.GetType().Name );
        }

    }
}
