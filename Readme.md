# Overview
The library that helps you implement a stateful object, i.e. state pattern.

# Reference
```
namespace System.StateMachine;
public interface IStateful<T> where T : StateBase<T> {

    protected T? State { get; set; }

    protected void SetState(T? state, object? argument, Action<T>? callback);
    protected void AddState(T state, object? argument);
    protected void RemoveState(T state, object? argument, Action<T>? callback);

}
public abstract class StateBase<TThis> where TThis : StateBase<TThis> {

    private protected IStateful<TThis>? Owner { get; private set; }
    public IStateful<TThis>? Stateful { get; }

    public event Action<object?>? OnBeforeAttachEvent;
    public event Action<object?>? OnAfterAttachEvent;
    public event Action<object?>? OnBeforeDetachEvent;
    public event Action<object?>? OnAfterDetachEvent;

    public StateBase() {
    }

    internal virtual void Attach(IStateful<TThis> owner, object? argument);
    internal virtual void Detach(IStateful<TThis> owner, object? argument);

    protected virtual void OnBeforeAttach(object? argument);
    protected abstract void OnAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected abstract void OnDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract class StateBase2<TThis> : StateBase<TThis> where TThis : StateBase2<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public Activity_ Activity { get; private set; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    public StateBase2() {
    }

    internal sealed override void Attach(IStateful<TThis> owner, object? argument);
    internal sealed override void Detach(IStateful<TThis> owner, object? argument);

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    protected virtual void OnBeforeActivate(object? argument);
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
```
