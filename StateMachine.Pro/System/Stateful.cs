namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public class Stateful<T> : IStateful<T> where T : StateBase<T> {

    // State
    T? IStateful<T>.State { get => State; set => State = value; }
    // State
    public T? State { get; private set; }

    // Constructor
    public Stateful() {
    }

    // SetState
    public virtual void SetState(T? state, object? argument = null) {
        IStateful<T>.SetState( this, state, argument );
    }

}
