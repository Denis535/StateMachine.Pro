namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public class Tests {

    [Test]
    public void Test_00() {
        using var stateful = new Stateful();
    }

}
// Stateful
internal class Stateful : StatefulBase<StateBase2>, IDisposable {

    public Stateful() {
        SetState( null );
        SetState( new A_State() );
        SetState( new A_State() );
        SetState( new B_State() );
        SetState( null );
    }
    public void Dispose() {
        SetState( null );
    }

}
// StateBase2
internal abstract class StateBase2 : StateBase<StateBase2> {

    protected override void OnActivate(object? argument) {
        TestContext.WriteLine( "OnActivate: " + GetType().Name );
    }
    protected override void OnDeactivate(object? argument) {
        TestContext.WriteLine( "OnDeactivate: " + GetType().Name );
    }

}
// A_State
internal class A_State : StateBase2 {
}
// B_State
internal class B_State : StateBase2 {
}
