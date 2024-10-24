# Overview
The library that helps you implement the state pattern, i.e. a stateful object.

# Reference
```
public interface IStateful {
}
public interface IStateful<T> : IStateful where T : StateBase<T> {

    protected T? State { get; set; }

    protected internal void SetState(T? state, object? argument = null);

}
public abstract class StateBase {
    public enum State_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }
}
public abstract class StateBase<T> : StateBase where T : StateBase<T> {

    public State_ State { get; }
    public IStateful<T>? Stateful { get; }
    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    public StateBase();
    protected virtual void DisposeWhenDeactivate();

    protected virtual void OnBeforeActivate(object? argument);
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
```

# Example
```
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
internal abstract class StateBase2 : StateBase<StateBase2> {

    protected override void OnActivate(object? argument) {
        TestContext.WriteLine( "OnActivate: " + GetType().Name );
    }
    protected override void OnDeactivate(object? argument) {
        TestContext.WriteLine( "OnDeactivate: " + GetType().Name );
    }

}
internal class A_State : StateBase2 {
}
internal class B_State : StateBase2 {
}
```
