#pragma warning disable CA2000 // Dispose objects before losing scope
namespace System.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

public class Tests_00 {

    [Test]
    public void Test_00() {
        var stateful = new Stateful<State>();
        stateful.SetState( null );
        stateful.SetState( new A() );
        stateful.SetState( new B() );
        stateful.SetState( null );
    }

    // State
    internal abstract class State : StateBase<State>, IDisposable {

        public State() {
            //TestContext.Out.WriteLine( "Constructor: " + GetType().Name );
        }
        public virtual void Dispose() {
            //TestContext.Out.WriteLine( "Dispose: " + GetType().Name );
        }

        protected override void OnActivate(object? argument) {
            TestContext.Out.WriteLine( "OnActivate: " + GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            TestContext.Out.WriteLine( "OnDeactivate: " + GetType().Name );
        }

    }
    // A
    internal class A : State {
    }
    // B
    internal class B : State {
    }
}
