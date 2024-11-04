#pragma warning disable CA2000 // Dispose objects before losing scope
namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public class Tests_00 {

    [Test]
    public void Test_00() {
        var stateful = new Stateful<StateBase2>();
        stateful.SetState( null );
        stateful.SetState( new A() );
        stateful.SetState( new B() );
        stateful.SetState( null );
    }

    // StateBase
    internal abstract class StateBase2 : StateBase<StateBase2>, IDisposable {

        public StateBase2() {
            //TestContext.WriteLine( "Constructor: " + GetType().Name );
        }
        public virtual void Dispose() {
            //TestContext.WriteLine( "Dispose: " + GetType().Name );
        }

        protected override void OnActivate(object? argument) {
            TestContext.WriteLine( "OnActivate: " + GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            TestContext.WriteLine( "OnDeactivate: " + GetType().Name );
        }

    }
    // A
    internal class A : StateBase2 {
    }
    // B
    internal class B : StateBase2 {
    }
}
