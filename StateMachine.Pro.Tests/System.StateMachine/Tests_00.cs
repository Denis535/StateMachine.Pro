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
        var a = new A();
        var b = new B();

        stateful.SetState( null );
        NUnit.Framework.Assert.That( stateful.State, Is.Null );
        {
            NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( a.Stateful, Is.Null );
        }
        {
            NUnit.Framework.Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( b.Stateful, Is.Null );
        }

        stateful.SetState( a );
        NUnit.Framework.Assert.That( stateful.State, Is.EqualTo( a ) );
        {
            NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );
            NUnit.Framework.Assert.That( a.Stateful, Is.EqualTo( stateful ) );
        }
        {
            NUnit.Framework.Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( b.Stateful, Is.Null );
        }

        stateful.SetState( b );
        NUnit.Framework.Assert.That( stateful.State, Is.EqualTo( b ) );
        {
            NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( a.Stateful, Is.Null );
        }
        {
            NUnit.Framework.Assert.That( b.Activity, Is.EqualTo( State.Activity_.Active ) );
            NUnit.Framework.Assert.That( b.Stateful, Is.EqualTo( stateful ) );
        }

        stateful.SetState( null );
        NUnit.Framework.Assert.That( stateful.State, Is.Null );
        {
            NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( a.Stateful, Is.Null );
        }
        {
            NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( a.Stateful, Is.Null );
        }
    }

    // State
    internal abstract class State : StateBase<State> {

        //public bool IsDisposed { get; private set; }

        public State() {
        }
        //public virtual void Dispose() {
        //    System.Assert.Operation.Message( $"Node {this} must be non-disposed" ).Valid( !IsDisposed );
        //    System.Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
        //    System.Assert.Operation.Message( $"Node {this} must have no stateful" ).Valid( Stateful == null );
        //    IsDisposed = true;
        //}

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