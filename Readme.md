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

    public State_ State { get; }
    public IStateful? Stateful { get; }

}
public abstract class StateBase<TThis> : StateBase where TThis : StateBase<TThis> {

    public new IStateful<TThis>? Stateful { get; }

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
