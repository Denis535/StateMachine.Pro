namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public interface IStateful {
}
public interface IStateful<T> : IStateful where T : StateBase<T> {

    // State
    protected T? State { get; set; }

    // SetState
    protected internal void SetState(T? state, object? argument = null);

    // Helpers
    protected static void SetState(IStateful<T> stateful, T? state, object? argument) {
        Assert.Argument.Message( $"Argument 'state' ({state}) must be valid" ).Valid( state != stateful.State );
        if (state != null) {
            Assert.Operation.Message( $"Stateful {stateful} must have no state" ).Valid( stateful.State == null );
            stateful.State = state;
            stateful.State.Activate( stateful, argument );
        } else {
            Assert.Operation.Message( $"Stateful {stateful} must have state" ).Valid( stateful.State != null );
            stateful.State.Deactivate( stateful, argument );
            stateful.State = null;
        }
    }

}
