namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var stateful = new Stateful();
            var a = new A();
            var b = new B();
            {
                // SetState a
                stateful.SetState( a, null, null );
                Assert.That( stateful.State, Is.EqualTo( a ) );

                Assert.That( a.Stateful, Is.EqualTo( stateful ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            }
            {
                // SetState a
                stateful.SetState( a, null, null );
                Assert.That( stateful.State, Is.EqualTo( a ) );

                Assert.That( a.Stateful, Is.EqualTo( stateful ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            }
            {
                // SetState b
                stateful.SetState( b, null, null );
                Assert.That( stateful.State, Is.EqualTo( b ) );

                Assert.That( a.Stateful, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );

                Assert.That( b.Stateful, Is.EqualTo( stateful ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Active ) );
            }
            {
                // SetState null
                stateful.SetState( null, null, null );
                Assert.That( stateful.State, Is.Null );

                Assert.That( a.Stateful, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            }
            {
                // SetState null
                stateful.SetState( null, null, null );
                Assert.That( stateful.State, Is.Null );

                Assert.That( a.Stateful, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            }
        }

    }
}
