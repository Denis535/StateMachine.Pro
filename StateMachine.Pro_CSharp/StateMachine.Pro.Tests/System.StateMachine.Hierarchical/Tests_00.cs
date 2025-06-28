namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );
                Assert.That( a.Child, Is.Null );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                Assert.That( b.Child, Is.Null );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState a
                stateful.SetState( a, null, null );
                Assert.That( stateful.State, Is.EqualTo( a ) );

                Assert.That( a.Stateful, Is.EqualTo( stateful ) );
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );
                Assert.That( a.Child, Is.Null );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                Assert.That( b.Child, Is.Null );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState b
                stateful.SetState( b, null, null );
                Assert.That( stateful.State, Is.EqualTo( b ) );

                Assert.That( a.Stateful, Is.Null );
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                Assert.That( a.Child, Is.Null );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );

                Assert.That( b.Stateful, Is.EqualTo( stateful ) );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Active ) );
                Assert.That( b.Child, Is.Null );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState null
                stateful.SetState( null, null, null );
                Assert.That( stateful.State, Is.Null );

                Assert.That( a.Stateful, Is.Null );
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                Assert.That( a.Child, Is.Null );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                Assert.That( b.Child, Is.Null );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState null
                stateful.SetState( null, null, null );
                Assert.That( stateful.State, Is.Null );

                Assert.That( a.Stateful, Is.Null );
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                Assert.That( a.Child, Is.Null );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );

                Assert.That( b.Stateful, Is.Null );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
                Assert.That( b.Child, Is.Null );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
        }

    }
}
