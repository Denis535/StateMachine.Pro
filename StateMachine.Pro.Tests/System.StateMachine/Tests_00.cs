namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var stateful = new Stateful();
            var a = new A();
            var b = new B();

            stateful.SetState( null, null, null );
            NUnit.Framework.Assert.That( stateful.State, Is.Null );
            {
                NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                NUnit.Framework.Assert.That( a.Stateful, Is.Null );
            }
            {
                NUnit.Framework.Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                NUnit.Framework.Assert.That( b.Stateful, Is.Null );
            }

            stateful.SetState( a, null, null );
            NUnit.Framework.Assert.That( stateful.State, Is.EqualTo( a ) );
            {
                NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );
                NUnit.Framework.Assert.That( a.Stateful, Is.EqualTo( stateful ) );
            }
            {
                NUnit.Framework.Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                NUnit.Framework.Assert.That( b.Stateful, Is.Null );
            }

            stateful.SetState( b, null, null );
            NUnit.Framework.Assert.That( stateful.State, Is.EqualTo( b ) );
            {
                NUnit.Framework.Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                NUnit.Framework.Assert.That( a.Stateful, Is.Null );
            }
            {
                NUnit.Framework.Assert.That( b.Activity, Is.EqualTo( State.Activity_.Active ) );
                NUnit.Framework.Assert.That( b.Stateful, Is.EqualTo( stateful ) );
            }

            stateful.SetState( null, null, null );
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

        // A
        internal class A : State {
        }
        // B
        internal class B : State {
        }
    }
}
